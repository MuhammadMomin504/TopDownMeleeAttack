using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MovementController
{
    #region Private_Variables

    private Vector3 myWantedPosition = default;
    
    #endregion

    #region Exposed_Variables

    [SerializeField] private Transform target = default;
    

    #endregion

    #region Getters

    

    #endregion
    
    private void Awake()
    {
        base.Awake();
        Debug.Log("Ai Awake is called");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayAnimation(Constants.Animations.Idle);
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        myWantedPosition = target.position;
        UpdateWantedPosition(myWantedPosition);
        transform.position = Vector3.MoveTowards(transform.position, myWantedPosition, Time.deltaTime * CurrentMovementSpeed);
        SetRotation();
    }

    private void FindTarget()
    {
        //Find the vector from the source to the target and move the enemy in that direction
    }

    private float CalculateDistance()
    {
        //Calculate the distance in float which gives the value for how much enemy is near to the target.
        return Vector3.Distance(target.position, transform.position);
    }
    
    private void SetRotation()
    {
        transform.LookAt(target);
    }
}
