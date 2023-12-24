using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Private_Variables

    private Animation animationComponent = default;
    [SerializeField] private Transform character = default;
    
    #endregion

    #region Exposed_Variables
    [SerializeField] private AnimationClip[] animationClips = default;
    #endregion

    #region Getters

    #endregion
    
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    protected virtual void Init()
    {
        animationComponent = character.GetComponent<Animation>();
        SetLegacyModeToAllAnimations();
        Debug.Log("Animation Controller Init Called");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SetLegacyModeToAllAnimations()
    {
        for (int i = 0; i < animationClips.Length; i++)
        {
            animationClips[i].legacy = true;
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

    public void ChangeSpeedOfAnimation(string animName, float speed = 1f)
    {
        animationComponent[animName].speed = speed;
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
}
