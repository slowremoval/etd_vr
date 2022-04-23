using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrapplingGunInteractable : XRGrabInteractable
{
    private GrapplingGun _grapplingGun;

    protected override void Awake()
    {
        base.Awake();
        _grapplingGun = GetComponent<GrapplingGun>();
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        GameObject interactorObject = args.interactorObject.transform.gameObject;

        if (interactorObject.TryGetComponent<HandControllerInput>(out HandControllerInput interactorController))
        {
            _grapplingGun.activeHandControllerInput = interactorController;
        }
        else
        {
            Debug.Log("HandControllerInput not found!");
        }
        _grapplingGun.StartGrapple();
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        GameObject interactorObject = args.interactorObject.transform.gameObject;

        if (interactorObject.TryGetComponent<HandControllerInput>(out HandControllerInput interactorController))
        {
            if (_grapplingGun.activeHandControllerInput != null
                & _grapplingGun.activeHandControllerInput.gameObject.name == interactorController.gameObject.name)
            {
                _grapplingGun.activeHandControllerInput = null;
            }
        }

        _grapplingGun.StopGrapple();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        _grapplingGun.StopGrapple();
    }
}