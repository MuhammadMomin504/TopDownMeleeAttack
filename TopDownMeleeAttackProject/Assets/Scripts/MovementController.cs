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

    private bool leftInput = false;
    private bool rightInput = false;
    private bool forwardInput = false;
    private bool backwardInput = false;
    private bool jumpInput = false;
    private bool mouseInput = false;

    private Rigidbody myRigidBody = default;
    private Vector3 lastPosition = default;
    private float currentMovementSpeed = 0f;
    private bool isWalkState = false;
    private bool isIdleState = false;
    private Vector3 wantedPosition = default;
    
    
    #endregion

    #region Exposed_Variables
    [SerializeField] private float movementSpeed = 5f;

    #endregion

    #region Getters

    protected bool LeftInput => leftInput;
    protected bool RightInput => rightInput;
    protected bool ForwardInput => forwardInput;
    protected bool BackwardInput => backwardInput;
    protected bool MouseInput => mouseInput;
    public bool JumpInput => jumpInput;

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
    public Rigidbody MyRigidBody => myRigidBody;

    #endregion

    public void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        Init();
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
        leftInput = InputManager.left;
        rightInput = InputManager.right;
        forwardInput = InputManager.forward;
        backwardInput = InputManager.backward;
        mouseInput = InputManager.mouseClicked;

        if (wantedPosition != Vector3.zero)
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
            ChangeSpeedOfAnimation(Constants.Animations.Walk, 3.5f);
            isWalkState = true;
            isIdleState = false;
        }
        else if (currentMovementSpeed < 1f && !isIdleState)
        {
            isWalkState = false;
            isIdleState = true;
            SwitchToIdleAnimation();
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

    public virtual void Attack()
    {
        
    }
    
    
}
