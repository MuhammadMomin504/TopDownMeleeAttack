using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    #region Private_Variables

    private Vector3 wantedPosition = default;
   

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

        //Assign wanted position based on the user input.
        if (RightInput)
        {
            wantedPosition.x = 1f;
        }
        if (LeftInput)
        {
            wantedPosition.x = -1f;
        }
        if (ForwardInput)
        {
            wantedPosition.z = 1f;
        }
        if (BackwardInput)
        {
            wantedPosition.z = -1f;
        }
        
        if (!RightInput && !LeftInput && !ForwardInput && !BackwardInput)
        {
            wantedPosition = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        MyRigidBody.MovePosition(transform.position + wantedPosition * Time.deltaTime * movementSpeed);
    }
}
