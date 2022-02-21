using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    public float speed = 5f;
    public XRController controller;

    private Animator animator;
    private int startAnimationStateHash;

    private readonly Dictionary<Finger.FingerType, Finger> gripFingers = new Dictionary<Finger.FingerType, Finger>()
    {
        { Finger.FingerType.Middle, new Finger(Finger.FingerType.Middle)},
        { Finger.FingerType.Ring, new Finger(Finger.FingerType.Ring)},
        { Finger.FingerType.Pinky, new Finger(Finger.FingerType.Pinky)}
    };

    private readonly Dictionary<Finger.FingerType, Finger> pointFingers = new Dictionary<Finger.FingerType, Finger>()
    {
        { Finger.FingerType.Index, new Finger(Finger.FingerType.Index)},
        { Finger.FingerType.Thumb, new Finger(Finger.FingerType.Thumb)}
    };


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        startAnimationStateHash = animator.GetCurrentAnimatorStateInfo(-1).shortNameHash;
    }

    private void Update()
    {
        CheckGrip();
        CheckPointer();

        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        AnimateFinger(pointFingers);
        AnimateFinger(gripFingers);
    }

    public void SetActive(bool isActive)
    {
        if (!isActive)
        {
            animator.Rebind();
            animator.enabled = false;
            this.enabled = false;
        }
        else
        {
            animator.enabled = true;
            this.enabled = true;
        }
    }

    private void CheckGrip()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            SetFingerTargets(gripFingers, gripValue);
        }
    }

    private void CheckPointer()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float pointerValue))
        {
            SetFingerTarget(pointFingers[Finger.FingerType.Index], pointerValue);
        }

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton) && primaryButton)
        {
            SetFingerTarget(pointFingers[Finger.FingerType.Thumb], primaryButton ? 1 : 0);
        }
        else if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButton))
        {
            SetFingerTarget(pointFingers[Finger.FingerType.Thumb], secondaryButton ? 1 : 0);
        }
    }

    private void SetFingerTargets(Dictionary<Finger.FingerType, Finger> fingers, float value)
    {
        foreach (Finger finger in fingers.Values)
        {
            SetFingerTarget(finger, value);
        }
    }

    private void SetFingerTarget(Finger finger, float value)
    {
        finger.target = value;
    }

    private void SmoothFinger(Dictionary<Finger.FingerType, Finger> fingers)
    {
        float time = speed * Time.unscaledDeltaTime;
        foreach (Finger finger in fingers.Values)
        {
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(Dictionary<Finger.FingerType, Finger> fingers)
    {
        foreach (Finger finger in fingers.Values)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}