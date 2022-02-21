using UnityEngine;
using UnityEngine.XR;
using XRInputManager;

public class SnapTurn : MonoBehaviour
{

    [SerializeField] XRInput.XRInputs btnInput = XRInput.XRInputs.Grip;

    public float turnAngle = 45f;
    private Vector3 rotateVector = Vector3.zero;

    private void OnEnable()
    {
        XRInput.onJoystickLeftDown += TurnLeft;
        XRInput.onJoystickRightDown += TurnRight;
    }


    private void OnDisable()
    {
        XRInput.onJoystickLeftDown -= TurnLeft;
        XRInput.onJoystickRightDown -= TurnRight;
    }

    private void Turn(float value)
    {
        rotateVector.y = value;
        transform.eulerAngles += rotateVector;
    }

    private void TurnLeft(XRNode xRNode)
    {
        Turn(-turnAngle);
    }

    private void TurnRight(XRNode xRNode)
    {
        Turn(turnAngle);
    }
}