using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour
{
    public Material handMat;
    public Renderer leftHand;
    public Renderer rightHand;
    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        //timer++;
       if (!leftHand) {
            leftHand = GameObject.Find("hand_left_renderPart_0").GetComponent<Renderer>();
        }

        if (!rightHand) {
            rightHand = GameObject.Find("hand_right_renderPart_0").GetComponent<Renderer>();
        }

        if (leftHand && leftHand.material != handMat) {
            leftHand.material = handMat;
        }

         if (rightHand && rightHand.material != handMat) {
            rightHand.material = handMat;
        }
    }
}
