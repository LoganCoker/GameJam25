using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMenu : MonoBehaviour {
    public SceneTransition sceneTransition;
    public AudioSource menuMusic;
    public AudioClip menuSong;
    public AudioClip playSound;

    void Start() {
        menuMusic.volume = 0.8f;
        menuMusic.clip = menuSong;
        menuMusic.Play();
    }

    public void Play() {
        if (sceneTransition != null) {
            sceneTransition.StartCoroutine(sceneTransition.FadeLoadScene("Level1"));
            menuMusic.clip = playSound;
            menuMusic.Play();
        } else {
            SceneManager.LoadScene("Level1");
        }
    }

    public void Exit() {
        Application.Quit();
    }

}

