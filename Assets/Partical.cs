using UnityEngine;
using System.Collections;

public class Partical : MonoBehaviour
{
    public Vector3 m_Pos;               //r
    public Vector3 m_Mass;              //m
    public Vector3 m_Velocity;          //v     {v = v + a *t}  
    public Vector3 m_Acceleration;      //a     {a = (1/m) *f
    public Vector3 m_Force;             //f     {f = Ef}
    public Vector3 m_Momentum;          //p     {p = m * v}

    public int RowNum;
    public int ColNum;

    public bool locked;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = m_Pos;
    }
}
