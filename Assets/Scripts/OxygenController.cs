using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenController : MonoBehaviour
{
    public Text indicator;

    public float startingOxyLevel = 50;
    public float maxOxyLevel = 50;
    public float oxyPerMinute = 5;
    public float oxyAlertThreshold = 0.1f;

    public AudioSource alertSFXSource;
    public AudioSource alertVoiceSource;

    private bool indicatorFlashing = false;
    private bool alertPlaying = false;


    // allow OxyLevel to be read but not written by other scripts
    public float OxyLevel { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        OxyLevel = startingOxyLevel;
        InvokeRepeating("DoFlashIfSet", 0, 0.333333f);
    }

    void DoFlashIfSet()
    {
        if (indicatorFlashing)
        {
            if (indicator.color == Color.white)
            {
                indicator.color = Color.red;
            }
            else
            {
                indicator.color = Color.white;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("oxyContainer")) {
            O2Container container = other.gameObject.GetComponent("O2Container") as O2Container;
            OxyLevel += container.consume(maxOxyLevel - OxyLevel);
            Debug.Log("consumed!");
        }
    }

    void FixedUpdate()
    {
        OxyLevel -= (oxyPerMinute / 60) * Time.fixedDeltaTime;
        indicator.text = OxyLevel.ToString("000");
    }

    void Update()
    {
        if (OxyLevel < startingOxyLevel * oxyAlertThreshold)
        {
            indicatorFlashing = true;
            if (!alertPlaying)
            {
                alertPlaying = true;
                alertSFXSource.Play();
                Debug.Log("Playing");
                //alertVoiceSource.Play();
            }
        }
        else
        {
            indicatorFlashing = false;
            alertSFXSource.Stop();
            alertPlaying = false;
        }
    }
}
