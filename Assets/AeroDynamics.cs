using UnityEngine;
using System.Collections;

public class AeroDynamics : MonoBehaviour
{
    public Node p1, p2, p3;     //Three node the triangle connects too.
    public Vector3 a;             //cross sectional area of the object
    public Vector3 e;           //Unit vector in the opposite direction of the velocity

    //sets the three points of the triangle based on the values passed in through the arguments.
    //Called by the SpawnTriangles() in ClothSimulation
    public void CreateTriangle(Node a, Node b, Node c)
    {
        p1 = a;
        p2 = b;
        p3 = c;
        DrawTrianlge();
    }

    //Draws lines between the three points of the triangle
    public void DrawTrianlge()
    {
        Debug.DrawLine(p1.transform.position, p2.transform.position, Color.green);
        Debug.DrawLine(p2.transform.position, p3.transform.position, Color.green);
        Debug.DrawLine(p3.transform.position, p1.transform.position, Color.green);
    }
}
