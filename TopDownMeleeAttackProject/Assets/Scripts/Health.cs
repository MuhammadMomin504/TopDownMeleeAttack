using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Private_Variables

    private float currentHealth = 0f;
    private float lerpAmount = 0f;
    private float finalHealth = 0f;
    private bool shouldDeductHealth = false; 
    

    #endregion
    
    #region Exposed_Variables

    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private float currentLerpHealth = 0f;

    #endregion

    #region Getters

    public float TotalHealth => totalHealth;
    

    #endregion
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        currentHealth = totalHealth;
        currentLerpHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldDeductHealth)
        {
            currentLerpHealth = Mathf.MoveTowards(currentLerpHealth, finalHealth, Time.deltaTime * 10f);

            if (currentLerpHealth <= currentHealth)
            {
                currentLerpHealth = currentHealth;
                shouldDeductHealth = false;
            }
        }
    }

    public void DeductHealth(float deductAmount)
    {
        finalHealth = currentHealth - deductAmount;
        finalHealth = Mathf.Clamp(finalHealth, 0f, TotalHealth);
        currentHealth = finalHealth;
        shouldDeductHealth = true;
    }
    
    
    
}
