using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Slider gravityMod;
    public Slider windMod;
    public Slider stiffnessMod;

    public ClothBehavior Cloth;

    void OnGui()
    {
        Cloth.gCoeficient = gravityMod.value;
        Cloth.p = windMod.value;
        Cloth.k = stiffnessMod.value;
    }

	// Use this for initialization
	void Start ()
    {
        Cloth = (ClothBehavior)FindObjectOfType(typeof(ClothBehavior));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
