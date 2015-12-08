using UnityEngine;
using System.Collections;

public class SpringDamper : MonoBehaviour
{
    public float l; //rest length

    public Partical p1;
    public Partical p2;

    public void MakeSpring(Partical a, Partical b, float dis)
    {
        p1 = a;
        p2 = b;
        l = dis;
        DrawLines();
    }

    public void DrawLines()
    {
        Debug.DrawLine(p1.transform.position, p2.transform.position, Color.yellow);
    }
}
