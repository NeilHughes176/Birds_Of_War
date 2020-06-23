using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitforSecondsCoroutine : MonoBehaviour {

    // Use this for initialization
    void Start() {
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
    }
}
