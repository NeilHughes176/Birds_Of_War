using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_HowTo_Load : MonoBehaviour {

    void Start()
    {
        StartCoroutine(WaitTime());

    }


    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("HowToPlay_Screen");
    }

}
