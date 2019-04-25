using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class BPMController : MonoBehaviour
{

    public AudioHelmClock clock;
    public Vector2 defaultPos;
    int defaultBPM = 120;

    // Start is called before the first frame update
    void Start()
    {
     defaultPos = transform.position;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dif = transform.position.y - defaultPos.y;
        clock.bpm = 120 + dif;
    }
}
