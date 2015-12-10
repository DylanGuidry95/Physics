using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float l; //rest length

    public Node p1; //node1
    public Node p2; //node 2

    public LineRenderer spring;

    //Creates a spring based on the arguments based in when the spring is being created in the 
    //Spawn Springs function in Cloth Behavir class
    public void MakeSpring(Node a, Node b, float dis)
    {
        p1 = a;
        p2 = b;
        l = dis;
        DrawLines();
    }

    //Draws the lines between nodes the spring is connected to
    public void DrawLines()
    {
        //spring.SetPosition(0, p1.transform.position);
        //spring.SetPosition(1, p2.transform.position);
    }
}
