using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Private_Variables

    private bool leftInput = false;
    private bool rightInput = false;
    private bool forwardInput = false;
    private bool backwardInput = false;
    private bool jumpInput = false;

    private Rigidbody myRigidBody = default;
    
    #endregion

    #region Exposed_Variables

    

    #endregion

    #region Getters

    public bool LeftInput => leftInput;
    public bool RightInput => rightInput;
    public bool ForwardInput => forwardInput;
    public bool BackwardInput => backwardInput;
    public bool JumpInput => jumpInput;
    public Rigidbody MyRigidBody => myRigidBody;

    #endregion

    public void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
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