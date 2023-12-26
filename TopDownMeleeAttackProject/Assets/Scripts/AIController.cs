using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MovementController
{
    #region Private_Variables

    private Vector3 myWantedPosition = default;
    private float remainingDistance = 0f;
    private Health healthController = default;
    private Transform target = default;
    #endregion

    #region Exposed_Variables

    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float damageAmount = 20f;

    #endregion

    #region Getters

    

    #endregion
    
    private void Awake()
    {
        base.Awake();
        healthController = GetComponent<Health>();
        healthController.Init();
        //yPosition = transform.position.y;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameplayManager.instance.Target;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead)
            return;
        
        base.Update();
        FindTarget();
        
        if (!GameplayManager.instance.IsPlayerDead())
        {
            remainingDistance = CalculateDistance();
            if (remainingDistance < attackDistance)
            {
                Attack();
            }
            if(IsAttacking)
                return;
            
        }
        else
        {
            if (CalculateDistance() < 3f)
            {
                CurrentMovementSpeed = 0f;
            }
            
        }
        
       
        transform.position = Vector3.MoveTowards(transform.position, myWantedPosition, Time.deltaTime * CurrentMovementSpeed);
        SetRotation();

    }

    private void FindTarget()
    {
        //Find the target and move the enemy in that direction
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
        ChangeSpeedOfAnimation(Constants.Animations.MeleeAttack, 0.5f);
        base.Attack();
    }
    
    public override void TakeHit(float damageAmount)
    {
        base.TakeHit(damageAmount);
        healthController.DeductHealth(damageAmount);
        if (healthController.CurrentHealth <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        base.Death();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7 && IsAttacking && !other.gameObject.GetComponent<PlayerController>().IsAttacking && !other.gameObject.GetComponent<PlayerController>().IsDead)
        {
            //Enemy's hand collided with player, if this is true, damage the player
            Debug.Log("Enemy attacked player = " + other.gameObject.name);
            other.gameObject.GetComponent<PlayerController>().TakeHit(damageAmount);

            if (other.gameObject.GetComponent<PlayerController>().IsDead)
            {
                Debug.Log("Reset target");
                ResetTarget();
            }
            
        }
    }

    private void ResetTarget()
    {
        //Target has been destroyed, need to assign new target but for now target is null so assign the center point in world as target.
        target = GameplayManager.instance.EnemySpawnPosition[Random.Range(0, GameplayManager.instance.EnemySpawnPosition.Length)];
        //SwtichAnimation(Constants.Animations.Walk);
        
    }
}
