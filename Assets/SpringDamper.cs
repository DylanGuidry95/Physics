using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float SpringCosntant;   //ks
    public float DampingFactor;    //kd
    public float RestLength;       //lo

    public GameObject p1;
    public GameObject p2;

    public float viscous; //coeficient for damp factor
    public float tension; //coeficient for rest

    public Vector3 m_Pos;

    // Use this for initialization
    void Start ()
    {

	}

    public void DrawSpring()
    {
        Debug.DrawLine(p1.GetComponent<Partical>().m_Pos, p2.GetComponent<Partical>().m_Pos, Color.green);
    }

	// Update is called once per frame
	void Update ()
    {
	    
	}

    //public Vector3 CalcSpringDamp(GameObject partical)
    //{
    //    Vector3 a = Vector3.zero;
    //    Vector3 e = Vector3.zero;
    //    Vector3 v = Vector3.zero;



    //    foreach (GameObject p in gameObject.GetComponent<ClothBehavior>().PARTICALS)
    //    {
    //        e = p.GetComponent<Partical>().m_Pos - partical.GetComponent<Partical>().m_Pos;
    //        v = p.GetComponent<Partical>().m_Velocity.normalized - partical.GetComponent<Partical>().m_Velocity.normalized;
    //        a = e - v;
    //    }
    //    return a * (tension / gameObject.GetComponentInParent<ClothBehavior>().PARTICALS.Count);

    //}
}
