using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public GameObject WindControls;
    public GameObject SpringControls;
    public GameObject GravityControls;
    public GameObject Pause;

    bool windDirty = false;
    bool SpringDirty = false;
    bool gravDirty = false;
    bool pauseDirty = false;

    void OnGUI()
    {

    }

	// Use this for initialization
	void Start ()
    {
        WindControls.SetActive(false);
        SpringControls.SetActive(false);
        GravityControls.SetActive(false);
        Pause.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisplaysOn();
	}

    void DisplaysOn()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            windDirty = !windDirty;
            WindControls.SetActive(windDirty);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            gravDirty = !gravDirty;
            GravityControls.SetActive(gravDirty);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpringDirty = !SpringDirty;
            SpringControls.SetActive(SpringControls);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseDirty = !Pause;
            Pause.SetActive(Pause);
        }
    }
}
