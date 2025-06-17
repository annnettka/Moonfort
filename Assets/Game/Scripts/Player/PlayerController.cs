using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackCooldown = 1f;

    private Rigidbody _rb;
    private PlayerState _currentState = PlayerState.Idle;
    private float _lastAttackTime = -Mathf.Infinity;
    private IPlayerInput _input;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<IPlayerInput>();

        if (_input == null)
        {
            _input = FindFirstObjectByType<PlayerInputKeyboard>();
        }

        if (_input == null)
        {
            Debug.LogError("IPlayerInput реалізація не знайдена!");
        }
    }

    private void Update()
    {
        if (_input == null) return;

        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        Vector2 move = _input.MoveInput;
        if (move == Vector2.zero)
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            _currentState = PlayerState.Idle;
            return;
        }

        Vector3 direction = new Vector3(move.x, 0, move.y).normalized;
        Vector3 velocity = direction * moveSpeed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
        _currentState = PlayerState.Moving;
    }

    private void HandleAttack()
    {
        if (_input.AttackPressed && Time.time - _lastAttackTime >= attackCooldown)
        {
            _currentState = PlayerState.Attacking;
            _lastAttackTime = Time.time;
            Debug.Log("Player Attacked");
            Invoke(nameof(ResetToIdle), 0.5f);
        }
    }

    private void ResetToIdle()
    {
        if (_currentState == PlayerState.Attacking)
            _currentState = PlayerState.Idle;
    }
    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking
    }

}
