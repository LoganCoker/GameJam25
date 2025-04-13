using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static PlayerMovement pm;
    public GameObject gameOverScreen;
    public GameObject pauseMenu;
    private bool gameOver = false;
    private bool paused = false;
    public KeyCode PauseKey = KeyCode.Escape;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnPause() {

        togglePause();

    }

    public bool togglePause() {
        if (paused == true) {
            pm.enabled = true;
            pauseMenu.SetActive(false);
            paused = false;
            return (false);

        }
        else {
            pm.enabled = false;
            pauseMenu.SetActive(true);
            paused = true;
            return (true);

        }
    }

    // game over bounces in
    public void GameOver() {
        gameOverScreen.SetActive(true);
        RectTransform gameOverTransform = gameOverScreen.GetComponent<RectTransform>();
        gameOverTransform.anchoredPosition = new Vector3(0, 1000, 0);
        LeanTween.moveY(gameOverTransform, 0, 1f).setEaseOutBounce().setIgnoreTimeScale(true); 

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseKey)) {
            OnPause();
        }

        if (gameOver) {

            GameOver();

        }
    }
}
