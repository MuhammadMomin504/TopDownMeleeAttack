using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    #region Private_Variables

    private bool leftInput = false;
    private bool rightInput = false;
    private bool forwardInput = false;
    private bool backwardInput = false;
    private bool jumpInput = false;
    private bool mouseInput = false;
    
    private Vector3 lastRotationVector = default;
    private Vector3 lastMousePosition = default;
    private Quaternion targetRotation = default;
    private Vector3 myWantedPosition = default;

    #endregion
    
    #region Exposed_Variables
    

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
        lastMousePosition = Input.mousePosition;
        PlayAnimation(Constants.Animations.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        leftInput = InputManager.left;
        rightInput = InputManager.right;
        forwardInput = InputManager.forward;
        backwardInput = InputManager.backward;
        mouseInput = InputManager.mouseClicked;
        
        base.Update();

        //Assign wanted position based on the user input.
        if (rightInput)
        {
            myWantedPosition.x = 1f;
            UpdateWantedPosition(myWantedPosition);
        }
        if (leftInput)
        {
            myWantedPosition.x = -1f;
            UpdateWantedPosition(myWantedPosition);
        }
        if (forwardInput)
        {
            myWantedPosition.z = 1f;
            UpdateWantedPosition(myWantedPosition);
        }
        if (backwardInput)
        {
            myWantedPosition.z = -1f;
            UpdateWantedPosition(myWantedPosition);
        }

        if (mouseInput)
        {
            Debug.Log("Mouse clicked");
            Attack();
        }
        
        if (!rightInput && !leftInput && !forwardInput && !backwardInput)
        {
            myWantedPosition = Vector3.zero;
            UpdateWantedPosition(myWantedPosition);
        }
        else
        {
            lastRotationVector = myWantedPosition;
        }
        
      
        transform.position = Vector3.MoveTowards(transform.position, transform.position + myWantedPosition, Time.deltaTime * CurrentMovementSpeed);
        SetRotation();
        //RotateUsingMouse();

        

    }

    private void SetRotation()
    {
        if(lastRotationVector != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lastRotationVector);
    }

   

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + wantedPosition, Time.deltaTime * movementSpeed);
        //MyRigidBody.MovePosition(transform.position + wantedPosition * Time.deltaTime * movementSpeed);

    }

    public override void Attack()
    {
        base.Attack();
        
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
        
        transform.Rotate(  new Vector3(0, (-angleDelta * Mathf.Rad2Deg), 0));


    }
}
