using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputSystem))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackCooldown = 1f;

    private Rigidbody _rb;
    private IPlayersInput _input;

    private float _lastAttackTime = -Mathf.Infinity;

    private enum PlayerState { Idle, Moving, Attacking }
    private PlayerState _currentState = PlayerState.Idle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<IPlayersInput>();

        if (_input == null)
        {
            Debug.LogError("IPlayersInput implementation not found on player");
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (_input == null) return;

        Vector2 moveInput = _input.MoveInput;

        if (moveInput != Vector2.zero)
        {
            Move(moveInput);
            _currentState = PlayerState.Moving;
        }
        else if (_currentState != PlayerState.Attacking)
        {
            _currentState = PlayerState.Idle;
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }

        if (_input.AttackPressed && Time.time - _lastAttackTime >= attackCooldown)
        {
            Attack();
        }
    }

    private void Move(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y);
        Vector3 velocity = direction * moveSpeed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }

    private void Attack()
    {
        _currentState = PlayerState.Attacking;
        _lastAttackTime = Time.time;
        Debug.Log("Attack!");

        Invoke(nameof(ResetState), 0.5f);
    }

    private void ResetState()
    {
        if (_currentState == PlayerState.Attacking)
            _currentState = PlayerState.Idle;
    }
}
