using System;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public SpawnPointType _spawnPointType;
    private bool _isSpawning = false;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _isSpawning == false) {
            if (_spawnPointType == SpawnPointType.WallSpawn) 
            {
                Vector3 spawnPosition = new Vector3(transform.position.x+1f, transform.position.y, transform.position.z);
                EnemyManager.SpawnEnemy.Invoke(spawnPosition, transform.rotation * Quaternion.Euler(0, -90f, 0), _spawnPointType); 
            }
            else 
            {
                Vector3 spawnPosition = new Vector3(transform.position.x+0.5f, transform.position.y, transform.position.z);
                EnemyManager.SpawnEnemy.Invoke(spawnPosition, transform.rotation * Quaternion.Euler(0, 90f, 0), _spawnPointType); 
            }
            _isSpawning = true;
        }
    }

    public void DestroySpawnPoint()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
