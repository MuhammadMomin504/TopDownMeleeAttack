using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


public class MovementController : AnimationController
{
    #region Private_Variables
    
    private Rigidbody myRigidBody = default;
    private Vector3 lastPosition = default;
    private float currentMovementSpeed = 0f;
    private bool isWalkState = false;
    private bool isIdleState = false;
    private Vector3 wantedPosition = default;
    private bool isAttacking = false;
    private float yClampedPosition = 0f;
    private bool isDead = false;
    
    #endregion

    #region Exposed_Variables
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float walkAnimationSpeed = 1f;

    #endregion

    #region Getters

    protected Vector3 WantedPosition
    {
        get { return wantedPosition; }
    }

    public Vector3 LastPosition
    {
        get { return lastPosition;}
        set { lastPosition = value; }
    }

    public float CurrentMovementSpeed
    {
        get { return currentMovementSpeed; }
        set { currentMovementSpeed = value; }
    }

    public bool IsAttacking => isAttacking;
    public Rigidbody MyRigidBody => myRigidBody;

    public bool IsDead => isDead;
    
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    #endregion

    public void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        Init();
        yClampedPosition = transform.position.y;
    }

    protected override void Init()
    {
        base.Init();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if(isDead)
            return;
        
        base.Update();
        transform.position = new Vector3(transform.position.x, yClampedPosition, transform.position.z);
        if (wantedPosition != Vector3.zero && !isDead)
        {
            currentMovementSpeed = Mathf.MoveTowards(currentMovementSpeed, movementSpeed, Time.deltaTime * 30f);
        }
        else
        {
            currentMovementSpeed = 0f;
        }
        
        if (currentMovementSpeed >= 1f && !isWalkState)
        {
            SwitchToWalkAnimation();
            ChangeSpeedOfAnimation(Constants.Animations.Walk, walkAnimationSpeed);
            isWalkState = true;
            isIdleState = false;
            Debug.Log("Walk State");
        }
        else if (currentMovementSpeed < 1f && !isIdleState)
        {
            isWalkState = false;
            isIdleState = true;
            SwitchToIdleAnimation();
            Debug.Log("Idle State");
        }
        
    }

    public void UpdateWantedPosition(Vector3 wantedPosition)
    {
        this.wantedPosition = wantedPosition;
    }

    private void SwitchToWalkAnimation()
    {
        SwtichAnimation(Constants.Animations.Walk);

    }
    private void SwitchToIdleAnimation()
    {
        SwtichAnimation(Constants.Animations.Idle);

    }

    public override void SwtichAnimation(string animName)
    {
        base.SwtichAnimation(animName);
    }

    public virtual void Attack()
    {
        isAttacking = true;
        SwtichAnimation(Constants.Animations.MeleeAttack);
        StopCoroutine("PlayPreviousAnimationWhenCurrentAnimationCompletes");
        StartCoroutine("PlayPreviousAnimationWhenCurrentAnimationCompletes", GetAnimationLength(Constants.Animations.MeleeAttack));
    }

    public virtual void Death()
    {
        isDead = true;
        SwtichAnimation(Constants.Animations.Death);
        StartCoroutine("DisableCharacterAfterDeath");

    }

    public virtual void TakeHit(float damageAmount  = 0f)
    {
        if (!isDead)
        {
            SwtichAnimation(Constants.Animations.Hit);
            StopCoroutine("PlayPreviousAnimationWhenCurrentAnimationCompletes");
            StartCoroutine("PlayPreviousAnimationWhenCurrentAnimationCompletes", GetAnimationLength(Constants.Animations.Hit));
            
        }
    }
    private IEnumerator DisableCharacterAfterDeath()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Disable character");
        gameObject.SetActive(false);
        
    }
   

    private IEnumerator PlayPreviousAnimationWhenCurrentAnimationCompletes(float animationLength)
    {
        yield return new WaitForSeconds(animationLength);
        if (!isDead)
        {
            if(isIdleState)
                SwitchToIdleAnimation();
            else if (isWalkState)
                SwitchToWalkAnimation();

            isAttacking = false;
            
        }
    }
    
    
}
