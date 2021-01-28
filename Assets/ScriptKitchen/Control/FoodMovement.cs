using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D col;

    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private float _pushForce = 4f;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;

    private Trajectory trajectory;

    public Vector3 pos
    {
        get
        {
            return transform.position;
        }
    }

    public float pushForce
    {
        get
        {
            return _pushForce;
        }
        set
        {
            _pushForce = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        //DeactivateRb();
    }

    public void PreMovement(Vector2 startPoint, Vector2 endPoint)
    {
        _endPoint = endPoint;
        _startPoint = startPoint;
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = -_direction * _distance * _pushForce;

        Debug.Log("startPoint: " + _startPoint);
        Debug.Log("endPoint: " + _endPoint);

        PlayMovement(_force);
    }

    private void PlayMovement(Vector2 force)
    {
        ActivateRb();
        Push(force);
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
}
