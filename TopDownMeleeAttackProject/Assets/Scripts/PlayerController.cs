using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    #region Private_Variables

    private Vector3 inputVector = default;
    private float rightFloat = 0f;
    private float forwardFloat = 0f;
    
   

    #endregion
    
    #region Exposed_Variables

    [SerializeField] private float movementSpeed = 5f;
    

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
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (RightInput)
        {
            rightFloat = 1f;
            //inputVector = new Vector3(1f, 0f, 0f);
        }
        if (LeftInput)
        {
            rightFloat = -1f;
            //inputVector = new Vector3(-1f, 0f, 0f);
        }
        if (ForwardInput)
        {
            forwardFloat = 1f;
            //inputVector = new Vector3(0f, 0f, 1f);
        }
        if (BackwardInput)
        {
            forwardFloat = -1f;
            //inputVector = new Vector3(0f, 0f, -1f);
        }

        
        if (!RightInput && !LeftInput && !ForwardInput && !BackwardInput)
        {
            rightFloat = 0f;
            forwardFloat = 0f;
        }
        inputVector = new Vector3(rightFloat, 0f , forwardFloat);
        //Debug.Log("Input vector = " + inputVector);

        
        
    }

    private void FixedUpdate()
    {
        MyRigidBody.MovePosition(transform.position + inputVector * Time.deltaTime * movementSpeed);
    }
}
