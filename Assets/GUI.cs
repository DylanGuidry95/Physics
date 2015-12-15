using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public ClothBehavior Cloth;

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

    bool windDirty = false;
    bool springDirty = false;
    bool gravDirty = false;
    bool helpDirty = false;
    bool ClothDirty = false;

    [Header("Cloth")]
    public GameObject ClothControls;
    public Slider clothSize;
    public Text cSize;

    void OnGUI()
    {
        Wind();
        Spring();
        Gravity();
        ClothUpdate();
    }

    // Use this for initialization
    void Start()
    {
        WindControls.SetActive(false);
        SpringControls.SetActive(false);
        GravityControls.SetActive(false);
        Help.SetActive(false);
        ClothControls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplaysOn();
        if (helpDirty == true || springDirty == true || gravDirty == true || windDirty == true || ClothDirty == true)
        {
            Cursor.visible = true;
        }
        else
        {

            Cursor.visible = false;
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

    void ClothUpdate()
    {
        Cloth.Width = (int)clothSize.value;
        Cloth.Height = (int)clothSize.value;
        cSize.text = Cloth.Width.ToString() + " X " + Cloth.Width.ToString();
    }

    public void Close()
    {
        Application.Quit();
    }

    public void Reload()
    {
        Application.LoadLevel(0);
    }

    public void CreateNewCloth()
    {
        Cloth.GenCloth();
    }

    void DisplaysOn()
    {
        if (Input.GetKeyDown(KeyCode.W))
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

        if(Input.GetKeyDown(KeyCode.C))
        {
            ClothDirty = !ClothDirty;
            ClothControls.SetActive(ClothDirty);
        }
    }
}