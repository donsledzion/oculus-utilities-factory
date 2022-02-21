using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRInputManager
{
    public partial class XRInput
    {
        /// <summary>
        /// Klasa obs³uguj¹ca przyciski zwracaj¹ce wartoœæ Vector2 (-1;-1 - 1;1)
        /// </summary>
        [System.Serializable]
        public sealed class XRButtonVector2 : XRButton<Vector2, InputFeatureUsage<Vector2>>
        {
            private static readonly Dictionary<XRInputs, InputFeatureUsage<Vector2>> availableButtons = new Dictionary<XRInputs, InputFeatureUsage<Vector2>>()
        {
            { XRInputs.JoystickAxis, CommonUsages.primary2DAxis}
        };
            protected override Dictionary<XRInputs, InputFeatureUsage<Vector2>> AvailableButtons => availableButtons;

            public XRButtonVector2(XRInputs button, UnityAction<XRNode, Vector2> buttonAction) : base(button, buttonAction)
            {
            }

            public override void ProcessData(ControllerInputData inputData)
            {
                if (inputData.GetController.inputDevice.TryGetFeatureValue(GetButton, out Vector2 value) && !Value.Equals(value))
                {
                    Value = value;
                    InvokeButton(inputData.GetController.controllerNode);
                }
            }
        }
    }
}