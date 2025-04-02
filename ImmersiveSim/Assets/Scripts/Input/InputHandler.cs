using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private float _mouseScaling   = 0.05f;
    //private float _gamepadScaling = 0.15f;

    public Mouse CurrentMouse           => Mouse.current;
    public Gamepad CurrentGamepad       => Gamepad.current;
    public float ScrollDelta            => CurrentMouse.scroll.ReadValue().normalized.y;
    public Vector3 MouseScreenPosition  => CurrentMouse.position.ReadValue();
    public Vector3 MouseDelta           => CurrentMouse.delta.ReadValue() * _mouseScaling;
    public Vector3 WASDAxis             { get; private set; }

    private InputBindings InputBindings;

    private void Awake()
    {
        InputBindings = new InputBindings();

        InputBindings.Keyboard.WASDMovement.performed += context => WASDAxis = InputBindings.Keyboard.WASDMovement.ReadValue<Vector3>().normalized;
        InputBindings.Keyboard.WASDMovement.canceled += context => WASDAxis = Vector3.zero;
    }

    private void OnEnable()
    {
        InputBindings.Keyboard.Enable();
    }

    private void OnDisable()
    {
        InputBindings.Keyboard.Disable();
    }
}
