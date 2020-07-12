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
    private Vector2 _currentBallSpeed;
    private Rigidbody2D _rb;
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
        //----Tying to arrow----

        //_currentBallSpeed = GetComponent<Rigidbody2D>().velocity;
        //_rb.velocity = constantSpeed * (_rb.velocity.normalized); //keeping constant speed

        
        ////----Ball stuck timer procedure----
        //if (_currentBallSpeed.y == 0 && startTimer == false && _hasStarted == true)
        //    startTimer = true;
        //if (startTimer == true)
        //    ballStuckTimer -= Time.deltaTime;
        //if (ballStuckTimer <= 0 && _currentBallSpeed.y == 0 && _hasStarted == true)
        //{
        //    startTimer = false;
        //    ySpeedZero = true;
        //}
        //if (_currentBallSpeed.x == 0 && startTimer == false)
        //    startTimer = true;
        //if (startTimer == true)
        //    ballStuckTimer -= Time.deltaTime;
        //if (ballStuckTimer <= 0 && _currentBallSpeed.y == 0 && _hasStarted == true)
        //{
        //    startTimer = false;
        //    xSpeedZero = true;
        //}
        ////----Ball stuck timer procedure----
        
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

        ////----Getting out of eternal cycling----
        //if (collision.gameObject.tag == "walls_or_ceiling" && ySpeedZero) 
        //{
        //    if (transform.position.y >= 0) //checking in which part of screen the ball is: >= 0 => upper
        //    {
        //        _rb.AddForce(new Vector2(_currentBallSpeed.x, -0.1f), ForceMode2D.Impulse);
        //        ySpeedZero = false;
        //    } else
        //    {
        //        _rb.AddForce(new Vector2(_currentBallSpeed.x, 0.1f), ForceMode2D.Impulse);
        //        ySpeedZero = false;
        //    }
        //} else if (collision.gameObject.tag == "walls_or_ceiling" && xSpeedZero) 
        //{
        //    if (transform.position.x >= 0) // left or right half of screen
        //    {
        //        _rb.AddForce(new Vector2(-0.1f, _currentBallSpeed.y), ForceMode2D.Impulse);
        //        xSpeedZero = false;
        //    } else
        //    {
        //        _rb.AddForce(new Vector2(0.1f, _currentBallSpeed.y), ForceMode2D.Impulse);
        //        xSpeedZero = false;
        //    }
        // }
        ////----Getting out of eternal cycling----
    }

}
