using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe
{
    public class EnemySpawner : MonoBehaviour, IReloadable
    {
        [SerializeField]
        private GameObject spawnedEnemy;

        [SerializeField]
        private GameObject prefab;

        private void Start()
        {
            if (spawnedEnemy)
            {
                transform.position = spawnedEnemy.transform.position;
                transform.rotation = spawnedEnemy.transform.rotation;
                spawnedEnemy.transform.parent = transform;
            }
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (spawnedEnemy) return;

            spawnedEnemy = Instantiate(prefab, transform.position, transform.rotation);
            spawnedEnemy.transform.SetParent(transform, true);
        }

        public void OnReload()
        {
            if (spawnedEnemy)
            {
                Destroy(spawnedEnemy);
                spawnedEnemy = null;
            }

            SpawnEnemy();
        }
    }
}
