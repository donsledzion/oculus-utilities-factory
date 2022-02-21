using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace XRInputManager
{
    public partial class XRInput
    {
        [System.Serializable]
        public class ControllerInputData
        {
            [SerializeField] XRController controller = default;

            [SerializeField] XRButtonJoystickDir[] buttonJoystickDir = default;
            [SerializeField] XRButtonBool[] buttonBools = default;
            [SerializeField] XRButtonFloat[] buttonFloats = default;
            [SerializeField] XRButtonVector2[] buttonVector2s = default;

            public XRController GetController => controller;

            public void ProcessDatas()
            {
                foreach (var xrButton in buttonJoystickDir) xrButton.ProcessData(this);
                foreach (var xrButton in buttonBools) xrButton.ProcessData(this);
                foreach (var xrButton in buttonFloats) xrButton.ProcessData(this);
                foreach (var xrButton in buttonVector2s) xrButton.ProcessData(this);
            }

            public ControllerInputData(XRController controller, XRButtonJoystickDir[] buttonJoystickDir, XRButtonBool[] buttonBools, XRButtonFloat[] buttonFloats, XRButtonVector2[] buttonVector2s)
            {
                this.controller = controller;
                this.buttonJoystickDir = buttonJoystickDir;
                this.buttonBools = buttonBools;
                this.buttonFloats = buttonFloats;
                this.buttonVector2s = buttonVector2s;
            }
        }
    }
}