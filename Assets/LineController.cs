using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public int index;
    public MeshController meshController;
    public LineRenderer line;
    public Transform startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, startPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, meshController.pts[index]);
    }
}
