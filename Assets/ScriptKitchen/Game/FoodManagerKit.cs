using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerKit : MonoBehaviour
{
    #region Singleton class: FoodManager

    public static FoodManagerKit Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _timeSpaw;
    [SerializeField] private float _spawRate;

    //private Camera cam;

    //[SerializeField] private FoodMovement food;
    //[SerializeField] private Trajectory trajectory;
    //[SerializeField] private float pushForce = 4f;

    //private bool isDragging = false;

    //private Vector2 startPoint;
    //[SerializeField] private Vector2 endPoint;
    //private Vector2 direction;
    //private Vector2 force;
    //private float distance;

    private void Start()
    {
        //cam = Camera.main;
        //food.DeactivateRb();

        if (!GameManager.Instance.isGameOver)
        {
            StartSpawFood();
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isDragging = true;
        //    OnDragStart();
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    isDragging = false;
        //    OnDragEnd();
        //}

        //if (isDragging)
        //{
        //    OnDrag();
        //}
        if (GameManager.Instance.isGameOver)
        {
            CancelInvoke("SpawFood");
        }
    }

    private Vector2 RandomAngle()
    {
        float angleX = UnityEngine.Random.Range(-9.5f, -9.1f);
        float angleY = UnityEngine.Random.Range(1.7f, 2.0f);

        return new Vector2(angleX, angleY);
    }

    private void StartSpawFood()
    {
        InvokeRepeating("SpawFood", _timeSpaw, _spawRate);
    }

    private void SpawFood()
    {
        Vector2 angleEnd = RandomAngle();
        Vector2 spawPosition = transform.position;
        GameObject foodObject = Instantiate(_foodPrefab, spawPosition, Quaternion.identity) as GameObject;
        foodObject.GetComponent<FoodMovement>().PreMovement(spawPosition, angleEnd);
    }

    public float timeSpaw
    {
        set
        {
            _timeSpaw = value;
        }
    }

    public float spawRate
    {
        set
        {
            _spawRate = value;
        }
    }

    //void OnDragStart()
    //{
    //    food.DeactivateRb();
    //    //startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    //    startPoint = food.pos;

    //    trajectory.Show();
    //}

    //void OnDrag()
    //{
    //    endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    //    distance = Vector2.Distance(startPoint, endPoint);
    //    direction = (startPoint - endPoint).normalized;
    //    force = -direction * distance * pushForce;

    //    Debug.DrawLine(startPoint, endPoint);

    //    //Debug.Log("direction: " + direction);
    //    Debug.Log("startPoint: " + startPoint);
    //    Debug.Log("endPoint: " + endPoint);

    //    trajectory.UpdateDots(food.pos, force);
    //}

    //void OnDragEnd()
    //{
    //    food.ActivateRb();

    //    food.Push(force);

    //    trajectory.Hide();
    //}
}
