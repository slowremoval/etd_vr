using UnityEngine;
using Zenject;

public class SpawnerActivator : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    
    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    public void SpawnEnemies()
    {
        _enemySpawner.SpawnCharacters();
    }
}
