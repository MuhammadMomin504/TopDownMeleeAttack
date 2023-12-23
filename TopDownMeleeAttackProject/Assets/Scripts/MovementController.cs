using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;


public class MovementController : MonoBehaviour
{
    #region Private_Variables

    private bool leftInput = false;
    private bool rightInput = false;
    private bool forwardInput = false;
    private bool backwardInput = false;
    private bool jumpInput = false;

    private Rigidbody myRigidBody = default;
    private Animation animationComponent = default;

    
    #endregion

    #region Exposed_Variables
    [SerializeField] private Transform character = default;
    [SerializeField] private AnimationClip[] animationClips = default;
    
    

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
        SetLegacyModeToAllAnimations();
        //SetAnimationLoopWrapMode(Constants.Animations.Idle);
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

    private void SetLegacyModeToAllAnimations()
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
            animationClips[i].legacy = true;
        }
    }

    private void SetAnimationLoopWrapMode(string animName)
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
            //if (animName == animationClips[i].ToString())
            {
                animationClips[i].wrapMode = WrapMode.Loop;
                Debug.Log("Setting");
            }
        }
    }

    public virtual void PlayAnimation(string animName)
    {
        animationComponent.Play(animName);
    }

    public virtual void StopAnimation()
    {
        animationComponent.Stop();
    }
    public virtual void SwtichAnimation(string animName)
    {
        animationComponent.CrossFade(animName);
    }
    
    
    
    
    
}
