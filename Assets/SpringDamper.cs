using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float k; //stiffness
    public float b; //damping factor
    public float l; //rest length

    public GameObject p1;
    public GameObject p2;

    public float tension = 200; 
}
