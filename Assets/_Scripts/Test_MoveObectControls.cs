using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MoveObectControls : MonoBehaviour {

    public float speed = 5.0F; //speed of movement
   // public float rotationspeed = 50F; //speed of rotaton 
  
    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed; //check if axis up/down
        float translation2 = Input.GetAxis("Horizontal") * speed; //check of axis L/R
        translation *= Time.deltaTime; //Move over time seconds 
        translation2 *= Time.deltaTime; //Move over time seconds
        transform.Translate(0, 0, translation); //Move along translation
        transform.Translate(translation2, 0, 0); //Move along rotation 

    }	
}
