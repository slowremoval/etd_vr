using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private readonly DiContainer _diContainer;

    private const string _grayGolemName = "Stas`s Pelvis";
    private const string _whiteGolemName = "Stas`s Pelvis Variant";
    private const string _spiderName = "MegaStepan";
    private const string _smallSpiderName = "MiniMegaStepan";
    
    private Object _greyGolemPrefab;
    private Object _whiteGolemPrefab;
    private Object _spiderPrefab;
    private Object _smallSpiderPrefab;


    
    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Load()
    {
        _greyGolemPrefab = Resources.Load(_grayGolemName);
        _whiteGolemPrefab = Resources.Load(_whiteGolemName);
        _spiderPrefab = Resources.Load(_spiderName);
        _smallSpiderPrefab = Resources.Load(_smallSpiderName);
    }

    public void Create(EnemyType enemyType, Vector3 at)
    {
        switch (enemyType)
        {
            case EnemyType.GreyGolem:
                _diContainer.InstantiatePrefab(_greyGolemPrefab, at, Quaternion.identity * Quaternion.Euler(0, 180, 0), null);
                break;
            case EnemyType.WhiteGolem:
                _diContainer.InstantiatePrefab(_whiteGolemPrefab, at, Quaternion.identity * Quaternion.Euler(0, 180, 0), null);
                break;
            // case EnemyType.Spider:
            //     _diContainer.InstantiatePrefab(_spiderPrefab, at, Quaternion.identity, null);
            //     break;
            // case EnemyType.SmallSpider:
            //     _diContainer.InstantiatePrefab(_smallSpiderPrefab, at, Quaternion.identity, null);
                break;
            default: break;
        }
    }
}