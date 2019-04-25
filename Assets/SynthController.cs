using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class SynthController : MonoBehaviour
{
    // Start is called before the first frame update
    public HelmController synth;
    public Transform hand;
    public Vector2 defaultPos;
    public int[] scale = {0, 2, 4, 5, 7, 9, 11 };
    public int octaveSize = 12;

    public int numKeys = 60;
    public int startingKey = 24;
    public float rangeOfMovementX;
    public float rangeOfMovementY;

    public int noteMin;
    public int noteMax;

    GroundKey[] keys;
    int currentNote;

    void Start()
    {
        defaultPos = hand.position;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float diff = hand.position.y - defaultPos.y;
        diff = Remap(diff, -rangeOfMovementY, rangeOfMovementY, -10, 10);
        int tempNote = Mathf.RoundToInt(noteMin + diff);
        if (tempNote != currentNote) {
            currentNote = tempNote;
            NoteOn(currentNote);
        }

        SetFilter();

    }

    void NoteOn(int index)
    {
        if (synth) {
            synth.NoteOn(GetKeyFromIndex(index));
        }
        //keys[index].SetOn(true);
    }

    int GetKeyFromIndex(int index)
    {
        int octave = index / scale.Length;
        int noteInScale = index % scale.Length;
        return startingKey + octave * octaveSize + scale[noteInScale];
    }

    public void SetFilter()
    {
        float val = hand.transform.position.x;
        val = Remap(val, -10f, 10f, 0, rangeOfMovementX);  
        val = Mathf.Clamp(val, 0, 1);
        synth.SetParameterAtIndex(0, .5f+val);
        synth.SetParameterAtIndex(1, 1-val);
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