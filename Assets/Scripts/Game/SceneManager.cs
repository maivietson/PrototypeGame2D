using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class SceneManager : MonoBehaviour
    {
        private string _message = string.Empty;

        public string message
        {
            get { return _message; }
        }

        public void SendAction(string message)
        {
            _message = message;
        }
    }
}
