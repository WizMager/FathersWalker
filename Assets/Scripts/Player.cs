using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //private Transform _firstPosition;
    private InputActions _inputActions;
    private void Awake()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.TouchInput.Touch.Enable();
        _inputActions.TouchInput.Touch.performed += OnTouch;
    }

    private void OnTouch(InputAction.CallbackContext obj)
    {
        //transform.DOMove(_firstPosition.position, 3f);
    }

    private void OnDisable()
    {
        _inputActions.TouchInput.Touch.performed -= OnTouch;
        _inputActions.TouchInput.Touch.Disable();
    }
}