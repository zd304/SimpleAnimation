using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationTransitions : MonoBehaviour {
    enum AnimationType
    {
        Legacy,
        SimplePlayable,
        StateMachine
    }
    AnimationType animationType;
    SimpleAnimation simpleAnimation;
    public AnimationClip clip;
	// Use this for initialization
	IEnumerator Start ()
    {
        var animationComponent = GetComponent<Animation>(); 
        var animatorComponent = GetComponent<Animator>();
        var simpleAnimationComponent = GetComponent<SimpleAnimation>();
        if (animationComponent)
            animationType = AnimationType.Legacy;
        else if (simpleAnimationComponent)
            animationType = AnimationType.SimplePlayable;
        else
            animationType = AnimationType.StateMachine;
        

        switch (animationType)
        {
            case AnimationType.Legacy:
                animationComponent.AddClip(clip, "A");
                animationComponent.AddClip(clip, "B");
                break;
            case AnimationType.SimplePlayable:
                simpleAnimationComponent.AddClip(clip, "A");
                simpleAnimationComponent.AddClip(clip, "B");
                simpleAnimationComponent.GetState("B").layer = 1;
                simpleAnimationComponent.AddClip(clip, "C");
                simpleAnimationComponent.GetState("C").layer = 1;
                //simpleAnimationComponent.AddClip(clip, "D");
                break;
            case AnimationType.StateMachine:
                break;
            default:
                break;
        }

        switch (animationType)
        {
            case AnimationType.Legacy:
                animationComponent.Play("A");
                break;
            case AnimationType.SimplePlayable:
                //simpleAnimationComponent.Play("D");
                break;
            case AnimationType.StateMachine:
                animatorComponent.Play("A");
                break;
            default:
                break;
        }

        while (true)
        {
            switch (animationType)
            {
                case AnimationType.Legacy:
                    animationComponent.Play("A");
                    break;
                case AnimationType.SimplePlayable:
                    simpleAnimationComponent.CrossFade("B", 0.5f);
                    break;
                case AnimationType.StateMachine:
                    animatorComponent.Play("A");
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(2.0f);

            switch (animationType)
            {
                case AnimationType.Legacy:
                    animationComponent.Play("B");
                    break;
                case AnimationType.SimplePlayable:
                    simpleAnimationComponent.CrossFade("C", 0.5f);
                    break;
                case AnimationType.StateMachine:
                    animatorComponent.Play("B");
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(2.0f);
        }
	}

   
}
