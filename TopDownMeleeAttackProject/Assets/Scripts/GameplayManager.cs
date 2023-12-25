using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager instance;

    #region Exposed_Variables

    [SerializeField] private PlayerController player;
    [SerializeField] private Transform enemySpawnPosition = default;
    

    #endregion


    #region Getters

    public Transform EnemySpawnPosition => enemySpawnPosition;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
