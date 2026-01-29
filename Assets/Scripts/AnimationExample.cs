using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation anim;

    public AnimationClip run;
    public AnimationClip Idle;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(run);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PlayAnimation(Idle);
        }
    }

    private void PlayAnimation(AnimationClip c)
    {
        //Animation.clip = c;
        anim.CrossFade(c.name);
    }
}
