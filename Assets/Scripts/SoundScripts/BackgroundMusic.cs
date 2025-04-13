using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource musicSource;
    public GameObject musicSoundObject;
    public AudioClip[] musicClips;
    public AudioClip bossStartSound;
    public float volume = 0.25f;
    private string currentSceneName;
    public int levelCounter;
    public bool isBossfightStarted = false;

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
        string newSceneName = SceneManager.GetActiveScene().name;

        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;
            SetMusicForScene(currentSceneName);
        }

         if (currentSceneName == "FinalLvl" && isBossfightStarted && musicSource.clip != musicClips[3]) {
            musicSource.PlayOneShot(bossStartSound);
            playSong(4);
        }
    }

    void playSong(int songNum) {
        musicSource.clip = musicClips[songNum - 1];
        musicSource.volume = volume;
        musicSource.Play();
    }

    void SetMusicForScene(string sceneName)
    {
        if (sceneName == "Level1") {
            playSong(1);
        } else if (sceneName == "LowerHeaven") {
            playSong(1);
        } else if (sceneName == "MoveLvl") {
            playSong(2);

        } else if (sceneName == "MRBossFight") {
            playSong(3);
        } else if (sceneName == "FinalLvl") {
            musicSource.Stop();
        }
    }
}
