using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScrene : MonoBehaviour {
   
    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit(); 
    }
}
