using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRInputManager
{
    public partial class XRInput
    {
        /// <summary>
        /// Klasa obs³uguj¹ca przyciski zwracaj¹ce wartoœæ float <0-1>
        /// </summary>
        [System.Serializable]
        public sealed class XRButtonFloat : XRButton<float, InputFeatureUsage<float>>
        {
            private static readonly Dictionary<XRInputs, InputFeatureUsage<float>> availableButtons = new Dictionary<XRInputs, InputFeatureUsage<float>>()
        {
            { XRInputs.TriggerFloat, CommonUsages.trigger },
            { XRInputs.GripFloat, CommonUsages.grip }
        };
            protected override Dictionary<XRInputs, InputFeatureUsage<float>> AvailableButtons => availableButtons;

            public XRButtonFloat(XRInputs button, UnityAction<XRNode, float> buttonAction) : base(button, buttonAction)
            {
            }

            public override void ProcessData(ControllerInputData inputData)
            {
                if (inputData.GetController.inputDevice.TryGetFeatureValue(GetButton, out float value) && !Value.Equals(value))
                {
                    Value = value;
                    InvokeButton(inputData.GetController.controllerNode);
                }
            }
        }
    }
}