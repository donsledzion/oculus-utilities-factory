using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRInputManager
{
    public partial class XRInput
    {
        [System.Serializable]
        public sealed class XRButtonJoystickDir : XRButton<bool, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button>
        {
            public const float CLICK_TRESHOLD = .8f;

            private static readonly Dictionary<XRInputs, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button> availableButtons = new Dictionary<XRInputs, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button>()
        {
            { XRInputs.JoystickLeft, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button.PrimaryAxis2DLeft },
            { XRInputs.JoystickRight, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button.PrimaryAxis2DRight },
            { XRInputs.JoystickUp, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button.PrimaryAxis2DUp },
            { XRInputs.JoystickDown, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button.PrimaryAxis2DDown }
        };
            protected override Dictionary<XRInputs, UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button> AvailableButtons => availableButtons;

            public XRButtonJoystickDir(XRInputs button, UnityAction<XRNode, bool> buttonAction) : base(button, buttonAction)
            {
            }

            public override void ProcessData(ControllerInputData inputData)
            {
                if (UnityEngine.XR.Interaction.Toolkit.InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputData.GetController.controllerNode),
                                                                                 GetButton,
                                                                                 out bool isPressed,
                                                                                 CLICK_TRESHOLD))
                {
                    if (isPressed && !Value)
                    {
                        if (!Value)
                        {
                            Value = true;
                            InvokeButton(inputData.GetController.controllerNode);
                        }

                    }
                    else if (!isPressed && Value)
                    {
                        Value = false;
                        InvokeButton(inputData.GetController.controllerNode);
                    }
                }
            }
        }
    }
}