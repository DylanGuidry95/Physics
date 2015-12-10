using UnityEngine;
using System.Collections;

public class AeroDynamics : MonoBehaviour
{
    public Node p1, p2, p3;     //Three node the triangle connects too.
    public Vector3 a;           //cross sectional area of the object

    void Start()
    {
        //triangle = gameObject.GetComponent<LineRenderer>();
    }

    //sets the three points of the triangle based on the values passed in through the arguments.
    //Called by the SpawnTriangles() in ClothSimulation
    public void CreateTriangle(Node a, Node b, Node c)
    {
        p1 = a;
        p2 = b;
        p3 = c;
    }
}
