using PrototypeGame2D.Core;
using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class PowerUpManager : MonoBehaviour
    {
        #region Singleton class : PowerUpManager
        public static PowerUpManager Instance;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        private bool _completeAllFoodInConveyor;
        private bool _isPowerUpSlowConveyor;

        public bool PowerupCompleteAllFoodInConveyor
        {
            get { return _completeAllFoodInConveyor; }
            set { _completeAllFoodInConveyor = value; }
        }

        public void AddLive()
        {
            GameManager.Instance.PowerupAddLive();
        }

        public void SpawnFoodOnlySymbol()
        {
            FoodSpawn.Instance.PowerupStartSpawnFoodWithOneSymbol();
        }

        public void SlowConveyor()
        {
            StartCoroutine("SlowConveyorWithTime");
        }

        IEnumerator SlowConveyorWithTime()
        {
            int currentLevel = FoodManager.Instance.LevelConveyor;
            FoodManager.Instance.LevelConveyor = (FoodManager.Instance.LevelConveyor - Defination.LEVEL_REDUCE_TO_SLOW_CONVEYOR) > 0 ? (FoodManager.Instance.LevelConveyor - Defination.LEVEL_REDUCE_TO_SLOW_CONVEYOR) : 0;
            _isPowerUpSlowConveyor = true;
            yield return new WaitForSeconds(Defination.TIME_SLOW_CONVEYOR);
            FoodManager.Instance.LevelConveyor = currentLevel;
            _isPowerUpSlowConveyor = false;
        }

        public void CompleteOrder()
        {
            FoodManager.Instance.PowerupCompleteOrder();
        }

        public void CompleteMoreFoodForAllOrder()
        {
            FoodManager.Instance.PowerupCompleteFoodLotsOfAllOrder();
        }

        public void CompleteAllFoodInConveyor()
        {
            StartCoroutine("CompleteAllFoodInConveyorWait");
        }

        IEnumerator CompleteAllFoodInConveyorWait()
        {
            _completeAllFoodInConveyor = true;
            yield return new WaitForSeconds(Defination.TIME_WAIT_COMPLETE_FOOD_IN_CONVEYOR);
            _completeAllFoodInConveyor = false;
        }

        // get area
        public bool IsPowerUpSlowConveyor { get { return _isPowerUpSlowConveyor; } }
    }
}

