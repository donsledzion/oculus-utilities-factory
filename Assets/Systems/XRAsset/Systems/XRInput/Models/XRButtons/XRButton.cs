using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRInputManager
{
    public partial class XRInput
    {
        [System.Serializable]
        public abstract class XRButton<T, Y> : IButtonInvoke, IProcessData where T : struct
        {
            protected abstract Dictionary<XRInputs, Y> AvailableButtons { get; }

            [SerializeField] XRInputs button = default;
            [SerializeField] UnityEventXRNode<T> buttonEvent = default;

            public Y GetButton => AvailableButtons[button];
            public T Value { get; set; }

            public XRButton(XRInputs button, UnityAction<XRNode, T> action)
            {
                this.button = button;

                buttonEvent = new UnityEventXRNode<T>();
#if UNITY_EDITOR
                UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonEvent, action);
#else
                buttonEvent.AddListener(action);
#endif
            }

            public abstract void ProcessData(ControllerInputData inputData);

            public void InvokeButton(XRNode xrNode)
            {
                buttonEvent.Invoke(xrNode, Value);
            }
        }
    }
}