using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Move paddle
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(
            Mathf.Clamp(mousePos.x, -7.912f, 7.912f),
            transform.position.y, 
            transform.position.z);
    }

    //Play sound on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.Play("paddle_ball");
    }

}
