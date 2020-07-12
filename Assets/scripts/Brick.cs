using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    Level level;
    [SerializeField] int health = 1;
    [SerializeField] GameObject prefabAnim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
        AudioManager.instance.Play("ball_brick");
        if (collision.gameObject.tag == "ball" && health <= 0)
         {
            level.subtractBricks();
            Instantiate<GameObject>(prefabAnim, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        
        level = FindObjectOfType<Level>();
        level.countBricks();
    }
}
