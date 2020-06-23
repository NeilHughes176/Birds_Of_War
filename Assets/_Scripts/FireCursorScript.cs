using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCursorScript : MonoBehaviour
{

    public GameObject fireText;
    public float fireTextOffset;
    public float speed = 5.0F; //speed of movement
                               // public float rotationspeed = 50F; //speed of rotaton 

    private game_Controller_Script game_controller;
    void Start()
    {
        game_controller = GameObject.Find("Game_Controller").GetComponent<game_Controller_Script>();
        fireText.SetActive(false);
    }

    

    void Update()
    {

        float translation = Input.GetAxis("FireCursorUPDOWN") * speed; //check if axis up/down
        float translation2 = Input.GetAxis("FireCursorLEFTRIGHT") * speed; //check of axis L/R
        translation *= Time.deltaTime; //Move over time seconds 
        translation2 *= Time.deltaTime; //Move over time seconds
        transform.Translate(0, 0, translation); //Move along translation
        transform.Translate(translation2, 0, 0); //Move along rotation 

        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            fireText.transform.position = other.gameObject.transform.position + new Vector3(0.0f, fireText.transform.position.y, fireTextOffset);

            fireText.SetActive(true);
            if (Input.GetAxis("FireWeapon") > 0.8f)
            {
                game_controller.FirePlane(other.gameObject.transform.position);
            }
        }

    }
    


    void OnTriggerExit(Collider other)
    {
        fireText.SetActive(false);
    }
}




