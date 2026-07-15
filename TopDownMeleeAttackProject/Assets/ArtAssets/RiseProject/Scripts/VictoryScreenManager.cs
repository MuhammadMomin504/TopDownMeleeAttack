using NUnit.Framework;
using UnityEngine;
using Spine;
using Spine.Unity;
using System.Collections.Generic;

public class VictoryScreenManager : MonoBehaviour
{
    [Header("Spine Settings")]
    [SerializeField] private SkeletonAnimation skeletonAnimation; // Use SkeletonGraphic if using UI
    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] 
    private const string spotLightEventName = "spotLightEvent";
    
    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] 
    private const string confettiEventName = "confettiEvent";
    
    [Header("Particle Settings")]
    private List<ParticleSystem> confettiParticles = default;
    [SerializeField] private GameObject confettiParent = default;
    
    [Header("Spot Light Settings")]
    [SerializeField] private GameObject spotLight = default;
    
    
    private Spine.EventData spotLightEventData;
    private Spine.EventData confettiEventData;

    void Awake()
    {
        if(spotLight != null)
            spotLight.SetActive(false);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (skeletonAnimation == null)
            skeletonAnimation = GetComponent<SkeletonAnimation>();

        if (confettiParent != null)
        {
            confettiParticles = new List<ParticleSystem>();

            Transform[] allChildren = GetComponentsInChildren<Transform>();
            
            for (int i = 0; i < allChildren.Length; i++)
            {
                if (allChildren[i].GetComponent<ParticleSystem>() != null)
                {
                    confettiParticles.Add(allChildren[i].GetComponent<ParticleSystem>());
                }
            }
        }

        if (skeletonAnimation != null)
        {
            // Initialize skeleton to ensure data is loaded
            skeletonAnimation.Initialize(false);
            
            // Cache the event data for better performance comparison
            if (skeletonAnimation.Skeleton != null)
            {
                spotLightEventData = skeletonAnimation.Skeleton.Data.FindEvent(spotLightEventName);
                confettiEventData = skeletonAnimation.Skeleton.Data.FindEvent(confettiEventName);
            }

            // Subscribe to the Spine event callback
            skeletonAnimation.AnimationState.Event += OnSpineEvent;
        }
    }
    
    private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
    {
        // 1. Check for SpotLight Event
        if (e.Data == spotLightEventData || e.Data.Name == spotLightEventName)
        {
            TriggerSpotlight();
        }
        
        // 2. Check for Confetti Event
        if (e.Data == confettiEventData || e.Data.Name == confettiEventName)
        {
            PlayConfetti();
        }
    }
    
    private void TriggerSpotlight()
    {
        Debug.Log("Spotlight Triggered!");
        if (spotLight != null)
            spotLight.SetActive(true);
    }


    public void PlayConfetti()
    {
        foreach (ParticleSystem confettiParticleSystem in confettiParticles)
            confettiParticleSystem.Play();
    }
    
    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        if (skeletonAnimation != null && skeletonAnimation.AnimationState != null)
        {
            skeletonAnimation.AnimationState.Event -= OnSpineEvent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayConfetti();
        }
    }
}
