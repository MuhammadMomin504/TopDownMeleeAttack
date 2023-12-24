using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    #region Private_Variables

    private Vector3 wantedPosition = default;
    private float walkingTimer = 0f;
    private bool isWalkCycle = false;
    private bool shouldSwitchAnimation = false;
    private Vector3 lastMousePosition = default;

    #endregion
    
    #region Exposed_Variables

    [SerializeField] private float movementSpeed = 5f;
    

    #endregion
    
    #region Getters

    #endregion


    public Transform cube;

    private void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        lastMousePosition = Input.mousePosition;
        PlayAnimation(Constants.Animations.Idle);
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
      
        transform.position = Vector3.MoveTowards(transform.position, transform.position + wantedPosition, Time.deltaTime * CurrentMovementSpeed);
        
        RotateUsingMouse();

        walkingTimer += Time.deltaTime;

        if (wantedPosition != Vector3.zero)
        {
            CurrentMovementSpeed = Mathf.MoveTowards(CurrentMovementSpeed, movementSpeed, Time.deltaTime * 30f);
        }
        else
        {
            CurrentMovementSpeed = 0f;
        }
        
        
        

    }

    private void SwitchToWalkAnimation()
    {
        SwtichAnimation(Constants.Animations.Walk);

    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + wantedPosition, Time.deltaTime * movementSpeed);
        //MyRigidBody.MovePosition(transform.position + wantedPosition * Time.deltaTime * movementSpeed);

    }
    void RotateUsingMouse()
    {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 direction = mousePosition - transform.position;
        // float angle = Mathf.Atan2(direction.x, direction.y);
        // //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        // //transform.Rotate(new Vector3(0, angle * Mathf.Rad2Deg, 0));
        // transform.Rotate(new Vector3(0, (-angle * Mathf.Rad2Deg), 0));
        // where is our center on screen?
        Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
        
        // angle to previous finger
        float anglePrevious = Mathf.Atan2(center.x - lastMousePosition.x, lastMousePosition.y - center.y);
        //float anglePrevious = Mathf.Atan2(center.y - lastPosition.y, center.x - lastPosition.x);
        
        Vector3 currPosition = Input.mousePosition;
        
        // angle to current finger
        float angleNow = Mathf.Atan2(center.x - currPosition.x, currPosition.y - center.y);
        //float angleNow = Mathf.Atan2(center.y - currPosition.y, center.x - currPosition.x);
        
        lastMousePosition = currPosition;
        
        // how different are those angles?
        float angleDelta = (angleNow - anglePrevious);
        //float angleDelta = (anglePrevious - angleNow);
        //Debug.Log("angle = " + angleDelta * Mathf.Rad2Deg);
        
        transform.Rotate(new Vector3(0, (-angleDelta * Mathf.Rad2Deg), 0));


    }
}
