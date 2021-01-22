using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo : MonoBehaviour
{
    private string _id;
    private Sprite _image;

    public string id
    {
        get { return _id; }
    }

    public void SetObstacle(string id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

}
