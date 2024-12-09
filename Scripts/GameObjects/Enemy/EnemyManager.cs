using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Manages enemy spawn and death
    private Hero _player;
    [SerializeField] private GameObject _enemyPrefab;
    private Enemy _enemy;
    public static Action<Vector3, Quaternion, SpawnPointType> SpawnEnemy;
    public static Action<int> EnemyChasing;
    public static Action<int> EnemyStopChasing;
    public static Action<int, Transform> EnemyDeath;
    private Dictionary<int, EnemyState> _enemyIDs = null;
    private enum EnemyState
    {
        Default,
        Chasing
    }

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Hero>();
    }
    void Start()
    {
        SpawnEnemy += OnSpawnEnemy;
        EnemyChasing += OnEnemyChasing;
        EnemyStopChasing += OnEnemyStopChasing;
    }

    private void OnSpawnEnemy(Vector3 position, Quaternion rotation, SpawnPointType spawnType)
    {
        if (_enemyIDs == null) 
        {
            _enemyIDs = new Dictionary<int, EnemyState>
            {
                {0, EnemyState.Default}
            };
            _enemy = Instantiate(_enemyPrefab, position, rotation, transform).GetComponent<Enemy>();
            _enemy.Init(_player, spawnType, 0);
        }
        else 
        {
            int enemyID = _enemyIDs.Keys.Last() + 1;
            _enemyIDs.Add(enemyID, EnemyState.Default);
            _enemy = Instantiate(_enemyPrefab, position, rotation, transform).GetComponent<Enemy>();
            _enemy.Init(_player, spawnType, enemyID);
        }
        
    }

    private void OnEnemyChasing(int enemyID)
    {
        if (!_enemyIDs.ContainsValue(EnemyState.Chasing)) 
        {
            AudioManager.PlayDanger.Invoke();
        }
        _enemyIDs[enemyID] = EnemyState.Chasing;
    }

    private void OnEnemyStopChasing(int enemyID)
    {
        _enemyIDs[enemyID] = EnemyState.Default;
        if (!_enemyIDs.ContainsValue(EnemyState.Chasing)) 
        {
            AudioManager.PlayAmbience.Invoke();
        }
    }

    private void OnDeathEnemy(Vector3 position)
    {
        //DropLoot(position)
    }
}
