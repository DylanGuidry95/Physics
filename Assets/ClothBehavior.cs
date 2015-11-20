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


    public List<GameObject> TriPoint1 = new List<GameObject>();
    public List<GameObject> TriPoint2 = new List<GameObject>();
    public List<GameObject> TriPoint3 = new List<GameObject>();

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
        SetPoints();
    }

    void SetPoints()
    {
        foreach(GameObject p in PARTICALS)
        {
            if (PARTICALS.IndexOf(p) % 3 == 0)
                TriPoint1.Add(p);
            else if (PARTICALS.IndexOf(p) % 3 == 1)
                TriPoint1.Add(p);
            else if (PARTICALS.IndexOf(p) % 3 == 2)
                TriPoint1.Add(p);
        }

    }

    void MoveCloth()
    {
        foreach (GameObject p in PARTICALS)
        {

        }
    }
}
