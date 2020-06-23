using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_Controller_Script : MonoBehaviour {

    public GameObject[] EnemyPlanes;

    public GameObject[] FriendlyPlanes;

    public GameObject weaponChoiceObj;

    public Mesh[] planeMeshes;
    public enum PLANETYPES { BOMBER, THUNDER, EAGLE }

    public PLANETYPES currentWeapon;

    [Range(1.0f, 100.0f)]
    public int baseHealth;

    [Header("Max no of: Bomber, Eagle, Thunder")]
    public int maxBombers;
    public int maxEagles;
    public int maxThunders;

    public float enemySpawnStaggerTime;
    private int totalBombers = 0; public void SetTotBom(int a) { totalBombers = a; } public int GetTotBom() { return totalBombers; }
    private int totalThunders = 0; public void SetTotThd(int a) { totalThunders = a; }  public int GetTotThd() { return totalThunders; }
    private int totalEagles = 0; public void SetTotEag(int a) { totalEagles = a; }  public int GetTotEag() { return totalEagles; }


    // Use this for initialization
    void Start () {
        SetPlaneTypes();
        baseHealth += EnemyPlanes.Length;
        StartCoroutine(SetPlaneToSpawn());
        currentWeapon = PLANETYPES.BOMBER;
        weaponChoiceObj.GetComponent<MeshFilter>().mesh = planeMeshes[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(baseHealth < 1)
        {
            Debug.Log("DEATH! Too many Planes made it through your Defenses");
            // death state
            // restart game
        }
        if (Input.GetButtonDown("CycleWeapon"))
        {
            Debug.Log("Weapon cylced");
            switch(currentWeapon)
            {
                case PLANETYPES.BOMBER:
                    currentWeapon = PLANETYPES.EAGLE;
                    weaponChoiceObj.GetComponent<MeshFilter>().mesh = planeMeshes[1];
                    break;
                case PLANETYPES.EAGLE:
                    currentWeapon = PLANETYPES.THUNDER;
                    weaponChoiceObj.GetComponent<MeshFilter>().mesh = planeMeshes[2];
                    break;
                case PLANETYPES.THUNDER:
                    currentWeapon = PLANETYPES.BOMBER;
                    weaponChoiceObj.GetComponent<MeshFilter>().mesh = planeMeshes[0];
                    break;
            }
        }
	}


    public void FirePlane(Vector3 target)
    {
        for(int i =0; i <FriendlyPlanes.Length; i++)
        {
            //if(FriendlyPlanes[i].GetComponent<Plane_Script>().GetPlaneType() == currentWeapon)
            //{
            if(!FriendlyPlanes[i].GetComponent<Plane_Script>().isFired)
                switch (currentWeapon)
                {
                    case PLANETYPES.BOMBER:
                        FriendlyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.BOMBER);
                        FriendlyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[0];
                        break;
                    case PLANETYPES.EAGLE:
                        FriendlyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.EAGLE);
                        FriendlyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[1];
                        break;
                    case PLANETYPES.THUNDER:
                        FriendlyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.THUNDER);
                        FriendlyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[2];
                        break;
                }
                if (FriendlyPlanes[i].GetComponent<Plane_Script>().isFired == false)
                FriendlyPlanes[i].GetComponent<Plane_Script>().isFired = true;
                FriendlyPlanes[i].GetComponent<Plane_Script>().planeTarget = target;
                break;
            }
        
    }

    void SetPlaneTypes()
    {
        for (int i = 0; i < EnemyPlanes.Length; i++)
        {
            if (totalBombers < maxBombers)
            {
                EnemyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.BOMBER);
                EnemyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[0];
                totalBombers++;
            }
            else if (totalEagles < maxEagles)
            {
                EnemyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.EAGLE);
                EnemyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[1];
                totalEagles++;
            }
            else if (totalThunders < maxThunders)
            {
                EnemyPlanes[i].GetComponent<Plane_Script>().SetPlaneType(PLANETYPES.THUNDER);
                EnemyPlanes[i].GetComponent<MeshFilter>().mesh = planeMeshes[2];
                totalThunders++;
            }
        }
        // for (int i = 0; i < Planes.Length; i++)
        {
            // Debug.Log(Planes[i].GetComponent<Plane_Script>().GetPlaneType().ToString());
        }
    }
    
    private IEnumerator SetPlaneToSpawn()
    {
        
        while (true)
        {
            int planeID = 0;
            while (EnemyPlanes[planeID].GetComponent<MeshRenderer>().enabled == true)
            {
                planeID++;
                if (planeID > EnemyPlanes.Length - 1)
                {
                    planeID = 0;
                    break;
                }
            }
            EnemyPlanes[planeID].GetComponent<Plane_Script>().canSpawn = true;
            yield return new WaitForSeconds(enemySpawnStaggerTime);
            
        }
    }
}
