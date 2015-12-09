using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    public float m_Mass;                //m
    public Vector3 m_Velocity;          //v     {v = v + a *t}  
    public Vector3 m_Acceleration;      //a     {a = (1/m) *f
    public Vector3 m_Force;             //f     {f = Ef}

    public bool locked;                 // Check to tell if the node is locked at its starting position
}
