using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonSound()
    {
        if (audioSource != null) {
            Debug.Log("Play clip.");
            audioSource.Play();
        }
    }
}
