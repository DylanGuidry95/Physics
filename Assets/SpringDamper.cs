using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float SpringCosntant;   //ks
    public float DampingFactor;    //kd
    public float RestLength;       //lo

    Particle p1, p2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ComputeForce()
    {
        float springForce = -SpringCosntant * CalcDis(p1.position, p2.position);
        float damForce = -DampingFactor * DiffInVelo(p1.velocity, p2.velocity);
        float springDampaner = springForce - damForce;
    }

    float DiffInVelo(Vector3 velo1, Vector3 velo2)
    {
        float diff;
        diff = ((velo2.y - velo1.y) * (velo2.y - velo1.y)) + ((velo2.x - velo1.x) * (velo2.x - velo1.x)) + ((velo2.z - velo1.z) * (velo2.z - velo1.z));
        return Mathf.Sqrt(diff);
    }

    float CalcDis(Vector3 pos1, Vector3 pos2)
    {
        float dis;
        dis = ((pos2.y - pos1.y) * (pos2.y - pos1.y)) + ((pos2.x - pos1.x) * (pos2.x - pos1.x)) + ((pos2.z - pos1.z) * (pos2.z - pos1.z));
        return Mathf.Sqrt(dis);
    }
}
