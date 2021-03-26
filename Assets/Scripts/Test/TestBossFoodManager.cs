using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrototypeGame2D.Object;

public class TestBossFoodManager : MonoBehaviour
{
    public Transform[] roads;
    public GameObject Food1;

    private GameObject Food1Obj;
    // Start is called before the first frame update
    void Start()
    {
        Food1Obj = Instantiate(Food1, roads[0].position, Quaternion.identity);
        Food1Obj.GetComponent<FoodInfoSpawn_OneTab>().SetTargetMoving(roads);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
