using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSound : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.Play("wall_ball");
    }
}
