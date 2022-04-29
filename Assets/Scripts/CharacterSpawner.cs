using UnityEngine;
using Zenject;

public class CharacterSpawner: MonoBehaviour
{
    private IEnemyFactory _characterFactory;
    private EnemyMarker[] _enemyMarkers;
    
    [Inject]
    private void Construct(IEnemyFactory enemyFactory, EnemyMarker[] enemyMarkers)
    {
        _characterFactory = enemyFactory;
        _enemyMarkers = enemyMarkers;
        _characterFactory.Load();
    }

    public void SpawnCharacters()
    {
        Debug.Log("Start spawn!");
        foreach (EnemyMarker enemyMarker in _enemyMarkers)
        {
            _characterFactory.Create(enemyMarker.EnemyType, enemyMarker.transform.position);
        }
    }
}