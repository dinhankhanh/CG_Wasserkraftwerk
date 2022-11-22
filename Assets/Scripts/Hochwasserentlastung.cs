using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hochwasserentlastung : MonoBehaviour
{

    private Animator animator;
    private ParticleSystem ps1;
    private ParticleSystem ps2;
    private ParticleSystem ps3;
    private ParticleSystem ps4;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ps1 = GameObject.Find("ps1").GetComponent<ParticleSystem>();
        ps2 = GameObject.Find("ps2").GetComponent<ParticleSystem>();
        ps3 = GameObject.Find("ps3").GetComponent<ParticleSystem>();
        ps4 = GameObject.Find("ps4").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            if (AnimatorIsPlaying("WaterDrain"))
            {
                if (!ps1.isPlaying) { 
                    ps1.Play();
                }
                if (!ps2.isPlaying)
                {
                    ps2.Play();
                }
                if (!ps3.isPlaying)
                {
                    ps3.Play();
                }
                if (!ps4.isPlaying)
                {
                    ps4.Play();
                }
            }
            else 
            {
                if(ps1.isPlaying)
                    ps1.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                if (ps2.isPlaying)
                    ps2.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                if (ps3.isPlaying)
                    ps3.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                if (ps4.isPlaying)
                    ps4.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }          
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() &
            animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &
                !animator.IsInTransition(0);
    }

    bool AnimatorIsPlaying()
    { 
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
