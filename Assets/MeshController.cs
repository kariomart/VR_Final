using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public Mesh mesh;
    public Vector3 pt;
    public Vector3[] pts = new Vector3[1];

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        pts[0] = transform.position+SelectRandomMeshPoints.GetRandomPointOnSurface(mesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
