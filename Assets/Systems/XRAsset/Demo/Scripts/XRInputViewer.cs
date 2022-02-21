using UnityEngine;
using UnityEngine.XR;
using XRInputManager;
using static XRInputManager.XRInput;

public class XRInputViewer : MonoBehaviour
{
    [Header("Registered Button")]
    [SerializeField] XRInputs buttonRegisterBool = XRInputs.Grip;
    [SerializeField] XRInputs buttonRegisterFloat = XRInputs.TriggerFloat;
    [SerializeField] XRInputs buttonRegisterVector2 = XRInputs.JoystickAxis;

    [Header("Grip")]
    [ReadOnly, SerializeField] bool gripIsDown;
    [ReadOnly, SerializeField] float gripValue;

    [Header("Trigger")]
    [ReadOnly, SerializeField] bool triggerIsDown;
    [ReadOnly, SerializeField] float triggerValue;

    [Header("Joystick")]
    [ReadOnly, SerializeField] bool joystickIsDown;
    [ReadOnly, SerializeField] Vector2 joystickAxis;

    [Space]
    [ReadOnly, SerializeField] bool joystickLeft;
    [ReadOnly, SerializeField] bool joystickRight;
    [ReadOnly, SerializeField] bool joystickUp;
    [ReadOnly, SerializeField] bool joystickDown;

    [Header("X/A, Y/B")]
    [ReadOnly, SerializeField] bool buttonOne;
    [ReadOnly, SerializeField] bool buttonTwo;

    private void OnEnable()
    {
        XRInput.RegisterButtonDown(buttonRegisterBool, ButtonRegister);
        XRInput.RegisterButton(buttonRegisterFloat, ButtonRegisterFloat);
        XRInput.RegisterButton(buttonRegisterVector2, ButtonRegisterVector2);

        XRInput.onGrip += XRInput_onGrip;
        XRInput.onGripDown += XRInput_onGripDown;
        XRInput.onGripUp += XRInput_onGripUp;

        XRInput.onTrigger += XRInput_onTrigger;
        XRInput.onTriggerDown += XRInput_onTriggerDown;
        XRInput.onTriggerUp += XRInput_onTriggerUp;

        XRInput.onJoystickAxis2D += XRInput_onJoystickAxis2D;
        XRInput.onJoystickDown += XRInput_onJoystickDown;
        XRInput.onJoystickUp += XRInput_onJoystickUp;

        XRInput.onJoystickDownDown += XRInput_onJoystickDownDown;
        XRInput.onJoystickDownUp += XRInput_onJoystickDownUp;

        XRInput.onJoystickUpDown += XRInput_onJoystickUpDown;
        XRInput.onJoystickUpUp += XRInput_onJoystickUpUp;

        XRInput.onJoystickLeftDown += XRInput_onJoystickLeftDown;
        XRInput.onJoystickLeftUp += XRInput_onJoystickLeftUp;

        XRInput.onJoystickRightDown += XRInput_onJoystickRightDown;
        XRInput.onJoystickRightUp += XRInput_onJoystickRightUp;

        XRInput.onButtonOneDown += XRInput_onButtonOneDown;
        XRInput.onButtonOneUp += XRInput_onButtonOneUp;

        XRInput.onButtonTwoDown += XRInput_onButtonTwoDown;
        XRInput.onButtonTwoUp += XRInput_onButtonTwoUp;
    }

    private void OnDisable()
    {
        XRInput.onGrip -= XRInput_onGrip;
        XRInput.onGripDown -= XRInput_onGripDown;
        XRInput.onGripUp -= XRInput_onGripUp;

        XRInput.onTrigger -= XRInput_onTrigger;
        XRInput.onTriggerDown -= XRInput_onTriggerDown;
        XRInput.onTriggerUp -= XRInput_onTriggerUp;

        XRInput.onJoystickAxis2D -= XRInput_onJoystickAxis2D;
        XRInput.onJoystickDown -= XRInput_onJoystickDown;
        XRInput.onJoystickUp -= XRInput_onJoystickUp;

        XRInput.onJoystickDownDown -= XRInput_onJoystickDownDown;
        XRInput.onJoystickDownUp -= XRInput_onJoystickDownUp;

        XRInput.onJoystickUpDown -= XRInput_onJoystickUpDown;
        XRInput.onJoystickUpUp -= XRInput_onJoystickUpUp;

        XRInput.onJoystickLeftDown -= XRInput_onJoystickLeftDown;
        XRInput.onJoystickLeftUp -= XRInput_onJoystickLeftUp;

        XRInput.onJoystickRightDown -= XRInput_onJoystickRightDown;
        XRInput.onJoystickRightUp -= XRInput_onJoystickRightUp;
    }

    private void XRInput_onButtonTwoUp(XRNode xrNode)
    {
        buttonTwo = false;
    }

    private void XRInput_onButtonTwoDown(XRNode xrNode)
    {
        buttonTwo = true;
    }

    private void XRInput_onButtonOneUp(XRNode xrNode)
    {
        buttonOne = false;
    }

    private void XRInput_onButtonOneDown(XRNode xrNode)
    {
        buttonOne = true;
    }

    private void XRInput_onJoystickRightUp(XRNode xrNode)
    {
        joystickRight = false;
    }

    private void XRInput_onJoystickRightDown(XRNode xrNode)
    {
        joystickRight = true;
    }

    private void XRInput_onJoystickLeftUp(XRNode xrNode)
    {
        joystickLeft = false;
    }

    private void XRInput_onJoystickLeftDown(XRNode xrNode)
    {
        joystickLeft = true;
    }

    private void XRInput_onJoystickUpUp(XRNode xrNode)
    {
        joystickUp = false;
    }

    private void XRInput_onJoystickUpDown(XRNode xrNode)
    {
        joystickUp = true;
    }

    private void XRInput_onJoystickDownUp(XRNode xrNode)
    {
        joystickDown = false;
    }

    private void XRInput_onJoystickDownDown(XRNode xrNode)
    {
        joystickDown = true;
    }

    private void XRInput_onJoystickUp(XRNode xrNode)
    {
        joystickIsDown = false;
    }

    private void XRInput_onJoystickDown(XRNode xrNode)
    {
        joystickIsDown = true;
    }

    private void XRInput_onJoystickAxis2D(XRNode xrNode, Vector2 axis)
    {
        joystickAxis = axis;
    }

    private void XRInput_onTriggerUp(XRNode xrNode)
    {
        triggerIsDown = false;
    }

    private void XRInput_onTriggerDown(XRNode xrNode)
    {
        triggerIsDown = true;
    }

    private void XRInput_onTrigger(XRNode xrNode, float value)
    {
        triggerValue = value;
    }

    private void XRInput_onGripUp(XRNode xrNode)
    {
        gripIsDown = false;
    }

    private void XRInput_onGripDown(XRNode xrNode)
    {
        gripIsDown = true;
    }

    private void XRInput_onGrip(XRNode xrNode, float value)
    {
        gripValue = value;
    }

    private void ButtonRegister(XRNode xrNode)
    {
        Debug.Log($"ButtonRegister node: {xrNode}");
    }

    private void ButtonRegisterFloat(XRNode xrNode, float value)
    {
        Debug.Log($"ButtonRegister node: {xrNode}, value: {value}");
    }

    private void ButtonRegisterVector2(XRNode xrNode, Vector2 value)
    {
        Debug.Log($"ButtonRegister node: {xrNode}, value: {value}");
    }
}
