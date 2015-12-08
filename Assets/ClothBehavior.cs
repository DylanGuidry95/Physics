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
    public int Height; //Length of Cloth
    public int Width; //Width of Cloth

    public float gCoeficient;

    public Vector3 Gravity;

    public float k; //tensioin
    public float b; //damping factor

    public bool net;
    public bool drape;

    // Use this for initialization
    void Start ()
    {
        SpawnParticles();
        SpawnSprings();
    }

    void SpawnSprings()
    {
        for(int i = 0; i < PARTICALS.Count; i++)
        {
            //horizontal
            if ((i % Width) != (Width - 1))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + 1].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + 1], l);
                SPRINGS.Add(spring);
            }

            if ((i % Width) != (Width - 1) && (i % Width) != (Width - 2))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + 2].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + 2], l);
                SPRINGS.Add(spring);
            }

            //Vertical Springs
            if (i < ((Width * Height) - Width))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + Width].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + Width], l);
                SPRINGS.Add(spring);
            }

            if (i < ((Width * Height) - Width * 2))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + Width * 2].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + Width * 2], l);
                SPRINGS.Add(spring);
            }

            //Dag Down
            if (((i % Width) != (Width - 1)) && ( i < ((Width * Height) - Height)))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + Width + 1].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + Width +1], l);
                SPRINGS.Add(spring);
            }

            //Dag Up
            if((i % Width != 0) && (i < (Width * Height) - Height))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(PARTICALS[i].transform.position, PARTICALS[i + Width - 1].transform.position);
                spring.MakeSpring(PARTICALS[i], PARTICALS[i + Width - 1], l);
                SPRINGS.Add(spring);
            }
        }

    }

    [ContextMenu ("SpawnPart")]
    void SpawnParticles()
    {
        int y = 0;
        int x = 0;
        int pos = 1;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Partical partical = Instantiate(ParticalPrefab);
                partical.gameObject.name = "Node" + pos;
                x++;
                pos++;
                partical.transform.parent = gameObject.transform;
                PARTICALS.Add(partical);

                if(drape & !net)
                {
                    partical.transform.position = new Vector3(j, i, 0);
                    if (partical.transform.position == new Vector3(Width - 1, Height - 1, 0) || partical.transform.position == new Vector3(0, Height - 1, 0))
                    {
                        partical.locked = true;
                    }
                }

                if(net && !drape)
                {
                    partical.transform.position = new Vector3(j, 0, i);
                    if (partical.transform.position == new Vector3(Width - 1, 0, Height - 1) || partical.transform.position == new Vector3(0, 0, Height - 1) ||
                        partical.transform.position == new Vector3(0, 0, 0) || partical.transform.position == new Vector3(Width - 1, 0, 0))
                    {
                        partical.locked = true;
                    }
                }

                y++;
            }
            y = 0;
        }
    }

    void EulerIntergration(Partical p)
    {
        if (!p.locked)
        {
            p.m_Acceleration = p.m_Force / p.m_Mass;
            p.m_Velocity = p.m_Acceleration.normalized * Time.fixedDeltaTime;
            p.transform.position += p.m_Velocity.normalized * Time.fixedDeltaTime;
            p.m_Force = Vector3.zero;
        }

    }

    void CalcSpringForce(SpringDamper s)
    {
        Vector3 disBetween = s.p1.transform.position - s.p2.transform.position;
        Vector3 disBetweenNorm = disBetween.normalized;

        float dis = CalcDis(s.p1.transform.position, s.p2.transform.position);
        float springForce = -k * (dis - s.l);  

        float v1 = Vector3.Dot(disBetweenNorm, s.p1.transform.position);
        float v2 = Vector3.Dot(disBetweenNorm, s.p2.transform.position);

        float springDamp = -b * (v1 - v2);

        float Damper = springForce + springDamp;

        Vector3 f1 = Damper * disBetweenNorm;
        Vector3 f2 = -f1;

        s.p1.m_Force += f1;
        s.p2.m_Force += f2;
        s.DrawLines();
    }

    void FixedUpdate()
    {
        Time.timeScale = 2f;
        Time.fixedDeltaTime = .02f * Time.timeScale;

        foreach (Partical p in PARTICALS)
        {
            p.m_Force = p.m_Mass * (Gravity * gCoeficient);
        }

        foreach (SpringDamper s in SPRINGS)
        {
            CalcSpringForce(s);
        }

        foreach (Partical p in PARTICALS)
        {
            EulerIntergration(p);
        }
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
