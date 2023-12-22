using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    #region Private_Variables

    private Vector3 wantedPosition = default;
    private Vector3 lastPosition = default;


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
        transform.position = Vector3.MoveTowards(transform.position, transform.position + wantedPosition, Time.deltaTime * movementSpeed);

        PerformCircularRotation();
    }

    // private void FixedUpdate()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, transform.position + wantedPosition, Time.deltaTime * movementSpeed);
    //     //MyRigidBody.MovePosition(transform.position + wantedPosition * Time.deltaTime * movementSpeed);
    // }
    void PerformCircularRotation()
    {
        // where is our center on screen?
        Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
 
        // angle to previous finger
         float anglePrevious = Mathf.Atan2(center.x - lastPosition.x, lastPosition.y - center.y);
        //float anglePrevious = Mathf.Atan2(center.x - lastPosition.x, center.y - lastPosition.y);
    
        Vector3 currPosition = Input.mousePosition;
 
        // angle to current finger
         float angleNow = Mathf.Atan2(center.x - currPosition.x, currPosition.y - center.y);
        //float angleNow = Mathf.Atan2(center.x - currPosition.x, center.y - currPosition.y);
 
        lastPosition = currPosition;
 
        // how different are those angles?
        float angleDelta = (angleNow - anglePrevious);
        //Debug.Log("angle = " + angleDelta * Mathf.Rad2Deg);
 
        // rotate by that much
        transform.Rotate(new Vector3(0, -angleDelta * Mathf.Rad2Deg, 0));
    }
}
