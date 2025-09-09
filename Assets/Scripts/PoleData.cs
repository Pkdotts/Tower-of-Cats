using UnityEngine;


[CreateAssetMenu(menuName = "Pole Data")]
public class PoleData : ScriptableObject 
{

    // Multiplier on the angle
    public float angleAmplitude = 1f;
    
    // The amount of swaying when the angle is at 0
    public float swayAngleStartAmplitude = 0f;
    // How much the angle increases the more tilted it is
    public float swayAngleAmplitude = 0f;

    // The speed at which the pole sways when at angle 0
    public float swaySpeedStartAmplitude = 0f;

    // How much the speed of the sway increases the more tilted it becomes
    public float swaySpeedAmplitude = 0f;

    public float swaySpeed = 0f;
    // Angle at which the pole falls
    public float fallAngle;

    
    
}