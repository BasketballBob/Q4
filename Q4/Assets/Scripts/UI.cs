using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int x, y, w, h;
    public Font font;
    public int fontSize;
    public Texture2D textureToDisplay;
    private void OnGUI()
    {
        GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        //Displays Text at the given corintaes
        GUI.Label(new Rect(x, y, w, h), "Hello World!");

        //Displays a Texture at the given corintaes and sets it at the given texeture width and hight
        GUI.DrawTexture(new Rect(x, y, textureToDisplay.width, textureToDisplay.height), textureToDisplay);


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
