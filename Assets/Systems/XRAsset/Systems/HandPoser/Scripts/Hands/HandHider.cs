using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRDirectInteractor))]
public class HandHider : MonoBehaviour
{
    public GameObject handObject;

    private XRDirectInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(Hide);
        interactor.onSelectExited.AddListener(Show);
    }

    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(Hide);
        interactor.onSelectExited.RemoveListener(Show);
    }


    private void Show(XRBaseInteractable interactable)
    {
        handObject.SetActive(true);
    }

    private void Hide(XRBaseInteractable interactable)
    {
        PoseContainer poseContainer = interactable.GetComponent<PoseContainer>();
        if (poseContainer == null || poseContainer.pose == null)
            handObject.SetActive(false);
    }
}
