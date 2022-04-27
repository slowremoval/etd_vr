using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class HandInteractor : XRDirectInteractor
    {
        public Inventory _playerInventory;

        public bool IsRightHand;
        
        /*
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            var interactableObject = args.interactableObject.transform.gameObject;
            // Debug.Log($"_playerInventory is : {_playerInventory}!");
            // Debug.Log($"Interactable object is {interactableObject.name}! Everything is right?");
            _playerInventory.Item = interactableObject.transform;
            // Debug.Log($"Item in inventory is {_playerInventory.Item.name}");
        }
        */

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            var interactableObject = args.interactableObject.transform.gameObject;
            // Debug.Log($"_playerInventory is : {_playerInventory}!");
            // Debug.Log($"Interactable object is {interactableObject.name}! Everything is right?");
            _playerInventory.Item = interactableObject.transform;
            // Debug.Log($"Item in inventory is {_playerInventory.Item.name}");
        }
    }
}