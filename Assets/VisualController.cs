using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour
{
    public Material handMat;
    int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        timer++;
       if (timer == 200) {
            GameObject.Find("hand_left_renderPart_0").GetComponent<Renderer>().material = handMat;
            GameObject.Find("hand_right_renderPart_0").GetComponent<Renderer>().material = handMat;
        }
    }
}
