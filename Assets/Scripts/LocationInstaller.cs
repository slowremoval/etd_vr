using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LocationInstaller : MonoInstaller, IInitializable
{
    public Transform PlayerStartPoint;
    public Transform PlayerParentTransform;
    public GameObject PlayerPrefab;
    public EnemyMarker[] EnemyMarkers;

    public override void InstallBindings()
    {
        BindInstallerInterfaces();

        BindPlayer();

        BindEnemyFactory();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        Debug.Log("Enemy factory binded!");
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
        var enemyFactory = Container.Resolve<IEnemyFactory>();
        enemyFactory.Load();
        Debug.Log("Start spawning!");
        foreach (EnemyMarker enemyMarker  in EnemyMarkers)
        {
            enemyFactory.Create(enemyMarker.EnemyType, enemyMarker.transform.position);
        }
    }
}