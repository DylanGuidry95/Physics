using UnityEngine;
using System.Collections.Generic;

public class ClothBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    GameObject target;
    public GameObject ParticalPrefab; //used to hold a refrence to are prefab
    public List<GameObject> PARTICALS = new List<GameObject>(); //List that hold all of the boids 
    public GameObject SpringsPrefab; //used to refrence the spring prefab
    public List<GameObject> SPRINGS = new List<GameObject>(); //List of all the springs in the system

    public float vLimit; //Velocity Limit

    [Header("Size of Cloth")]
    [Space(10)]
    public int Rows = 10; //Length of Cloth
    public int Cols = 10; //Width of Cloth



    // Use this for initialization
    void Start ()
    {
        SpawnParticles();
        foreach(GameObject p in PARTICALS)
            SpawnSprings(p);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //MoveCloth();
	}

    void CalcForces()
    {
        foreach (GameObject p in PARTICALS)
        {
            p.GetComponent<Partical>().m_Acceleration = p.GetComponent<Partical>().m_Mass * -9.81f;
        }
    }

    void SpawnSprings(GameObject part)
    {
        foreach (GameObject p in PARTICALS)
        {
            if(p != part)
            {
                if (p.GetComponent<Partical>().m_Pos.x == p.GetComponent<Partical>().m_Pos.x % Cols && p.GetComponent<Partical>().m_Pos.y == p.GetComponent<Partical>().m_Pos.y % Rows)
                {
                    GameObject spring = Instantiate(SpringsPrefab) as GameObject;
                    spring.GetComponent<SpringDamper>().p1 = part;
                    spring.GetComponent<SpringDamper>().p2 = p;
                    spring.GetComponent<SpringDamper>().DrawSpring();
                }
            }

        }
    }

    [ContextMenu ("SpawnPart")]
    void SpawnParticles()
    {
        int t = 0;
        int y = 0;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                GameObject partical = Instantiate(ParticalPrefab) as GameObject;
                partical.transform.parent = gameObject.transform;
                PARTICALS.Add(partical);

                partical.GetComponent<Partical>().m_Pos = new Vector3(j + y, i + t, 0);
                y++;
            }
            y = 0;
            t++;
        }
    }

    void MoveCloth()
    {
        //foreach (GameObject p in PARTICALS)
        //{
        //    Vector3 SpringDamper = gameObject.GetComponent<SpringDamper>().CalcSpringDamp(p);
        //    p.GetComponent<Partical>().m_Velocity = p.GetComponent<Partical>().m_Velocity.normalized + SpringDamper;
        //    //p.GetComponent<Partical>().m_Velocity = p.GetComponent<Partical>().m_Velocity.normalized;
        //    p.GetComponent<Partical>().m_Pos = p.GetComponent<Partical>().m_Pos + p.GetComponent<Partical>().m_Velocity.normalized;
        //}
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
