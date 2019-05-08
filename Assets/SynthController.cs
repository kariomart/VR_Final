using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using OVR;

public class SynthController : MonoBehaviour
{
    
    public bool left;
    public bool vrEnabled;
    public AudioSource source;
    public HelmController synth;
    public Transform hand;
    public Vector2 defaultPos;

    public int[] scale = {0, 2, 4, 5, 7, 9, 11 };
    public int octaveSize = 12;
    public int octaveSpan;

    public int numKeys = 60;
    public int startingKey = 24;

    public float rangeOfMovementX;
    public float rangeOfMovementY;

    public int noteMin;
    public int noteMax;

    GroundKey[] keys;
    int currentNote;

    Vector2 leftThumbstickVal;
    Vector2 rightThumbstickVal;


    void Start()
    {
        source = GetComponent<AudioSource>();
        synth = GetComponent<HelmController>();
        defaultPos = hand.position; 

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       //UpdateRangeOfMotion();
        float diffY = hand.position.y - defaultPos.y;
        diffY = Remap(diffY, -rangeOfMovementY, rangeOfMovementY, -10, 10);

        float diffX = hand.position.x - defaultPos.x;
        source.panStereo = getPan(diffX);

        //Debug.Log(noteMin);
        int tempNote = noteMin + Mathf.RoundToInt(diffY);
        Debug.Log(tempNote + "\n" + diffY);

        if (tempNote != currentNote) {
            ChordOff(currentNote);
            currentNote = tempNote;
            ChordOn(currentNote);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            synth.AllNotesOff();
            synth.NoteOn(60, 1f);
        }

        if (vrEnabled) {
            VRInput();
        }

        SetFilter();
        Debug.Log(leftThumbstickVal);

    }

    void NoteOn(int index)
    {
        if (synth) {
            synth.NoteOn(GetKeyFromIndex(index));
        }
        //keys[index].SetOn(true);
    }

    void NoteOff(int index)
    {
        if (synth) {
            synth.NoteOff(GetKeyFromIndex(48+index));
        }

    }

    void ChordOn(int index) {

        if (synth) {
            synth.NoteOn(index);
            synth.NoteOn(index+4);
            synth.NoteOn(index+7);
        }
    }

    void ChordOff(int index) {

        if (synth) {
            synth.NoteOff(index);
            synth.NoteOff(index+4);
            synth.NoteOff(index+7);
        }
    }

    int GetKeyFromIndex(int index)
    {
        int octave = index / scale.Length;
        int noteInScale = index % scale.Length;
        return startingKey + octave * octaveSize + scale[noteInScale];
    }

    // int GetNote(int index) {

    //     int note;
    //     note = minNote + scale[Random.Range(0,scale.Length)] + (12*Random.Range(0, octaveSpan));
    // }

    public float getPan(float val) {

        float pan =  Remap(val, -rangeOfMovementX, rangeOfMovementX, -.8f, .8f);
        pan = Mathf.Clamp(pan, -.8f, .8f);
        return pan;

    }
    public void SetFilter()
    {
        float val = hand.transform.position.x;
        val = Remap(val, -10f, 10f, 0, rangeOfMovementX);  
        val = Mathf.Clamp(val, 0, 1);
        //synth.SetParameterAtIndex(0, .5f+val);
        //synth.SetParameterAtIndex(1, 1-val);
        
        if (vrEnabled) {
            if (left) {
                synth.SetParameterAtIndex(3, leftThumbstickVal.x);
                synth.SetParameterAtIndex(4, leftThumbstickVal.y);
            } else {
                synth.SetParameterAtIndex(3, rightThumbstickVal.x);
                synth.SetParameterAtIndex(4, rightThumbstickVal.y);
            }
        }
    }

    public void SetParameter(int index, float val) {
        synth.SetParameterAtIndex(index, val);
    }

    public void UpdateRangeOfMotion() {

        if (hand.position.x > rangeOfMovementX) {
            rangeOfMovementX = hand.position.x;
        }

        if (hand.position.y > rangeOfMovementY) {
            rangeOfMovementY = hand.position.y;
        }


    }

    void VRInput() {

        if (left) {
            source.volume=1-OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
            //leftThumbstickVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            leftThumbstickVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.All);
        } else {
            source.volume=1-OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);  
            rightThumbstickVal = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        }

        if (left)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (synth.GetParameterAtIndex(5) == 1)
                {
                    synth.SetParameterAtIndex(5, 0);
                }
                else
                {
                    synth.SetParameterAtIndex(5, 1);
                }
            }
        } else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                if (synth.GetParameterAtIndex(5) == 1)
                {
                    synth.SetParameterAtIndex(5, 0);
                }
                else
                {
                    synth.SetParameterAtIndex(5, 1);
                }
            }

        }


    }

    public float Remap (float from, float fromMin, float fromMax, float toMin,  float toMax)
    {
        var fromAbs  =  from - fromMin;
        var fromMaxAbs = fromMax - fromMin;      
       
        var normal = fromAbs / fromMaxAbs;
 
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
 
        var to = toAbs + toMin;
       
        return to;
    }
}