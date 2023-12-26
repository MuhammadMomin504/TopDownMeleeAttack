using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager instance;

    #region Private_Variables

    private float enemySpawnTimeElapsed = 0f;
    private List<Transform> spawnedEnemies = new List<Transform>();

    #endregion
    
    #region Exposed_Variables

    [SerializeField] private PlayerController player;
    [SerializeField] private Transform[] enemySpawnPosition = default;
    [SerializeField] private Transform enemyPrefab = default;
    [SerializeField] private float enemySpawnTime = 3f;
    [SerializeField] private int totalEnemies = 3;

    #endregion


    #region Getters

    public Transform[] EnemySpawnPosition => enemySpawnPosition;
    public Transform Target => player.transform;
    
    #endregion
    

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine( "SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsDead)
        {
            enemySpawnTimeElapsed += Time.deltaTime;

            if (enemySpawnTimeElapsed > enemySpawnTime)
            {
                StartCoroutine( "SpawnEnemy");
                enemySpawnTimeElapsed = 0f;
            }
            
        }
    }

    private IEnumerator SpawnEnemy()
    {
        
        if (spawnedEnemies.Count < totalEnemies)
        {
            RemoveFromListIfEnemyIsDestroyed();
            for (int i = 0; i < totalEnemies; i++)
            {

                Transform enemy = Instantiate(enemyPrefab, enemySpawnPosition[Random.Range(0, enemySpawnPosition.Length)].position, Quaternion.identity);
                spawnedEnemies.Add(enemy);
                enemy.GetComponent<AIController>().MovementSpeed = Random.Range(2f, 5f);
                yield return new WaitForSeconds(2f);

            }
        }
        
    }

    private void RemoveFromListIfEnemyIsDestroyed()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].GetComponent<AIController>().IsDead)
                spawnedEnemies.Remove(spawnedEnemies[i]);

        }
    }


    public bool IsPlayerDead()
    {
        if (player.IsDead)
        {
            return true;
        }

        return false;
    }
    
}
