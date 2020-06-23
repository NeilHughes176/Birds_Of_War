using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_LoadScene : MonoBehaviour {

    public void NextLevelButton(int index)
    {
        Application.LoadLevel(index);
    }

}
/* public void LoadScene(int level)
 {
     SceneManager.LoadScene(level);
 }
 */

