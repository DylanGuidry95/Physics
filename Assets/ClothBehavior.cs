using UnityEngine;
using System.Collections.Generic;

public class ClothBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    GameObject target;
    public Partical ParticalPrefab; //used to hold a refrence to are prefab
    public List<Partical> PARTICALS = new List<Partical>(); //List that hold all of the boids 
    [Space(10)]
    public SpringDamper SpringsPrefab; //used to refrence the spring prefab
    public List<SpringDamper> SPRINGS = new List<SpringDamper>(); //List of all the springs in the system

    [Header("Size of Cloth")]
    [Space(10)]
    public int Rows; //Length of Cloth
    public int Cols; //Width of Cloth

    public float gCoeficient = 1;

    // Use this for initialization
    void Start ()
    {
        SpawnParticles();
        SpawnSprings();
    }

    void SpawnSprings()
    {
        Partical[] part = PARTICALS.ToArray();
        for(int i = 0; i <= PARTICALS.Count; i++)
        {
            SpringDamper spring = Instantiate(SpringsPrefab);
            spring.p1 = part[i];

            if (part[i].pos % Cols != Cols - 1)
            {
                Debug.Log("hor");
                spring.p2 = part[i + 1];

                Debug.DrawLine(spring.p1.m_Pos, spring.p2.m_Pos, Color.yellow, 200);
                SPRINGS.Add(spring);
            }
            
            else if(part[i].pos < (Cols * Rows) - Cols)
            {
                Debug.Log("vert");
                spring.p2 = part[i + Cols];

                Debug.DrawLine(spring.p1.m_Pos, spring.p2.m_Pos, Color.yellow, 200);
                SPRINGS.Add(spring);
            }
        }

    }

    [ContextMenu ("SpawnPart")]
    void SpawnParticles()
    {
        int t = 5;
        int y = 0;
        int x = 0;
        int pos = 1;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Partical partical = Instantiate(ParticalPrefab);
                partical.gameObject.name = "Node" + pos;
                partical.pos = pos;
                x++;
                pos++;
                partical.transform.parent = gameObject.transform;
                PARTICALS.Add(partical);

                partical.m_Pos = new Vector3(j, i, 0);
                y++;
            }
            y = 0;
            t++;
        }
    }

    void ClothMovement()
    {
        foreach(Partical p in PARTICALS)
        {
            p.m_Force = p.m_Mass * -9.81f;
        }
        CalcSpringForce();
        EulerIntergration();
    }

    void EulerIntergration()
    {
        foreach(Partical p in PARTICALS)
        {
            p.m_Acceleration = (p.m_Mass.magnitude) * p.m_Force;
            p.m_Velocity = p.m_Velocity + p.m_Acceleration * Time.deltaTime;
            p.m_Pos = p.m_Pos + p.m_Velocity * Time.deltaTime;
        }
    }

    void CalcSpringForce()
    {
        foreach(SpringDamper s in SPRINGS)
        {
            Vector3 disBetween = s.p1.m_Pos - s.p2.m_Pos;
            Vector3 disBetweenNorm = disBetween.normalized;

            float dir = s.e / s.l;
            float dis = CalcDis(s.p1.m_Pos, s.p2.m_Pos);
            float springForce = -s.k * (s.l - dis);

            float v1 = Vector3.Dot(disBetweenNorm, s.p1.m_Pos);
            float v2 = Vector3.Dot(disBetweenNorm, s.p2.m_Pos);

            float springDamp = -s.b * (v1 - v2);

            float Damper = springForce + springDamp;

            Vector3 f1 = Damper * disBetweenNorm;
            Vector3 f2 = -f1;

            s.p1.m_Force = f1;
            s.p2.m_Force = f2;
        }
    }

    void Update()
    {
        //ClothMovement();
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
