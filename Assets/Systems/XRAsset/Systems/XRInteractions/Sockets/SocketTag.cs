using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketTag : XRSocketInteractor
{
    [TagSelector]
    [SerializeField] string targetTag = string.Empty;

    protected override void Awake()
    {
        if (interactionManager == null) interactionManager = FindObjectOfType<XRInteractionManager>();
        base.Awake();
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchTag(interactable);
    }


    private bool MatchTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }
}
