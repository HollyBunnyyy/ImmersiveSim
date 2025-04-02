using UnityEngine;

[CreateAssetMenu( menuName = "Settings/InputSettings" )]
public class InputSettings : ScriptableObject
{
    public float MouseScaling   = 0.05f;
    public float GamepadScaling = 0.15f;
}
