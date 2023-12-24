using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    private Rigidbody myRigidBody = default;
    private Vector3 lastPosition = default;
    private float currentMovementSpeed = 0f;

    
    
    #endregion

    #region Exposed_Variables
   

    #endregion

    #region Getters

    public bool LeftInput => leftInput;
    public bool RightInput => rightInput;
    public bool ForwardInput => forwardInput;
    public bool BackwardInput => backwardInput;
    public bool JumpInput => jumpInput;

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
    }

   
    
    
}
