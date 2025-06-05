using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        RespondToQuittingKey();
    }

    private void RespondToQuittingKey()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
            Debug.Log("Quitting");
        }
    }
}
