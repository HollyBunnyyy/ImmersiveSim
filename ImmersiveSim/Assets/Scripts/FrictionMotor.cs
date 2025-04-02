using UnityEngine;

public class FrictionMotor : MonoBehaviour
{
    public Vector3 Velocity => _rigidbody.linearVelocity;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private AnimationCurve _frictionCurve;

    private float _velocityDirectionScalar;
    private float _frictionToApply;

    private Vector3 _targetVelocity;
    private Vector3 _targetAcceleration;
    private Vector3 _gravityScalar = new Vector3(1.0f, 0.0f, 1.0f);

    protected void Reset()
    {
        _frictionCurve = new AnimationCurve();

        _frictionCurve.keys = new Keyframe[]
        {
            new Keyframe( -1.0f,  2.0f,  0.0f,  0.0f ),
            new Keyframe(  0.0f,  1.0f,  0.0f,  0.0f ),
            new Keyframe(  1.0f,  1.0f,  0.0f,  0.0f )
        };
    }

    public void ApplyFrictionForce( Vector3 directionToApply, float forceToApply, float frictionAmount )
    {
        _velocityDirectionScalar = Vector3.Dot(directionToApply, _rigidbody.linearVelocity.normalized);

        _frictionToApply = frictionAmount * _frictionCurve.Evaluate(_velocityDirectionScalar);

        // Calculates the position between the current velocity and target velocity by moving 'frictionToApply' units.
        // -- Note this is different from Vector3.Lerp as lerp calculates T as a percentage, where this calculates it as units.
        _targetVelocity = Vector3.MoveTowards(_targetVelocity, directionToApply * forceToApply, _frictionToApply * Time.fixedDeltaTime);

        // Calculates the amount of force to apply based off the difference of the rigidbody's current velocity and
        // the desired target velocity - also works as a corrective force if the rigidbody's velocity is higher than
        // the given targetVelocity.
        _targetAcceleration = (_targetVelocity - _rigidbody.linearVelocity) / Time.fixedDeltaTime;

        // This limits the amount of force that can be applied by the acceleration amount.
        // Since the formula above can act as a 'corrective force' it's also important to limit how much it can
        // correct by otherwise it will just instantly stop and feel unrealistic. 
        _targetAcceleration = Vector3.ClampMagnitude(_targetAcceleration, _frictionToApply);
    }

    protected void FixedUpdate()
    {
        // Finally, apply our desired acceleration amount.
        _rigidbody.AddForce( Vector3.Scale( _targetAcceleration, _gravityScalar ) * _rigidbody.mass);
    }
}
