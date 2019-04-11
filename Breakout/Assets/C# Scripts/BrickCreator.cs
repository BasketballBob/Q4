using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCreator : MonoBehaviour
{
    public GameObject brick;
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        /*GameObject clone;
        float x, y;
        for(y=1;y<=7; y++)
        {
            for(x= -7.5f;x<=7.5f; x++)
            {
                
                clone = Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity);
            }
        }*/

        float gridx = -7.5f;
        float gridy = 10f;
        float GridWidth = 12;
        float GridHeight = 8;
        float RectHeight = .255f;
        float RectWidth = 1f;
        float RectSpacing = 4/13f;

        for (int xPos = 0; xPos < GridWidth; xPos++)
        {
            for (int  yPos = 0;yPos < GridHeight; yPos++)
            {
                GameObject clone;
                clone = Instantiate(brick, new Vector3(gridx + RectWidth * xPos + RectSpacing * (xPos + 1), gridy - RectHeight * yPos - RectSpacing * (yPos + 1), 0), Quaternion.identity);
            }
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
