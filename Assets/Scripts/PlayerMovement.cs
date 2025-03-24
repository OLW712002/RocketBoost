using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputAction thrush;

    private void OnEnable()
    {
        thrush.Enable();
    }

    void Update()
    {
        if (thrush.IsPressed()) Debug.Log("Im Flyiiiing");
    }
}
