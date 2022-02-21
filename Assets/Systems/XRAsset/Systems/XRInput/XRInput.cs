using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRInputManager
{
    public partial class XRInput : MonoBehaviour
    {
        #region Events
        public static event System.Action<XRNode> onTriggerDown;
        public static event System.Action<XRNode> onTriggerUp;
        public static event System.Action<XRNode, float> onTrigger;

        public static event System.Action<XRNode> onGripDown;
        public static event System.Action<XRNode> onGripUp;
        public static event System.Action<XRNode, float> onGrip;

        public static event System.Action<XRNode> onJoystickDown;
        public static event System.Action<XRNode> onJoystickUp;
        public static event System.Action<XRNode, Vector2> onJoystickAxis2D;

        public static event System.Action<XRNode> onJoystickLeftDown;
        public static event System.Action<XRNode> onJoystickLeftUp;

        public static event System.Action<XRNode> onJoystickRightDown;
        public static event System.Action<XRNode> onJoystickRightUp;

        public static event System.Action<XRNode> onJoystickUpDown;
        public static event System.Action<XRNode> onJoystickUpUp;

        public static event System.Action<XRNode> onJoystickDownDown;
        public static event System.Action<XRNode> onJoystickDownUp;

        public static event System.Action<XRNode> onButtonOneDown;
        public static event System.Action<XRNode> onButtonOneUp;

        public static event System.Action<XRNode> onButtonTwoDown;
        public static event System.Action<XRNode> onButtonTwoUp;
        #endregion

        [HideInInspector, SerializeField] ControllerInputData[] inputDatas = default;

        private void Start()
        {

            XRController leftController = GameObject.Find("LeftHand Controller").GetComponent<XRController>();
            XRController rightController = GameObject.Find("RightHand Controller").GetComponent<XRController>();
            inputDatas = new ControllerInputData[2] { DefaultControllerInputDataBuilder(leftController), DefaultControllerInputDataBuilder(rightController) };
        }

        #region RegisterUnregister
        public static void RegisterButtonDown(XRInputs button, System.Action<XRNode> action)
        {
            switch (button)
            {
                case XRInputs.Trigger:
                    onTriggerDown += action;
                    break;
                case XRInputs.Grip:
                    onGripDown += action;
                    break;
                case XRInputs.Joystick:
                    onJoystickDown += action;
                    break;
                case XRInputs.JoystickLeft:
                    onJoystickLeftDown += action;
                    break;
                case XRInputs.JoystickRight:
                    onJoystickRightDown += action;
                    break;
                case XRInputs.JoystickUp:
                    onJoystickUpDown += action;
                    break;
                case XRInputs.JoystickDown:
                    onJoystickDownDown += action;
                    break;
                case XRInputs.One:
                    onButtonOneDown += action;
                    break;
                case XRInputs.Two:
                    onButtonTwoDown += action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButtonDown");
                    break;
            }
        }

        public static void RegisterButtonUp(XRInputs button, System.Action<XRNode> action)
        {
            switch (button)
            {
                case XRInputs.Trigger:
                    onTriggerUp += action;
                    break;
                case XRInputs.Grip:
                    onGripUp += action;
                    break;
                case XRInputs.Joystick:
                    onJoystickUp += action;
                    break;
                case XRInputs.JoystickLeft:
                    onJoystickLeftUp += action;
                    break;
                case XRInputs.JoystickRight:
                    onJoystickRightUp += action;
                    break;
                case XRInputs.JoystickUp:
                    onJoystickUpUp += action;
                    break;
                case XRInputs.JoystickDown:
                    onJoystickDownUp += action;
                    break;
                case XRInputs.One:
                    onButtonOneUp += action;
                    break;
                case XRInputs.Two:
                    onButtonTwoUp += action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButtonUp");
                    break;
            }
        }

        public static void RegisterButton(XRInputs button, System.Action<XRNode, float> action)
        {
            switch (button)
            {
                case XRInputs.TriggerFloat:
                    onTrigger += action;
                    break;
                case XRInputs.GripFloat:
                    onGrip += action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButton <float>");
                    break;
            }
        }

        public static void RegisterButton(XRInputs button, System.Action<XRNode, Vector2> action)
        {
            switch (button)
            {
                case XRInputs.JoystickAxis:
                    onJoystickAxis2D += action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButton <Vector2>");
                    break;
            }
        }

        public static void UnregisterButtonDown(XRInputs button, System.Action<XRNode> action)
        {
            switch (button)
            {
                case XRInputs.Trigger:
                    onTriggerDown -= action;
                    break;
                case XRInputs.Grip:
                    onGripDown -= action;
                    break;
                case XRInputs.Joystick:
                    onJoystickDown -= action;
                    break;
                case XRInputs.JoystickLeft:
                    onJoystickLeftDown -= action;
                    break;
                case XRInputs.JoystickRight:
                    onJoystickRightDown -= action;
                    break;
                case XRInputs.JoystickUp:
                    onJoystickUpDown -= action;
                    break;
                case XRInputs.JoystickDown:
                    onJoystickDownDown -= action;
                    break;
                case XRInputs.One:
                    onButtonOneDown -= action;
                    break;
                case XRInputs.Two:
                    onButtonTwoDown -= action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButtonDown");
                    break;
            }
        }

        public static void UnregisterButtonUp(XRInputs button, System.Action<XRNode> action)
        {
            switch (button)
            {
                case XRInputs.Trigger:
                    onTriggerUp -= action;
                    break;
                case XRInputs.Grip:
                    onGripUp -= action;
                    break;
                case XRInputs.Joystick:
                    onJoystickUp -= action;
                    break;
                case XRInputs.JoystickLeft:
                    onJoystickLeftUp -= action;
                    break;
                case XRInputs.JoystickRight:
                    onJoystickRightUp -= action;
                    break;
                case XRInputs.JoystickUp:
                    onJoystickUpUp -= action;
                    break;
                case XRInputs.JoystickDown:
                    onJoystickDownUp -= action;
                    break;
                case XRInputs.One:
                    onButtonOneUp -= action;
                    break;
                case XRInputs.Two:
                    onButtonTwoUp -= action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for RegisterButtonUp");
                    break;
            }
        }

        public static void UnregisterButton(XRInputs button, System.Action<XRNode, float> action)
        {
            switch (button)
            {
                case XRInputs.TriggerFloat:
                    onTrigger -= action;
                    break;
                case XRInputs.GripFloat:
                    onGrip -= action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for UnregisterButton <float>");
                    break;
            }
        }

        public static void UnregisterButton(XRInputs button, System.Action<XRNode, Vector2> action)
        {
            switch (button)
            {
                case XRInputs.JoystickAxis:
                    onJoystickAxis2D -= action;
                    break;
                default:
                    Debug.LogError($"Unkown button {button} for UnregisterButton <Vector2>");
                    break;
            }
        }
        #endregion

        #region Buttons
        public void OnTrigger(XRNode xrNode, bool isDown)
        {
            OnButton(onTriggerDown, onTriggerUp, xrNode, isDown);
        }

        public void OnTrigger(XRNode xrNode, float value)
        {
            OnButton(onTrigger, xrNode, value);
        }

        public void OnGrip(XRNode xrNode, bool isDown)
        {
            OnButton(onGripDown, onGripUp, xrNode, isDown);
        }

        public void OnGrip(XRNode xrNode, float value)
        {
            OnButton(onGrip, xrNode, value);
        }

        public void OnJoystick(XRNode xrNode, bool isDown)
        {
            OnButton(onJoystickDown, onJoystickUp, xrNode, isDown);
        }

        public void OnJoystickAxis2D(XRNode xRNode, Vector2 axis)
        {
            OnButton(onJoystickAxis2D, xRNode, axis);
        }

        public void OnJoystickLeft(XRNode xrNode, bool isDown)
        {
            OnButton(onJoystickLeftDown, onJoystickLeftUp, xrNode, isDown);
        }

        public void OnJoystickRight(XRNode xrNode, bool isDown)
        {
            OnButton(onJoystickRightDown, onJoystickRightUp, xrNode, isDown);
        }

        public void OnJoystickUp(XRNode xrNode, bool isDown)
        {
            OnButton(onJoystickUpDown, onJoystickUpUp, xrNode, isDown);
        }

        public void OnJoystickDown(XRNode xrNode, bool isDown)
        {
            OnButton(onJoystickDownDown, onJoystickDownUp, xrNode, isDown);
        }

        public void OnOne(XRNode xRNode, bool isDown)
        {
            OnButton(onButtonOneDown, onButtonOneUp, xRNode, isDown);
        }

        public void OnTwo(XRNode xRNode, bool isDown)
        {
            OnButton(onButtonTwoDown, onButtonTwoUp, xRNode, isDown);
        }

        private void OnButton(System.Action<XRNode> onButtonDown, System.Action<XRNode> onButtonUp, XRNode xrNode, bool isDown)
        {
            if (isDown) onButtonDown?.Invoke(xrNode);
            else onButtonUp?.Invoke(xrNode);
        }

        private void OnButton(System.Action<XRNode, float> onButtonFloat, XRNode xrNode, float value)
        {
            onButtonFloat?.Invoke(xrNode, value);
        }

        private void OnButton(System.Action<XRNode, Vector2> onButtonAxis, XRNode xRNode, Vector2 axis)
        {
            onButtonAxis?.Invoke(xRNode, axis);
        }
        #endregion

        private void Reset()
        {
        }

        private void Update()
        {
            foreach (var inputData in inputDatas)
            {
                ProcessInteractor(inputData);
            }
        }

        private void ProcessInteractor(ControllerInputData inputData)
        {
            inputData.ProcessDatas();
        }

        private ControllerInputData DefaultControllerInputDataBuilder(XRController controller)
        {
            XRButtonJoystickDir[] buttonJoystickDir =
            {
                new XRButtonJoystickDir(XRInputs.JoystickLeft, OnJoystickLeft),
                new XRButtonJoystickDir(XRInputs.JoystickRight, OnJoystickRight),
                new XRButtonJoystickDir(XRInputs.JoystickUp, OnJoystickUp),
                new XRButtonJoystickDir(XRInputs.JoystickDown, OnJoystickDown)
        };

            XRButtonBool[] buttonBools =
            {
            new XRButtonBool(XRInputs.Trigger, OnTrigger),
            new XRButtonBool(XRInputs.Grip, OnGrip),
            new XRButtonBool(XRInputs.Joystick, OnJoystick),
            new XRButtonBool(XRInputs.One, OnOne),
            new XRButtonBool(XRInputs.Two, OnTwo)
        };

            XRButtonFloat[] buttonFloats =
            {
            new XRButtonFloat(XRInputs.TriggerFloat, OnTrigger),
            new XRButtonFloat(XRInputs.GripFloat, OnGrip)
        };

            XRButtonVector2[] buttonVector2s =
            {
            new XRButtonVector2(XRInputs.JoystickAxis, OnJoystickAxis2D)
        };

            return new ControllerInputData(controller, buttonJoystickDir, buttonBools, buttonFloats, buttonVector2s);
        }
    }
}