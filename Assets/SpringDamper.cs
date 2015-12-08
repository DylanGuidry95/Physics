using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float k; //stiffness
    public float b; //damping factor
    public float l; //rest length

    public float e; //place holder

    public Partical p1;
    public Partical p2;

    public void MakeSpring(Partical a, Partical b)
    {
        p1 = a;
        p2 = b;
        Debug.DrawLine(p1.m_Pos, p2.m_Pos, Color.yellow, 200f);
    }
}
