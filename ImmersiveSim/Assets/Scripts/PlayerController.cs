using UnityEngine;

public class PlayerController : FrictionMotor
{
    [SerializeField]
    private float _movementSpeed = 4.0f;

    [SerializeField]
    private float _frictionAmount = 16.0f;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private InputHandler _inputHandler;

    private Vector3 _inputDirectionFromCameraFacing;

    protected void Update()
    {

        _inputDirectionFromCameraFacing = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * _inputHandler.WASDAxis;

        this.ApplyFrictionForce(_inputDirectionFromCameraFacing, _movementSpeed, _frictionAmount);


    }
}
