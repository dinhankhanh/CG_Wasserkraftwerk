using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hochwasserentlastung : MonoBehaviour
{

    private Animator animator;
    private ParticleSystem ps1;
    private ParticleSystem ps2;
    private ParticleSystem ps3;
    private ParticleSystem ps4;
    private ParticleSystem GA1;
    private ParticleSystem GA2;
    private ParticleSystem GA3;
    private ParticleSystem GA4;
    private ParticleSystem GA5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ps1 = GameObject.Find("ps1").GetComponent<ParticleSystem>();
        ps2 = GameObject.Find("ps2").GetComponent<ParticleSystem>();
        ps3 = GameObject.Find("ps3").GetComponent<ParticleSystem>();
        ps4 = GameObject.Find("ps4").GetComponent<ParticleSystem>();
        GA1 = GameObject.Find("GA1").GetComponent<ParticleSystem>();
        GA2 = GameObject.Find("GA2").GetComponent<ParticleSystem>();
        GA3 = GameObject.Find("GA3").GetComponent<ParticleSystem>();
        GA4 = GameObject.Find("GA4").GetComponent<ParticleSystem>();
        GA5 = GameObject.Find("GA5").GetComponent<ParticleSystem>();
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
            if (AnimatorIsPlaying("Empty"))
            {
                if (GA1.isPlaying)
                {
                    GA1.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                if (GA2.isPlaying)
                {
                    GA2.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                if (GA3.isPlaying)
                {
                    GA3.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                if (GA4.isPlaying)
                {
                    GA4.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                if (GA5.isPlaying)
                {
                    GA5.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
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

  
    
    public void OnCloseBtOutlet()
    {
       /* for (int i = 0; i < GA.Length; i++)
        {
            if (GA[i].GetComponent<ParticleSystem>().isPlaying)
            {
                GA[i].GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }*/
    }
}
