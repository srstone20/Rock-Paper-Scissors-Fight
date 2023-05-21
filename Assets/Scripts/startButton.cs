using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{
    //build num of scene
    //public int startScene;

    public AudioSource select;
    //public AudioClip clip;
    //public float volume = .5f;

    public void StartGame()
    {
        select.Play();
        Invoke("moveOn", 1);
    }

    void moveOn()
    {
        SceneManager.LoadScene(1);
    }
}
