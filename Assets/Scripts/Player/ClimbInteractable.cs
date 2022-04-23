using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GameObject interactorObject = args.interactorObject.transform.gameObject;
        
        if (interactorObject.TryGetComponent<HandControllerInput>(out HandControllerInput interactorController))
        {
            Climber.CurrentHandControllerInput = interactorController;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        GameObject interactorObject = args.interactorObject.transform.gameObject;
        
        if (interactorObject.TryGetComponent<HandControllerInput>(out HandControllerInput interactorController))
        {
            if (Climber.CurrentHandControllerInput != null
                && Climber.CurrentHandControllerInput.gameObject.name == interactorController.gameObject.name)
            {
                Climber.CurrentHandControllerInput = null;
            }
        }
    }
}