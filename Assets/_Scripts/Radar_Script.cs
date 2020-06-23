using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Radar_Script : MonoBehaviour {


    [Range(10.0f, 500.0f)]
    public float radarSpeed;        // controls the speed at which the radar turns



    public Transform mapTransform; // stores the translation of the parent map object

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        // rotate the radar line around the centre
        gameObject.transform.RotateAround(mapTransform.position, new Vector3(0.0f, 1.0f, 0.0f), radarSpeed * Time.deltaTime);
	}
}
