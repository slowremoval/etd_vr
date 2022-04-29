using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LocationInstaller : MonoInstaller, IInitializable
{
    public Transform PlayerStartPoint;
    public Transform PlayerParentTransform;
    public GameObject PlayerPrefab;
    public GameObject CharacterSpawnerPrefab;
    public EnemyMarker[] EnemyMarkers;
    public NPCMarker[] NPCMarkers;
    public ItemMarker[] ItemMarkers;

    public override void InstallBindings()
    {
        BindInstallerInterfaces();

        BindEnemyFactory();

        BindEnemySpawner();
        BindPlayer();
    }
    
    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        Container.Bind<EnemyMarker[]>().FromInstance(EnemyMarkers).AsSingle().Lazy();
    }
    
    //todo : this!
    // private void BindItemFactory()
    // {
    //     Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
    // }
    //
    // private void BindNPCFactory()
    // {
    //     Container.Bind<INPCFactory>().To<INPCFactory>().AsSingle();;
    // }

    private void BindEnemySpawner()
    {
        EnemySpawner enemySpawner = Container
            .InstantiatePrefabForComponent<EnemySpawner>(CharacterSpawnerPrefab, Vector3.zero, Quaternion.identity,
                null);

        Container
            .Bind<EnemySpawner>()
            .FromInstance(enemySpawner)
            .AsSingle()
            .NonLazy();
    }
    
    private void BindInstallerInterfaces()
    {
        Container
            .BindInterfacesTo<LocationInstaller>()
            .FromInstance(this)
            .AsSingle();
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

    public void Initialize()
    {
        var enemySpawner = Container.Resolve<EnemySpawner>();
        enemySpawner.SpawnCharacters();
    }
    //todo : this!
    // private void SpawnItems()
    // {
    //     var itemFactory = Container.Resolve<IItemFactory>();
    //     itemFactory.Load();
    //
    //     foreach (ItemMarker itemMarker in ItemMarkers)
    //     {
    //         itemFactory.Create(itemMarker.ItemType, itemMarker.transform.position);
    //     }
    // }
}