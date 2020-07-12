using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCanvasWithLivesAndScore : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "loseScene" || SceneManager.GetActiveScene().name == "winScene")
            Destroy(gameObject);
    }
}
