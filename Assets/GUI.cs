using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public GameObject cursorPrefab;
    GameObject mouse;
    public ClothBehavior Cloth;
    public List<ClothBehavior> CLOTHS = new List<ClothBehavior>();
    [Header("Wind Controls")]
    public GameObject WindControls;
    public Slider VerticalWind;
    public Slider HorizontalWind;
    public Slider WindSpeed;
    public Text WindMph;

    [Header("Spring Controls")]
    public GameObject SpringControls;
    public Slider Stiffness;

    [Header("Gravity Controls")]
    public GameObject GravityControls;
    public Slider gForce;
    public Text GravForce;

    [Header("Pause")]
    public GameObject Help;

    [Header("Cloth Controls")]
    public GameObject ClothControls;
    public Slider ClothSize;
    public Text CSize;

    bool windDirty = false;
    bool springDirty = false;
    bool gravDirty = false;
    bool helpDirty = false;
    bool clothDirty = false;

    void OnGUI()
    {
        Wind();
        Spring();
        Gravity();
        ClothSim();
    }

	// Use this for initialization
	void Start ()
    {
        //Spawns the cursor in the scene
        mouse = Instantiate(cursorPrefab);
        mouse.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        WindControls.SetActive(false);
        SpringControls.SetActive(false);
        GravityControls.SetActive(false);
        Help.SetActive(false);
        ClothControls.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisplaysOn();
        if (helpDirty == true || springDirty == true || gravDirty == true || windDirty == true || clothDirty == true)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        Vector3 moveSpace = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 1);

        mouse.transform.position = (moveSpace - Input.mousePosition) * -0.15f;

        if (Cursor.visible == true)
        {
            mouse.SetActive(false);
        }
        else
        {
            mouse.SetActive(true);
        }

        foreach (ClothBehavior c in CLOTHS)
        {
            CursorMovement(c);
        }
    }

    void Wind()
    {
        Cloth.Air.z = VerticalWind.value;
        Cloth.Air.x = HorizontalWind.value;
        Cloth.p = WindSpeed.value;

        WindMph.text = ((int)Cloth.Air.magnitude * Cloth.p).ToString() + "  " + "MpH";
    }

    void Spring()
    {
        Cloth.k = Stiffness.value;
    }

    void Gravity()
    {
        Cloth.gCoeficient = gForce.value; 
        GravForce.text = (Cloth.gCoeficient * Cloth.Gravity.magnitude).ToString() + "  " + "gForce";
    }

    void ClothSim()
    {
        CSize.text = ClothSize.value.ToString()+ " X " + ClothSize.value.ToString() + " cloth";
    }

    public void GenCloth()
    {
        ClothBehavior c = Instantiate(Cloth);
        CLOTHS.Add(c);
        c.Width = (int)ClothSize.value;
        c.Height = (int)ClothSize.value;
        c.x = 25 * CLOTHS.Count;
        c.y = 25 * CLOTHS.Count;

    }

    public void Close()
    {
        Application.Quit();
    }

    public void Reload()
    {
        Application.LoadLevel(0);
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
            springDirty = !springDirty;
            SpringControls.SetActive(springDirty);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            helpDirty = !helpDirty;
            Help.SetActive(helpDirty);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            clothDirty = !clothDirty;
            ClothControls.SetActive(clothDirty);
        }
    }

    /// <summary>
    /// Spawns a cursor in the middle of the screen and is used to grab and tear the cloth
    /// </summary>
    void CursorMovement(ClothBehavior c)
    {
        foreach (Node p in c.NODES)
        {
            if (Input.GetMouseButton(0) && c.CalcDis(mouse.transform.position, p.transform.position) < 1)
            {
                p.transform.position = mouse.transform.position;
            }
        }

    }
}
