using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector3 rotation;
    [SerializeField]
    private Ball _ball;
    Vector2 position;
    private float _arrowLength;
    [SerializeField]
    private float _maxArrowLength;
    [SerializeField]
    private float _minArrowLength;

    void Start()
    {
        SetArrowDirectionToZero();
        SetArrowLength(1.0f);
        _ball = GameObject.FindWithTag("ball").GetComponent<Ball>();
    }

    void Update()
    {
        // move with the ball
        position.x = _ball.transform.position.x;
        position.y = _ball.transform.position.y + 0.125f;
        transform.position = position;
        // move with the ball

        if (Input.GetKey(KeyCode.D))
        {
            rotation.z = Mathf.Clamp(rotation.z - 0.5f, -89f, 89f);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotation.z = Mathf.Clamp(rotation.z + 0.5f, -89f, 89f);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _arrowLength += 0.01f;
            if (_arrowLength > _maxArrowLength)
                _arrowLength = _maxArrowLength;
            else if (_arrowLength < _minArrowLength)
                _arrowLength = _minArrowLength;
            transform.localScale = new Vector3(1, _arrowLength, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _arrowLength -= 0.01f;
            if (_arrowLength > _maxArrowLength)
                _arrowLength = _maxArrowLength;
            else if (_arrowLength < _minArrowLength)
                _arrowLength = _minArrowLength;
            transform.localScale = new Vector3(1, _arrowLength, 1);
        }
    }


    public Vector2 coordinates() //for shooting the ball in direction of arrow
    {
        float x = -Mathf.Sin((transform.rotation.eulerAngles.z * Mathf.PI) / 180);
        float y = Mathf.Cos((transform.rotation.eulerAngles.z * Mathf.PI) / 180);
        Vector2 coordinates = new Vector2(x, y);
        return coordinates;
    }

    public float ArrowLength()
    {
        return _arrowLength;
    }

    public void SetArrowLength(float length)
    {
        _arrowLength = length;
        transform.localScale = new Vector3(1, _arrowLength, 1);
    }

    public void SetArrowDirectionToZero()
    {
        rotation = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(rotation);
    }
}
