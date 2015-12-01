using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float SpringCosntant;   //ks
    public float DampingFactor;    //kd
    public float RestLength;       //lo

    public Particle p1;
    public Particle p2;

    public float tension;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void SetConnection(Particle a, Particle b)
    {
        p1 = a;
        p2 = b;
    }

    public Vector3 CalcSpringDamp(GameObject partical)
    {
        Vector3 a = Vector3.zero;
        Vector3 e = Vector3.zero;
        Vector3 v = Vector3.zero;
        foreach (GameObject p in gameObject.GetComponent<ClothBehavior>().PARTICALS)
        {
            e = p.GetComponent<Partical>().m_Pos - partical.GetComponent<Partical>().m_Pos;
            v = p.GetComponent<Partical>().m_Velocity.normalized - partical.GetComponent<Partical>().m_Velocity.normalized;
            a = e - v;
        }
        return a * (tension / gameObject.GetComponent<ClothBehavior>().PARTICALS.Count);

    }
}
