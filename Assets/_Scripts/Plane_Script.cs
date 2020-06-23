using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Script : MonoBehaviour {

    // decide what plane type it is
    // based on number of existing planes
    // ecide colour from plane type
    //random spawn point between 0-360 on circle edge
    // moves toward centre

    // PUBLIC //

 

    public game_Controller_Script game_controller;      // script reference

    public int planeHealth;         // stores the planes health
    public float baseSpeed;         // stores the base speed for planes
    public float panicSpeedFactor;   // stores the panic speed bonus to be added to base speed
    public bool canSpawn;

    public float fadeLerpSpeed =.2f;
    public float fadeIterationSpeed = .2f;

    

    public bool isIdentified;
    public Vector3 planeTarget;
    // PRIVATE //


    private bool isDead;
    private bool isAngry;       // if wrong weapon has been fired at the plane, become angry
    private bool isReturning; // if is returning is true, it is moving toward the origin, if not, it is not
    public bool isFired;

    public game_Controller_Script.PLANETYPES planeType;    // the plane type // Getter and setter below

    private MeshRenderer localMeshRend;

    // Getters and setters //
    public void SetPlaneType(game_Controller_Script.PLANETYPES type) { planeType = type; }
    public game_Controller_Script.PLANETYPES GetPlaneType() { return planeType; }


    // Use this for initialization
    void Start ()
    {
        // get script reference
        game_controller = GameObject.Find("Game_Controller").GetComponent<game_Controller_Script>();
        localMeshRend = gameObject.GetComponent<MeshRenderer>();
        localMeshRend.enabled = false;
        isIdentified = false;
        isDead = true;
        isAngry = false;
        canSpawn = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Friendly")
        {
            if (localMeshRend.enabled == false)
            {
                if (isFired)
                {
                    // set postion to centre
                    gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                    gameObject.transform.LookAt(planeTarget, Vector3.up);
                    localMeshRend.enabled = true;
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            if (isReturning)
            {
                planeTarget = new Vector3(0.0f, 0.0f, 0.0f);
                gameObject.transform.LookAt(planeTarget, Vector3.up);
                /*
                Vector3 heading = planeTarget - gameObject.transform.position;
                heading = Vector3.Normalize(heading);
                gameObject.transform.position += baseSpeed * 2 * heading / 100f;
                */
            }
            if (localMeshRend.enabled == true)
            {
                gameObject.transform.LookAt(planeTarget);
                Vector3 heading = planeTarget - gameObject.transform.position;
                heading = Vector3.Normalize(heading);
                if (isReturning)
                    gameObject.transform.position += baseSpeed * 2 * heading / 100f;
                else
                    gameObject.transform.position += baseSpeed * heading / 100f;
            }
        }


        if (gameObject.tag == "Enemy")
        {
            // set the spawn point of enemy planes to a random point on outside of the circle
            if (localMeshRend.enabled == false)
            {
                if (canSpawn == true)
                {
                    //localMeshRend.enabled = true;
                    float spawnAngle = Random.Range(0.0f, 360.0f);

                    gameObject.transform.position = new Vector3(5.0f, 0.0f, 0.0f);
                    gameObject.transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), spawnAngle);
                    //gameObject.GetComponent<MeshRenderer>().enabled = true;
                }

            }
            if (localMeshRend.enabled == true)
            {
                Vector3 heading = new Vector3(0.0f, 0.0f, 0.0f) - gameObject.transform.position;
                heading = Vector3.Normalize(heading);
                if (isAngry)
                    gameObject.transform.position += baseSpeed * panicSpeedFactor * heading / 100f;
                else
                    gameObject.transform.position += baseSpeed * heading / 100f;
            }
            gameObject.transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.up);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Friendly") && other.gameObject.CompareTag("Centre") && isReturning)
        {
            localMeshRend.enabled = false;
            isReturning = false;
            isFired = false;
        }
        if (other.gameObject.CompareTag("Friendly") && localMeshRend.enabled == true)
        {
            // if wrong plane type
            // get angry!! and add speed;

            // if other is bomber, weak to eagle
            // if local is thunder or bomber
            // get angery
            // if other is eagle, weak to thundr
            // if local is bomber or eagle
            // if other is thunder, weak to bomber
            // if local is thunder or eagle

            switch (planeType)
            {
                case game_Controller_Script.PLANETYPES.BOMBER:
                    if (other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.BOMBER || other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.THUNDER)
                    {
                        isAngry = true;
                    }
                    if(other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.EAGLE)
                    {
                        localMeshRend.enabled = false;
                    }break;
                case game_Controller_Script.PLANETYPES.EAGLE:
                    if (other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.EAGLE || other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.BOMBER)
                    {
                        isAngry = true;
                        break;
                    }
                    else
                    {
                        localMeshRend.enabled = false;
                        break;
                    }
                case game_Controller_Script.PLANETYPES.THUNDER:
                    if (other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.THUNDER || other.gameObject.GetComponent<Plane_Script>().GetPlaneType() == game_Controller_Script.PLANETYPES.EAGLE)
                    {
                        isAngry = true;
                        break;
                    }
                    else
                    {
                        localMeshRend.enabled = false;
                        break;
                    }
            }

            
            other.gameObject.GetComponent<Plane_Script>().isFired = false ;
            other.gameObject.GetComponent<Plane_Script>().isReturning = true;
        }
        if (other.gameObject.CompareTag("Centre"))
        {
            game_controller.baseHealth--;
        }
        if(other.gameObject.CompareTag("Radar") && gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            StartCoroutine(fadePlane());
        }
        
    }

    IEnumerator fadePlane()
    {
        Color tgtColor = (Vector4)gameObject.GetComponent<MeshRenderer>().material.color - new Vector4(0.0f, 0.0f, 0.0f, gameObject.GetComponent<MeshRenderer>().material.color.a);
        while (gameObject.GetComponent<MeshRenderer>().material.color.a > 0.0f && !isIdentified)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(gameObject.GetComponent<MeshRenderer>().material.color, tgtColor, fadeLerpSpeed * Time.deltaTime);
            yield return new WaitForSeconds(fadeIterationSpeed);
        }
    }


    

}
