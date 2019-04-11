using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public int PlayerSpeed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetAxisRaw("Mouse X") > .5f || Input.GetAxisRaw("Mouse X") < -.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Mouse X") * PlayerSpeed * Time.deltaTime, 0f, 0f));
        }
    }
}
