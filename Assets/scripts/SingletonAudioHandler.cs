using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAudioHandler : MonoBehaviour
{
    public void findAndPlayAudioManager()
    {
        FindObjectOfType<AudioManager>().Play("button_click");
    }
}
