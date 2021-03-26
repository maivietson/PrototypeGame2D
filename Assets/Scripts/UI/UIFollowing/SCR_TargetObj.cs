using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TargetObj : MonoBehaviour
{
    Vector3 offsetPos;
    float startY;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y + FollowGameObject.instance._radius, transform.position.z);
        offsetPos = transform.position;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        offsetPos.y = startY + FollowGameObject.instance._radius * Mathf.Sin(2 * Time.time);
        transform.position = offsetPos;
    }
    

}
