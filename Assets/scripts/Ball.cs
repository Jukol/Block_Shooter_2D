using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    private Vector2 _ballStartForce; 
    private int _score;
    [SerializeField] 
    private Paddle _paddle;
    bool _hasStarted;
    [SerializeField]
    float constantSpeed;
    
    ////----Ball stuck fields----
    //bool ySpeedZero;
    //bool xSpeedZero;
    //float ballStuckTimer = 3.0f;
    //bool startTimer;
    ////----Ball stuck fields----

    [SerializeField]
    private Arrow _arrow;

    int lives;

    void Start()
    {
        Cursor.visible = false;
        LockBallToPaddle();
        _score = GameObject.Find("ScoreText").GetComponent<Score>().score;
        _rb = GetComponent<Rigidbody2D>();
        _arrow = GameObject.FindWithTag("Arrow").GetComponent<Arrow>();
        _arrow.gameObject.SetActive(true);
        lives = GameObject.Find("LivesText").GetComponent<Lives>().lives;
        constantSpeed = 7.0f;
        _arrow.SetArrowLength(1.0f);
        
    }
    public void LockBallToPaddle ()
    {
        if (_paddle == null)
        {
            _paddle = FindObjectOfType<Paddle>();
        }

        Vector2 startPosition = new Vector2(
            _paddle.transform.position.x,
            _paddle.transform.position.y + 0.5f);
        transform.position = startPosition;
    }

    public void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            _hasStarted = true;
            _ballStartForce = _arrow.coordinates() * constantSpeed;            
            GetComponent<Rigidbody2D>().velocity = _ballStartForce;
            _arrow.gameObject.SetActive(false);
        }
    }
    
 

    void Update()
    {
        if (_hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }

        //----Tying to arrow----
        if (
            (Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.RightArrow) || 
            Input.GetKeyDown(KeyCode.UpArrow) || 
            Input.GetKeyDown(KeyCode.DownArrow)) 
            && !_hasStarted)
        {
            _arrow.gameObject.SetActive(true);
        }
        
        constantSpeed = 7 * _arrow.ArrowLength();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "brick")
        {
            _score++;
            GameObject.Find("ScoreText").GetComponent<Score>().score = _score;
            
        }

        if (collision.gameObject.tag == "Floor")
        {
            _hasStarted = false;
            _arrow.SetArrowLength(1.0f);
            _arrow.SetArrowDirectionToZero();
            _arrow.gameObject.SetActive(true);
            constantSpeed = 7.0f;

            lives--;
            GameObject.Find("LivesText").GetComponent<Lives>().lives = lives;
            if (lives >= 1)
                return;
            else
                SceneManager.LoadScene("loseScene");
        }
    }

}
