using PrototypeGame2D.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float _speedRot = 20.0f;
    private int _direction = 1;
    private float _rotation = 0;

    private float _speedTrans = 1.5f;
    private int _directionTrans = 1;

    // Update is called once per frame
    void Update()
    {
        CharacterRotate();
        CharacterTran();
    }

    private void CharacterTran()
    {
        if (transform.position.x < -3.6f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            _directionTrans = 1;
        }
        if (transform.position.x > 4.4f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _directionTrans = -1;
        }
        transform.Translate(new Vector3(_speedTrans * Time.deltaTime * _directionTrans * GameManager.Instance.LocalTimeScale, 0, 0));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void CharacterRotate()
    {
        if (transform.rotation.z > 0.04f)
        {
            _direction = -1;
        }
        if (transform.rotation.z < -0.04f)
        {
            _direction = 1;
        }
        _rotation = _speedRot * Time.deltaTime * _direction * GameManager.Instance.LocalTimeScale;
        transform.Rotate(new Vector3(0, 0, _rotation));
    }
}
