using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wasserstand : MonoBehaviour
{
    private bool ausBetrieb;
    private bool betriebStand;
    private Button startButton;
    private Button stopButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("WasserKWstart").GetComponent<Button>();
        stopButton = GameObject.Find("WasserKWstop").GetComponent<Button>();
        betriebStand = false;
        ausBetrieb = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ausBetrieb)
            startButton.interactable = false;
        else if (betriebStand)
            stopButton.interactable = false;
    }

    void OnTriggerEnter(Collision col)
    {
        if (col.gameObject.tag == "AusBetrieb")
        {
            ausBetrieb = true;
        }
        else if (col.gameObject.tag == "BetriebStand")
        {
            betriebStand = true;
        }
    }
}
