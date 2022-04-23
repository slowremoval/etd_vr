using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

namespace Player
{
    public class HandInteractor : XRDirectInteractor
    {
        public Inventory _playerInventory;
        
        //     [Inject]
        // private void Construct(Inventory playerInventory)
        // {
        //     Debug.Log("HandInteractor exists");
        //     _playerInventory = playerInventory;
        // }
        
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            var interactableObject = args.interactableObject.transform.gameObject;
            Debug.Log($"_playerInventory is : {_playerInventory}!");
            Debug.Log($"Interactable object is {interactableObject.name}! Everything is right?");
            _playerInventory.Item = interactableObject.transform;
            Debug.Log($"Item in inventory is {_playerInventory.Item.name}");
        }
    }
}