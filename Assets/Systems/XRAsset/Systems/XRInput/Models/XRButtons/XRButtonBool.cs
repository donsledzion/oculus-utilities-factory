using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRInputManager
{
    public partial class XRInput
    {
        /// <summary>
        /// Klasa obs³uguj¹ca przyciski zwracaj¹ce wartoœæ bool
        /// </summary>
        [System.Serializable]
        public sealed class XRButtonBool : XRButton<bool, InputFeatureUsage<bool>>
        {
            private static readonly Dictionary<XRInputs, InputFeatureUsage<bool>> availableButtons = new Dictionary<XRInputs, InputFeatureUsage<bool>>()
        {
            { XRInputs.Trigger, CommonUsages.triggerButton },
            { XRInputs.Grip, CommonUsages.gripButton },
            { XRInputs.Joystick, CommonUsages.primary2DAxisClick },
            { XRInputs.One, CommonUsages.primaryButton },
            { XRInputs.Two, CommonUsages.secondaryButton }
        };
            protected override Dictionary<XRInputs, InputFeatureUsage<bool>> AvailableButtons => availableButtons;

            public XRButtonBool(XRInputs button, UnityAction<XRNode, bool> buttonAction) : base(button, buttonAction)
            {

            }

            public override void ProcessData(ControllerInputData inputData)
            {
                if (inputData.GetController.inputDevice.TryGetFeatureValue(GetButton, out bool value) && !Value.Equals(value))
                {
                    Value = value;
                    InvokeButton(inputData.GetController.controllerNode);
                }
            }
        }
    }
}