using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int cx, cy, cw, ch;
    public int hx, hy, hw, hh;
    public int icx, icy;
    public int ihx, ihy;
    public Font font;
    public int fontSize;
    public static int currency;
    public static int health;
    public Texture2D CurencyTexture,HealthTexture;
    
    private void OnGUI()
    {
        GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        //Displays Curency at the given corintaes
        GUI.Label(new Rect(cx, cy, cw, ch), currency.ToString());

        //Displays Health at the given corinates
        GUI.Label(new Rect(hx, hy, hw, hh), health.ToString());

        //Displays the currecy texture at given cordinates
        GUI.DrawTexture(new Rect(icx, icy, CurencyTexture.width, CurencyTexture.height), CurencyTexture);
        //Displays the health texture at given cordinates
        GUI.DrawTexture(new Rect(ihx, ihy, HealthTexture.width, HealthTexture.height), HealthTexture);



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
