using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace pl.EpicVR.Interaction
{
    public class HandButton : XRBaseInteractable
    {
        public UnityEvent OnPress = null;
        public new XRInteractionManager interactionManager;
        public new LayerMask interactionLayerMask;

        private float yMin = 0;
        private float yMax = 0;
        private bool previousPress = false;

        private float previousHandHeight = 0;
        private XRBaseInteractor hoverInteractor = null;

        protected override void Awake()
        {
            base.Awake();
            base.interactionManager = interactionManager;
            base.interactionLayerMask = interactionLayerMask;

            onHoverEntered.AddListener(StartPress);
            onHoverExited.AddListener(EndPress);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onHoverEntered.RemoveListener(StartPress);
            onHoverExited.RemoveListener(EndPress);
        }

        private void Start()
        {
            SetMinMax();
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (hoverInteractor)
            {
                float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
                float handDifference = previousHandHeight - newHandHeight;
                previousHandHeight = newHandHeight;

                float newPosition = transform.localPosition.y - handDifference;
                SetYPosition(newPosition);

                CheckPress();
            }
        }


        private float GetLocalYPosition(Vector3 position)
        {
            Vector3 localPosition = transform.root.InverseTransformPoint(position);
            return localPosition.y;
        }

        private bool InPosition()
        {
            float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + .01f);
            return transform.localPosition.y == inRange;
        }

        private void StartPress(XRBaseInteractor interactor)
        {
            hoverInteractor = interactor;
            previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
        }

        private void EndPress(XRBaseInteractor interactor)
        {
            hoverInteractor = null;
            previousHandHeight = 0;

            previousPress = false;
            SetYPosition(yMax);
        }

        private void SetMinMax()
        {
            Collider collider = GetComponent<Collider>();
            yMin = transform.localPosition.y - (collider.bounds.size.y * .5f);
            yMax = transform.localPosition.y;
        }

        private void SetYPosition(float position)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Clamp(position, yMin, yMax);
            transform.localPosition = newPosition;
        }

        private void CheckPress()
        {
            bool inPosition = InPosition();

            if (inPosition && inPosition != previousPress)
            {
                Debug.Log($"@HandButton pressed ({gameObject.name})");
                OnPress.Invoke();
            }

            previousPress = inPosition;
        }
    }
}