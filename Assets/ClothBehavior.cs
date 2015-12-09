using UnityEngine;
using System.Collections.Generic;

public class ClothBehavior : MonoBehaviour
{
    [Header("GameObjects")]
    public Node NodePrefab; //used to hold a refrence to are prefab
    public List<Node> NODES = new List<Node>(); //List that hold all of the nodes 
    [Space(10)]
    public SpringDamper SpringsPrefab; //used to refrence the spring prefab
    public List<SpringDamper> SPRINGS = new List<SpringDamper>(); //List of all the springs in the system
    [Space(10)]
    public AeroDynamics AeroPrefab; //used to refrence the triangle prefab
    public List<AeroDynamics> TRIANGLES; //List of all the triangles in the simulation

    [Header("Size of Cloth")]
    [Space(10)]
    public int Height; //Length of Cloth
    public int Width; //Width of Cloth

    public float gCoeficient; //Adjusts the force of gravity

    public Vector3 Gravity;
    public Vector3 Air; 

    public float vLimit; //Velocity Limit of the nodes

    //Spring
    public float k; //stiffness
    public float b; //damping factor

    //AeroDynamic
    public float p;             //density of air/water (Constant)
    public float Cd;            //coeficient of drag for the object (Constant)

    public bool net; //Spawns the Nodes in the shape of a net
    public bool drape; //Spawns the Nodes in the shape of a drape

    // Use this for initialization
    void Start ()
    {
        SpawnNodes();
        SpawnSprings();
        SpawnTrianlge();
    }

