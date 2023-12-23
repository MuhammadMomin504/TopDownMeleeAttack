using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public Animation animationComponent = default;
    
    #endregion

    #region Exposed_Variables
    [SerializeField] private Transform character = default;
    
    

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
        animationComponent = character.GetComponent<Animation>();
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

    public virtual void PlayAnimation(string animName)
    {
        animationComponent.Play(animName);
    }

    public virtual void StopAnimation()
    {
        
    }
    
    
    
    
}
