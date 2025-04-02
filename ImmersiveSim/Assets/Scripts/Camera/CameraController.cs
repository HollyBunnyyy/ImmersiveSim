using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 2.0f;

    [SerializeField]
    private float _cameraZLeanDegrees = 1.0f;

    [SerializeField]
    private float _cameraLeanSmoothing = 2.0f;

    [SerializeField]
    private Camera _Camera;

    [SerializeField]
    private InputHandler _inputHandler;

    private Vector3 _inputRotationDelta;
    private Vector3 _targetRotationDelta;

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected void Update()
    {
        _inputRotationDelta = _inputHandler.MouseDelta * _mouseSensitivity;

        _targetRotationDelta.x = Mathf.Clamp(_targetRotationDelta.x - _inputRotationDelta.y, -90.0f, 90.0f);
        _targetRotationDelta.y += _inputRotationDelta.x;
        _targetRotationDelta.z = Mathf.Lerp(_targetRotationDelta.z, -_inputHandler.WASDAxis.x * _cameraZLeanDegrees, _cameraLeanSmoothing * Time.deltaTime);

        _Camera.transform.localEulerAngles = _targetRotationDelta;
    }
}