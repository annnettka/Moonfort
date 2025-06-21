using UnityEngine;

public class PlayerInputSystem : MonoBehaviour, IPlayerInput
{
    public Vector2 MoveInput
    {
        get
        {
            float x = 0f;
            float y = 0f;

            if (Input.GetKey(KeyCode.W)) y += 1f;
            if (Input.GetKey(KeyCode.S)) y -= 1f;
            if (Input.GetKey(KeyCode.D)) x += 1f;
            if (Input.GetKey(KeyCode.A)) x -= 1f;

            Vector2 input = new Vector2(x, y);
            return input.normalized; // Нормалізуємо, щоб не було швидшого руху по діагоналі
        }
    }

    public bool AttackPressed => Input.GetKeyDown(KeyCode.Space);
}
