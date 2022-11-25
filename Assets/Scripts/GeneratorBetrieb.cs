using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class GeneratorBetrieb : MonoBehaviour
{

    private Animator gene_animator;
    private Animator gate_animator;
    private GameObject gate;
    private Transform water;
    private UnityEngine.UI.Button startButton;
    private Button stopButton;
    private UnityEngine.UI.Button GA_start_button;
    private ParticleSystem damPipe;
    private ParticleSystem turbine;
    private RawImage GeneImg;
    private RawImage TurbineImg;
    private RawImage DampPipeImg;
    private Transform wasserStand;
    private Transform GA_stand;
    private Transform betriebStand;
    private bool geneStarted;
    private bool GAstarted;
    private bool isBetriebFaehig;
    private bool isGAFaehig;
    private ParticleSystem ps1;
    private ParticleSystem ps2;
    private ParticleSystem ps3;
    private ParticleSystem ps4;
    private ParticleSystem GA1;
    private ParticleSystem GA2;
    private ParticleSystem GA3;
    private ParticleSystem GA4;
    private ParticleSystem GA5;
    private Animator waterAnim;
    private GameObject waterLake;
    Vector3 scaleChange;
    Vector3 waterLakeSize;
    Vector3 waterLakeScale;
        Vector3 waterLakeGroundColSize;
    // Start is called before the first frame update
    private Vector3 scale;
    void Start()
    {
        waterLakeSize= waterLake.GetComponent<Renderer>().bounds.size;
        waterLakeScale = GameObject.Find("WaterLake").GetComponent<Transform>().localScale;
       
        water = GameObject.Find("Wasser").GetComponent<Transform>();
        waterAnim = GameObject.Find("Wasser").GetComponent<Animator>();
        startButton = GameObject.Find("WasserKWstart").GetComponent<Button>();
        stopButton = GameObject.Find("WasserKWStop").GetComponent<Button>();
        GA_start_button = GameObject.Find("GAstart").GetComponent<Button>();
        GA_stand = GameObject.Find("GA_Stand").GetComponent<Transform>();
        wasserStand = GameObject.Find("WasserStand").GetComponent<Transform>();
        betriebStand = GameObject.Find("BetriebStand").GetComponent<Transform>();
        gene_animator = GameObject.Find("generator").GetComponent<Animator>();
        gate = GameObject.Find("DamPipeGate");
        gate_animator = gate.GetComponent<Animator>();
        damPipe = GameObject.Find("PS_DampPipe").GetComponent<ParticleSystem>();
        turbine = GameObject.Find("ps_turbine").GetComponent<ParticleSystem>();
        GeneImg = GameObject.Find("GeneImg").GetComponent<RawImage>();
        TurbineImg = GameObject.Find("TurbineImg").GetComponent<RawImage>();
        DampPipeImg = GameObject.Find("DaPiImg").GetComponent<RawImage>();
        DisableImages();
        stopButton.interactable = false;
        startButton.interactable = false;
        GA_start_button.interactable = false;
        ps1 = GameObject.Find("ps1").GetComponent<ParticleSystem>();
        ps2 = GameObject.Find("ps2").GetComponent<ParticleSystem>();
        ps3 = GameObject.Find("ps3").GetComponent<ParticleSystem>();
        ps4 = GameObject.Find("ps4").GetComponent<ParticleSystem>();
        GA1 = GameObject.Find("GA1").GetComponent<ParticleSystem>();
        GA2 = GameObject.Find("GA2").GetComponent<ParticleSystem>();
        GA3 = GameObject.Find("GA3").GetComponent<ParticleSystem>();
        GA4 = GameObject.Find("GA4").GetComponent<ParticleSystem>();
        GA5 = GameObject.Find("GA5").GetComponent<ParticleSystem>();
        geneStarted = false;
        scale = new Vector3(3, 3, 3);
        stopButton.gameObject.SetActive(true);

        startButton.onClick.AddListener(OnOpen);
        stopButton.onClick.AddListener(OnClose);
        GA_start_button.onClick.AddListener(OnOpenBtOutlet);
    }

    // Update is called once per frame
    void Update()
    {

// waterLakeScale += scale;
  //      PumpWater();
        if (isBetriebFaehig & !geneStarted)
        {
            startButton.interactable = true;
        }
        if (geneStarted)
        {
            startButton.interactable = false;
            stopButton.interactable = true;
        }
        else { 
            stopButton.interactable = false;
        }

        if (!isBetriebFaehig)
            startButton.interactable = false;

        if (isGAFaehig & !GAstarted)
            GA_start_button.interactable = true;
        else
            GA_start_button.interactable = false;

        if (GAstarted)
        {
            GA_start_button.interactable = false;
        }
        else
        if (!GAstarted)
            GA_start_button.interactable = true;

        if (wasserStand.position.y < betriebStand.position.y)
        {
            isBetriebFaehig = false;
        }
        else
            isBetriebFaehig = true;
        if (wasserStand.position.y < GA_stand.position.y)
        {
            isGAFaehig = false;
        }
        else
        {
            isGAFaehig = true;
        }

        if (waterAnim!=null)
        {
            if (AnimatorIsPlaying(waterAnim,"WaterDrain"))
            {
                PlayPS(ps1, ps2, ps3, ps4);
            }
            else
            {
                StopPS(ps1, ps2, ps3, ps4);
            }
            if(!AnimatorIsPlaying(waterAnim, "EmptyLakeTr")) { 
                GAstarted = false;
                StopPS(GA1, GA2, GA3, GA4);
                if (GA5.isPlaying)
                    GA5.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
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
        ExecuteTrigger(gene_animator, "GeneratorRotation");
        if (!damPipe.isPlaying)
            damPipe.Play();
        if (!turbine.isPlaying)
            turbine.Play();
        EnableImages();
        geneStarted = true;
    }

    public void OnClose()
    {
        ExecuteTrigger(gate_animator, "GateClose");
        ExecuteTrigger(gene_animator, "Idle");
        if (damPipe.isPlaying)
            damPipe.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        if (turbine.isPlaying)
            turbine.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        DisableImages();
        geneStarted = false;
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

    public void OnOpenBtOutlet()
    {
        waterAnim.SetTrigger("EmptyLakeTr");
        PlayPS(GA1, GA2, GA3, GA4);
        if (!GA5.isPlaying)
            GA5.Play();
        GAstarted = true;
    }

    public void PlayPS(ParticleSystem ps1, ParticleSystem ps2, ParticleSystem ps3, ParticleSystem ps4)
    {
        if (!ps1.isPlaying)
        {
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

    public void StopPS(ParticleSystem ps1, ParticleSystem ps2, ParticleSystem ps3, ParticleSystem ps4)
    {
        if (ps1.isPlaying)
        {
            ps1.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (ps2.isPlaying)
        {
            ps2.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (ps3.isPlaying)
        {
            ps3.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (ps4.isPlaying)
        {
            ps4.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