    /// <summary>
    /// Spawns all springs in the simulation and links them to nodes that follow a set of case checks
    /// </summary>
    void SpawnSprings()
    {
        for(int i = 0; i < NODES.Count; i++)
        {
            //horizontal
            if ((i % Width) != (Width - 1))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + 1].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + 1], l);
                SPRINGS.Add(spring);
            }

            if ((i % Width) != (Width - 1) && (i % Width) != (Width - 2))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + 2].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + 2], l);
                SPRINGS.Add(spring);
            }

            //Vertical Springs
            if (i < ((Width * Height) - Width))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + Width].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + Width], l);
                SPRINGS.Add(spring);
            }

            if (i < ((Width * Height) - Width * 2))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + Width * 2].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + Width * 2], l);
                SPRINGS.Add(spring);
            }

            //Dag Down
            if (((i % Width) != (Width - 1)) && ( i < ((Width * Height) - Height)))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + Width + 1].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + Width +1], l);
                SPRINGS.Add(spring);
            }

            //Dag Up
            if((i % Width != 0) && (i < (Width * Height) - Height))
            {
                SpringDamper spring = Instantiate(SpringsPrefab);
                spring.transform.parent = gameObject.transform;
                float l = CalcDis(NODES[i].transform.position, NODES[i + Width - 1].transform.position);
                spring.MakeSpring(NODES[i], NODES[i + Width - 1], l);
                SPRINGS.Add(spring);
            }
        }

    }

    /// <summary>
    /// Spawns all the Nodes in the simulation based on the width and height of the cloth
    /// </summary>
    void SpawnNodes()
    {
        int y = 0;
        int x = 0;
        int pos = 1;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Node Node = Instantiate(NodePrefab);
                Node.gameObject.name = "Node" + pos;
                x++;
                pos++;
                Node.transform.parent = gameObject.transform;
                NODES.Add(Node);

                if(drape & !net)
                {
                    Node.transform.position = new Vector3(j, i, 0);
                    if (Node.transform.position == new Vector3(Width - 1, Height - 1, 0) || Node.transform.position == new Vector3(0, Height - 1, 0))
                    {
                        Node.locked = true;
                    }
                }

                if(net && !drape)
                {
                    Node.transform.position = new Vector3(j, 0, i);
                    if (Node.transform.position == new Vector3(Width - 1, 0, Height - 1) || Node.transform.position == new Vector3(0, 0, Height - 1) ||
                        Node.transform.position == new Vector3(0, 0, 0) || Node.transform.position == new Vector3(Width - 1, 0, 0))
                    {
                        Node.locked = true;
                    }
                }

                y++;
            }
            y = 0;
        }
    }

    /// <summary>
    /// Spawns all the Triangles in the simulation
    /// Called in through the start function.
    /// </summary>
    void SpawnTrianlge()
    {
        for (int i = 0; i < NODES.Count; i++)
        {
            if ((i < (Width * Height) - Height) && ((i % Width) != (Width - 1)))
            {
                AeroDynamics tri = Instantiate(AeroPrefab);
                tri.transform.parent = gameObject.transform; 
                //NW, NE, SW
                tri.CreateTriangle(NODES[i], NODES[i + 1], NODES[i + Width]);
                //NW, NE, SE
                tri.CreateTriangle(NODES[i], NODES[i + 1], NODES[i + Width + 1]);
                //NE, SW, SE
                tri.CreateTriangle(NODES[i + 1], NODES[i + Width], NODES[i + Width + 1]);
                //NW, SW, SE
                tri.CreateTriangle(NODES[i], NODES[i + Width], NODES[i + Width + 1]);
                TRIANGLES.Add(tri);
            }
        }
    }

    /// <summary>
    /// Intergrates motion for each node in the simulation
    /// 
    /// These vectors are only calculated if the node is not locked in place
    /// 
    /// Intergrates motion into the simulation
    /// Acceleration = force / mass
    /// Velocity = Acceleration * DeltaTime
    /// Position = current Position + Velocity normalized * DeltaTime
    /// Zero out the force
    /// </summary>
    /// <param name="p"></param>
    void EulerIntergration(Node p)
    {
        if (!p.locked)
        {
            p.m_Acceleration = (p.m_Force / p.m_Mass);
            p.m_Velocity += p.m_Acceleration * Time.deltaTime;
            if (p.m_Velocity.magnitude > vLimit)
            {
                p.m_Velocity = p.m_Velocity.normalized;
            }
            p.transform.position += p.m_Velocity * Time.deltaTime;
            p.m_Force = Vector3.zero;
        }
    }

    /// <summary>
    /// Calculates the Spring Force of each spring in the simulation
    /// </summary>
    /// <param name="s"></param>
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

    void CalcAeroForce(AeroDynamics a)
    {
        Vector3 velocity = (a.p1.m_Velocity + a.p2.m_Velocity + a.p3.m_Velocity) / 3;
        Vector3 relativeVelocity = velocity - Air;

        Vector3 disBetweenP2P1 = a.p2.transform.position - a.p2.transform.position;
        Vector3 normalP2P1 = disBetweenP2P1 / disBetweenP2P1.magnitude;
        Vector3 disBetweenP3P1 = a.p3.transform.position - a.p1.transform.position;
        Vector3 normalP3P1 = disBetweenP3P1 / disBetweenP3P1.magnitude;
        float normal = Vector3.Dot(normalP2P1, normalP3P1);

        float area = 0.5f * (normalP2P1.magnitude);
        a.a = area * ((velocity * normal) / velocity.magnitude);


        a.DrawTrianlge();
    }

    /// <summary>
    /// Steps for cloth simulation
    /// 1) Compute Forces
    ///     - For each node apply gravity
    ///     - for each spring compute and apply forces
    ///     - for each triangle compute and apply aerodynamic forces
    /// 
    /// 2)Integrate Motion
    ///     - for each Node apply forward Euler Integration
    /// </summary>
    void FixedUpdate()
    {
        Time.timeScale = .7f;
        Time.fixedDeltaTime = .01f * Time.timeScale;

        //Applies gravity to each Node
        foreach (Node p in NODES)
        {
            p.m_Force += p.m_Mass * (Gravity * gCoeficient);
        }

        //Computes and Applies forces for each spring
        foreach (SpringDamper s in SPRINGS)
        {
            CalcSpringForce(s);
        }

        foreach (AeroDynamics a in TRIANGLES)
        {
            CalcAeroForce(a);
        }

        //Applies Euler Intergration to each node
        foreach (Node p in NODES)
        {
            EulerIntergration(p);
        }
    }

    //Calculate distance between to vector3
    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
