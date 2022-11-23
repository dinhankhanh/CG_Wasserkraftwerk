using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorBetrieb : MonoBehaviour
{

    private Animator gene_animator;
    private Animator gate_animator;
    private GameObject gate;
    private Button startButton;
    private Button stopButton;
    private ParticleSystem damPipe;
    private ParticleSystem turbine;
    private RawImage GeneImg;
    private RawImage TurbineImg;
    private RawImage DampPipeImg;

    /*private Camera damPipeCam;
    private Camera turbineCam;
    private Camera geneCam;
*/
    // Start is called before the first frame update
    void Start()
    {
        gene_animator = GetComponent<Animator>();
        gate = GameObject.Find("DamPipeGate");
        gate_animator = gate.GetComponent<Animator>();
        startButton = GameObject.Find("WasserKWstart").GetComponent<Button>();
        stopButton = GameObject.Find("WasserKWstop").GetComponent<Button>();
        damPipe = GameObject.Find("PS_DampPipe").GetComponent<ParticleSystem>();
        turbine = GameObject.Find("ps_turbine").GetComponent<ParticleSystem>();
        GeneImg = GameObject.Find("GeneImg").GetComponent<RawImage>();
        TurbineImg = GameObject.Find("TurbineImg").GetComponent<RawImage>();
        DampPipeImg = GameObject.Find("DaPiImg").GetComponent<RawImage>();
        DisableImages();
        stopButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (gate_animator != null)
        {
            if (AnimatorIsPlaying(gate_animator,"Open"))
            {
                if (!damPipe.isPlaying)
                    damPipe.Play();
                if(!turbine.isPlaying)
                    turbine.Play();
                startButton.interactable = false;
            }
            else if (AnimatorIsPlaying(gate_animator, "GateClose"))
            {
                if (damPipe.isPlaying)
                    damPipe.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                if (turbine.isPlaying)
                    turbine.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                stopButton.interactable = false;
            }
            else
            {
                if(!startButton.interactable)
                startButton.interactable = true;
                if(!stopButton.interactable)
                stopButton.interactable = true;
            }
        }*/
    }

    private void ExecuteTrigger(Animator anim, string trigger)
    {
        if (anim != null)
        {
            anim.SetTrigger(trigger);
        }
    }

    public void OnOpen()
    {
        ExecuteTrigger(gate_animator, "GateOpen");
        ExecuteTrigger(gene_animator, "StartGenerator");
        if (!damPipe.isPlaying)
            damPipe.Play();
        if (!turbine.isPlaying)
            turbine.Play();
        startButton.interactable = false;
        stopButton.interactable = true;
        EnableImages();
    }

    public void OnClose()
    {
        ExecuteTrigger(gate_animator, "GateClose");
        ExecuteTrigger(gene_animator, "StopGenerator");
        if (damPipe.isPlaying)
            damPipe.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        if (turbine.isPlaying)
            turbine.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        startButton.interactable = true;
        DisableImages();
    }

    bool AnimatorIsPlaying(Animator anim, string stateName)
    {
        return AnimatorIsPlaying(anim) &
            anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &
                !anim.IsInTransition(0);
    }

    bool AnimatorIsPlaying(Animator anim)
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void DisableImages()
    {
        if (GeneImg.enabled)
            GeneImg.enabled = false;
        if (TurbineImg.enabled)
            TurbineImg.enabled = false;
        if (DampPipeImg.enabled)
            DampPipeImg.enabled = false;
    }

    void EnableImages()
    {
        if (!GeneImg.enabled)
            GeneImg.enabled = true;
        if (!TurbineImg.enabled)
            TurbineImg.enabled = true;
        if (!DampPipeImg.enabled)
            DampPipeImg.enabled = true;
    }
}
