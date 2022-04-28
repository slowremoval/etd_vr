using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class HandInteractor : XRDirectInteractor
    {
        [SerializeField] private Inventory _playerInventory;

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            
            var interactableObject = args.interactableObject.transform.gameObject;
            
            _playerInventory.Item = interactableObject.transform;
        }
    }
}