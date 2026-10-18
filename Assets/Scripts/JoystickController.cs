using UnityEngine;

// Simple joystick/controller input helper.
// Uses Unity's old Input system axes (Horizontal, Vertical) which usually map to joystick left stick.
// Rotation can be controlled by "Rotate" axis (custom) or by keys Q/E.
public class JoystickController : MonoBehaviour
{
    public Transform playerTransform;
    public LauncherController launcher;
    public float moveSpeed = 5f;
    public float rotationSpeed = 60f;

    void Update()
    {
        // Movement: Horizontal / Vertical (keyboard or joystick left stick)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0f, v).normalized;
        if (playerTransform != null)
        {
            playerTransform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        }

        // Rotation: try axis "Rotate" then fallback keys
        float rot = 0f;
        try { rot = Input.GetAxis("Rotate"); } catch { rot = 0f; }
        if (Mathf.Abs(rot) < 0.01f)
        {
            if (Input.GetKey(KeyCode.Q)) rot = -1f;
            if (Input.GetKey(KeyCode.E)) rot = 1f;
        }

        if (launcher != null && Mathf.Abs(rot) > 0.001f)
        {
            launcher.transform.Rotate(Vector3.up, rot * rotationSpeed * Time.deltaTime);
        }

        // Fire with joystick button (e.g., JoystickButton0) or spacebar
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {
            // start charging is handled by LauncherController using Input.GetKey methods
        }
    }
}
