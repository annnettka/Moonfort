using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputSystem : MonoBehaviour, IPlayersInput
{
    public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    public bool AttackPressed => Input.GetKeyDown(KeyCode.Space);
}
