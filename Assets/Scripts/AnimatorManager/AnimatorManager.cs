using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEATH
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {
        foreach (var anim in animatorSetups)
        {
            if (anim.type == type)
            {
                animator.SetTrigger(anim.trigger);
                animator.speed = anim.speed * currentSpeedFactor;
                break;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.RUN);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.DEATH);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.IDLE);
        }

      }
    }


            [System.Serializable]

    public class AnimatorSetup
    {
        public AnimatorManager.AnimationType type;
        public string trigger;
        public float speed = 1f;
   }

