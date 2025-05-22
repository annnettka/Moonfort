using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour, IPlayersInput
{
    public Vector2 MoveInput
    {
        get
        {
            float x = 0f;
            float y = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;

            return new Vector2(x, y).normalized;
        }
    }

    public bool AttackPressed => Input.GetKeyDown(KeyCode.Space);
}
