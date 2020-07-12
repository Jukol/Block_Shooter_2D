using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScoreOnRestart : MonoBehaviour
{
    public static DestroyScoreOnRestart instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
