using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int x;
    int BHealth;
    int MaxHealth;
    // Defining all the varibles for text
    public float cx, cy, cw, ch;
    public float wx, wy, ww, wh;

    //varible for images
    public float icx, icy;

   //varibles for font and font size
    public Font Defultfont,WaveFont;
    public int fontSize;

    //vectors varibles for game object that will be spawned at the start of the game.
    Vector3 BaseHealthPos, TowerPos, WaveProgressBarPos;
    public Vector3 Toffest,Hoffset,Poffset,HMoffset,PMoffset,BHBoffset;
    //Game object
    public GameObject BaseHealthBar, Tower, WaveProgressBar,ProgressMask,HealthMask,BaseHealthBarBackground;
    GameObject HealthRef, TowerRef, ProgressRef,ProgressMaskRef, HealthMaskRef, BaseHealthBarBackgroundRef;

    Transform ts;
    //curency, health, and wave varibles
    public static int currency;
    public static int health;
    public static int wave;

    // texture varibles
    public Texture2D CurencyTexture,WaveTeture;
    
    private void OnGUI()
    {
        GUI.skin.label.normal.textColor = Color.white;
        GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = Defultfont;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        //Displays Curency text at the given corintaes
        GUI.Label(new Rect(cx, cy, cw, ch), currency.ToString());


        //Displays the wave counter text at the given corinates
        GUI.skin.label.normal.textColor = Color.black;
        GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = WaveFont;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;
        GUI.Label(new Rect(wx, wy, ww, wh), wave.ToString());

        //Displays the currecy texture at given cordinates
            //GUI.DrawTexture(new Rect(icx, icy, CurencyTexture.width, CurencyTexture.height), CurencyTexture);
    }
    // Start is called before the first frame update
    void Start()
    {
       ts=GetComponent<Transform>();
       BHealth = GameObject.Find("Game Manager").GetComponent<GameManager>().Health;

      //spawn the game objects
        HealthRef = Instantiate(BaseHealthBar);
        TowerRef = Instantiate(Tower);
        ProgressRef = Instantiate(WaveProgressBar);
        ProgressMaskRef = Instantiate(ProgressMask);
        HealthMaskRef = Instantiate(HealthMask);
        BaseHealthBarBackgroundRef = Instantiate(BaseHealthBarBackground);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // set the vector to move the objects reltive to the camrea
        Vector3 TowerRefCam = new Vector3(ts.position.x, ts.position.y, TowerRef.transform.position.z);
        Vector3 HealthRefCam = new Vector3(ts.position.x, ts.position.y, HealthRef.transform.position.z);
        Vector3 ProgressRefCam = new Vector3(ts.position.x, ts.position.y, ProgressRef.transform.position.z);
        Vector3 ProgressMaskRefCam = new Vector3(ts.position.x, ts.position.y, ProgressMaskRef.transform.position.z);
        Vector3 HealthMaskRefCam = new Vector3(ts.position.x, ts.position.y, HealthMaskRef.transform.position.z);
        Vector3 BaseHealthBarBackgroundCam = new Vector3(ts.position.x, ts.position.y, BaseHealthBarBackgroundRef.transform.position.z);

        
       //Move the game objects reltive to the camrea
        TowerRef.transform.position=(TowerRefCam+Toffest);
        ProgressRef.transform.position=(ProgressRefCam+Poffset);
        HealthRef.transform.position=(HealthRefCam+Hoffset);
        ProgressMaskRef.transform.position = (ProgressMaskRefCam + PMoffset);
        HealthMaskRef.transform.position = (HealthMaskRefCam + HMoffset);
        BaseHealthBarBackgroundRef.transform.position = (BaseHealthBarBackgroundCam + BHBoffset);

        // move the sprite masks
        int y = 10;
        Debug.Log(y);
        float maskX = HealthRef.transform.localScale.x;
        Vector3 HealthMaskMovment = new Vector3(1, 0, 0);

        Vector3 HealthBarPos = HealthRef.transform.position;
        float HealthBarWidth = HealthRef.GetComponent<SpriteRenderer>().bounds.size.x;
        float Health = 5;
        float HealthCap = 10;
        float HealthRatio = Health / HealthCap;
        
        Vector3 MaskPos = new Vector3(HealthBarPos.x - HealthRatio * HealthBarWidth, HealthBarPos.y, HealthBarPos.z);
        HealthMaskRef.transform.position = MaskPos;


    }
}
