using UnityEngine;
using UnityEngine.InputSystem;

public class StartScreenScript : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnAnyKey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FindAnyObjectByType<SceneManagerScript>().NextLevel();
        }
    }
}
