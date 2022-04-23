using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform PlayerStartPoint;
    public Transform PlayerParentTransform;
    public GameObject PlayerPrefab;
    
    
    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        Inventory PlayerContainer = Container
            .InstantiatePrefabForComponent<Inventory>(PlayerPrefab, PlayerStartPoint.position, Quaternion.identity,
                PlayerParentTransform);

        Container
            .Bind<Inventory>()
            .FromInstance(PlayerContainer)
            .AsSingle()
            .NonLazy();
    }
}