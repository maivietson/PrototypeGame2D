using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognizer;
using PrototypeGame2D.Game;

namespace PrototypeGame2D.Object
{
    public class FoodInfoSpawn_OneTab : MonoBehaviour
    {
        public List<GesturePattern> gestures;
        public GameObject UIPrefabs;
        public float speed = 3;
        private string _id;
        private Sprite _image;
        //[SerializeField] private List<string> _symbol;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Sprite Image
        {
            get { return _image; }
            set { _image = value; }
        }
        

        public void SetFoodSpawn(string id, Sprite image)
        {
            _id = id;
            _image = image;
            
        }

        public void InitSymbol()
        {
            //foreach (string sb in _symbol)
            //{
            //    Sprite symbolSprite = Resources.Load<Sprite>("symbol/" + sb);
            //    transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = symbolSprite;
            //}
        }

        private void Start()
        {

        }

        private void Update()
        {
            //if (!GameManager.Instance.isGameOver)
            //{
            //    if (PowerUpManager.Instance.PowerupCompleteAllFoodInConveyor)
            //    {
            //        _symbol.Clear();
            //        CompleteFoodResource();
            //    }
            //    else
            //    {
            //        string message = GameManager.Instance.message;
            //        if (message.Length > 0)
            //        {
            //            if (_symbol.Count > 0)
            //            {
            //                //Debug.Log("FoodManager: message: " + message + " _symbol size: " + _symbol.Count);
            //                HandleSymbol(message);
            //            }
            //            if (_symbol.Count == 0)
            //            {
            //                //Debug.Log("FoodManager: _symbol size: " + _symbol.Count);
            //                CompleteFoodResource();
            //            }

            //        }
            //    }
            //}
            MoveBehavior();
        }

        private void HandleSymbol(string message)
        {
            //var result = _symbol.Where(i => i.Equals(message)).FirstOrDefault();
            //if (result != null)
            //{
            //    Debug.Log("FoodManager: message: " + message + " symbol: " + result + " _symbol size: " + _symbol.Count);
            //    _symbol.Remove(result);
            //    GameManager.Instance.message = "";
            //}
        }

        private void CompleteFoodResource()
        {
            FoodManager.Instance.HandleFood(_id);
            GameManager.Instance.message = "";
            Destroy(gameObject);
        }

     
        public void CreateBossInfo(List<GesturePattern> bossGesture)
        {
            gestures = new List<GesturePattern>(bossGesture);

        }


        #region testArea

        private Transform[] pointMovement;
        public void SetTargetMoving(Transform[] points)
        {
            pointMovement = points;
        }
        public void MoveBehavior()
        {
            transform.localPosition = Vector3.Lerp(pointMovement[0].position, pointMovement[3].position, Time.deltaTime * speed);
            if(Vector3.Distance(transform.position, pointMovement[1].position) < 0.001f)
            {

            }
            else if(Vector3.Distance(transform.position, pointMovement[2].position) < 0.001f)
            {

            }
            else if(Vector3.Distance(transform.position, pointMovement[3].position) < 0.001f)
            {

            }
        }

        #endregion

    }
}
