using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit
{
    [RequireComponent(typeof(Rigidbody))]
    public class XRGrabInteractableAdapter : XRGrabInteractable
    {
        private new Rigidbody rigidbody;

        private float drag, angularDrag;

        protected override void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            drag = rigidbody.drag;
            angularDrag = rigidbody.angularDrag;

            base.Awake();
        }

        public void ResetRigidbodyToDefault()
        {
            rigidbody.drag = drag;
            rigidbody.angularDrag = angularDrag;
        }

        protected override void OnSelectEntered(XRBaseInteractor interactor)
        {

            base.OnSelectEntered(interactor);
        }

        protected override void OnSelectExited(XRBaseInteractor interactor)
        {
            base.OnSelectExited(interactor);
            ResetRigidbodyToDefault();
        }
    }
}
