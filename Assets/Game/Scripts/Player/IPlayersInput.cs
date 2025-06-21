using UnityEngine;

public interface IPlayersInput
{
    Vector2 MoveInput { get; }
    bool AttackPressed { get; }
}
