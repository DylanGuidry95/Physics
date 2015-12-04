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

    [Header("Size of Cloth")]
    [Space(10)]
    public int Rows = 10; //Length of Cloth
    public int Cols = 10; //Width of Cloth

    public float gCoeficient = 1;
    public float SpringForce;

    // Use this for initialization
    void Start ()
    {
        SpawnParticles();
        //CalcParticalForces();
        //CalcSpringForce();
	}

    void SpawnSprings(GameObject part)
    {
        foreach (GameObject p in PARTICALS)
        {
            if (p != part)
            {
                if (0 == p.GetComponent<Partical>().number % (Cols) && 0 == p.GetComponent<Partical>().number % (Rows - 1))
                {
                    GameObject spring = Instantiate(SpringsPrefab) as GameObject;
                    spring.GetComponent<SpringDamper>().p1 = part;
                    spring.GetComponent<SpringDamper>().p2 = p;
                    spring.GetComponent<SpringDamper>().l = CalcDis(part.GetComponent<Partical>().m_Pos, p.GetComponent<Partical>().m_Pos);

                    SPRINGS.Add(spring);
                }
            }
        }
    }

    [ContextMenu ("SpawnPart")]
    void SpawnParticles()
    {
        int t = 5;
        int y = 0;
        int pos = 1; 
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                GameObject partical = Instantiate(ParticalPrefab) as GameObject;
                partical.gameObject.name = "Node" + pos;
                partical.GetComponent<Partical>().number = pos;
                pos++;
                partical.transform.parent = gameObject.transform;
                PARTICALS.Add(partical);

                partical.GetComponent<Partical>().m_Pos = new Vector3(j + y, i + t, 0);
                y++;
            }
            y = 0;
            t++;
        }

        foreach (GameObject p in PARTICALS)
            SpawnSprings(p);

    }

    void ClothMovement()
    {
        foreach(GameObject p in PARTICALS)
        {
            foreach(GameObject s in SPRINGS)
            {
                p.GetComponent<Partical>().m_Acceleration = p.GetComponent<Partical>().m_Mass * -9.81f;
                s.GetComponent<SpringDamper>().b = (s.GetComponent<SpringDamper>().l = CalcDis(s.GetComponent<SpringDamper>().p1.GetComponent<Partical>().m_Pos,
                                                    s.GetComponent<SpringDamper>().p1.GetComponent<Partical>().m_Pos));
                SpringForce = s.GetComponent<SpringDamper>().k * s.GetComponent<SpringDamper>().b;
                p.GetComponent<Partical>().m_Velocity = p.GetComponent<Partical>().m_Velocity + p.GetComponent<Partical>().m_Acceleration;
                p.GetComponent<Partical>().m_Pos = p.GetComponent<Partical>().m_Pos + p.GetComponent<Partical>().m_Velocity;  
            }
        }
    }

    void Update()
    {
        ClothMovement();
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
