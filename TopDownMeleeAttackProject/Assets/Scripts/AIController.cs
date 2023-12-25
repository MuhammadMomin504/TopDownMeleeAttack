using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MovementController
{
    #region Private_Variables

    private Vector3 myWantedPosition = default;
    private float remainingDistance = 0f;
    
    #endregion

    #region Exposed_Variables

    [SerializeField] private Transform target = default;
    [SerializeField] private float attackDistance = 1f;
    

    #endregion

    #region Getters

    

    #endregion
    
    private void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayAnimation(Constants.Animations.Idle);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        remainingDistance = CalculateDistance();
        if (remainingDistance < attackDistance)
        {
            Attack();
        }
        if(IsAttacking)
            return;
        
        base.Update();
        FindTarget();
        transform.position = Vector3.MoveTowards(transform.position, myWantedPosition, Time.deltaTime * CurrentMovementSpeed);
        SetRotation();

    }

    private void FindTarget()
    {
        //Find the vector from the source to the target and move the enemy in that direction
        myWantedPosition = target.position;
        UpdateWantedPosition(myWantedPosition);
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

    public override void Attack()
    {
        base.Attack();
    }
}
