using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Defining all the varibles for text
    public float cx, cy, cw, ch;
    public float hx, hy, hw, hh;
    public float wx, wy, ww, wh;
    //varible for images
    public float icx, icy;
    public float ihx, ihy;
    public float iwx, iwy;
   //varibles for font and font size
    public Font font;
    public int fontSize;

    //curency, health, and wave varibles
    public static int currency;
    public static int health;
    public static int wave;
    // texture varibles
    public Texture2D CurencyTexture,HealthTexture,WaveTeture;

    
    
    private void OnGUI()
    {
        GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        //Displays Curency text at the given corintaes
        GUI.Label(new Rect(cx, cy, cw, ch), currency.ToString());

        //Displays Health text at the given corinates
        GUI.Label(new Rect(hx, hy, hw, hh), health.ToString());

        //Displays the wave counter text at the given corinates
        GUI.Label(new Rect(wx, wy, ww, wh), wave.ToString());

        //Displays the currecy texture at given cordinates
        GUI.DrawTexture(new Rect(icx, icy, CurencyTexture.width, CurencyTexture.height), CurencyTexture);
        //Displays the health texture at given cordinates
        GUI.DrawTexture(new Rect(ihx, ihy, HealthTexture.width, HealthTexture.height), HealthTexture);
        //Displays the wave teture at the given corinates
        GUI.DrawTexture(new Rect(iwx, iwy, WaveTeture.width, WaveTeture.height), WaveTeture);



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currency++;
    }
}
