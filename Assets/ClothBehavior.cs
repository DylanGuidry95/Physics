using UnityEngine;
using System.Collections.Generic;

public class ClothBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    GameObject target;
    public GameObject ParticalPrefab; //used to hold a refrence to are prefab
    public List<GameObject> PARTICALS = new List<GameObject>(); //List that hold all of the boids 

    public float vLimit;

    [Header("Size of Cloth")]
    [Space(10)]
    public float LengthOfCloth = 10; //Length of Cloth
    public float WidthOfCloth = 10; //Width of Cloth

    public float tension;

    // Use this for initialization
    void Start () {
        DrawCloth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCloth();
	}

    void DrawCloth()
    {
        for(int i = 0; i < LengthOfCloth; i++)
        {
            for (int j = 0; j < WidthOfCloth; j++)
            {
                GameObject partical = Instantiate(ParticalPrefab) as GameObject;
                partical.transform.parent = gameObject.transform ;
                PARTICALS.Add(partical);

                partical.GetComponent<Partical>().m_Pos = new Vector3(i, j, 0);
            }
        }
    }

    Vector3 CalcSpringDamp(GameObject partical)
    {
        Vector3 damp = Vector3.zero;

        foreach (GameObject p in PARTICALS)
        {
            if(p != partical)
            {
                float e = CalcDis(partical.GetComponent<Partical>().m_Pos, p.GetComponent<Partical>().m_Pos);
                float v1 = e * partical.GetComponent<Partical>().m_Velocity.magnitude;
                float v2 = e * p.GetComponent<Partical>().m_Velocity.magnitude;
                damp = tension * new Vector3(e,v1,v2);
            }
        }

        return damp;
    }

    void MoveCloth()
    {
        foreach (GameObject p in PARTICALS)
        {
            Vector3 SpringDamper = CalcSpringDamp(p);
            p.GetComponent<Partical>().m_Velocity += SpringDamper;
            p.GetComponent<Partical>().m_Pos += p.GetComponent<Partical>().m_Velocity;  
        }
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
