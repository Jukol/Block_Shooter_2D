using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] 
    int bricks; //counting bricks
    [SerializeField] 
    SceneLoader sceneLoader;
    public void countBricks()
    {
        bricks++;
    }

    public void subtractBricks()
    {
        bricks--;
        
    }

    private void Update()
    {
        Animator lastAnim = FindObjectOfType<Animator>();
        if (bricks <= 0 && lastAnim == null) sceneLoader.LoadNextScene();
    }

}
