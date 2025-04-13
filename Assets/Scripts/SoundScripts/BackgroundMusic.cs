using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource musicSource;
    public GameObject musicSoundObject;
    public AudioClip[] musicClips;
    public float volume = 0.25f;
    private int levelCounter;
    private bool isBossfightStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = musicSoundObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(musicSource.gameObject);
        playSong(1);

    }

    // Update is called once per frame
    void Update()
    {   
        if (levelCounter == 2) {
            playSong(2);
        } else if (levelCounter == 3) {
            playSong(3);
        } else if (levelCounter == 4) {
            musicSource.Stop();
            if (isBossfightStarted == true) {
                playSong(3);
            }
        }
    }

    public void startBoss() {
        isBossfightStarted = true;
    }

    void playSong(int songNum) {
        musicSource.clip = musicClips[songNum - 1];
        musicSource.volume = volume;
        musicSource.Play();
    }
}
