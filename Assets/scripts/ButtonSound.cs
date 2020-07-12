using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    
    public AudioClip myClip;
    public static ButtonSound instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound()
    {
        GetComponent<AudioSource>().clip = myClip;
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(gameObject);
    }

}
