using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdenifyCursorScript : MonoBehaviour
{
    public GameObject identifyText;
    public float identifyTextOffset;
    public float speed = 5.0F; //speed of movement
                               // public float rotationspeed = 50F; //speed of rotaton 

    void Start()
    {
        identifyText.SetActive(false);
    }

    

    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed; //check if axis up/down
        float translation2 = Input.GetAxis("Horizontal") * speed; //check of axis L/R
        translation *= Time.deltaTime; //Move over time seconds 
        translation2 *= Time.deltaTime; //Move over time seconds
        transform.Translate(0, 0, translation); //Move along translation
        transform.Translate(translation2, 0, 0); //Move along rotation 
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            // Debug.Log("Hit");
            identifyText.transform.position = other.gameObject.transform.position + new Vector3(0.0f, identifyText.transform.position.y, identifyTextOffset);

            identifyText.SetActive(true); // Debug.Log("Set Active");


            if (Input.GetButtonDown("Identify"))
            {
                // Identify Game Code
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                other.gameObject.GetComponent<Plane_Script>().isIdentified = true;
            }
        }
    }
    


    void OnTriggerExit(Collider other)
    {
        identifyText.SetActive(false);
    }
}




