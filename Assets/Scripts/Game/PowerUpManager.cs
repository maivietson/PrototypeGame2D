using PrototypeGame2D.Core;
using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame2D.Game
{
    public class PowerUpManager : MonoBehaviour
    {
        public void AddLive()
        {
            GameManager.Instance.AddLive();
        }

        public void SpawnFoodOnlySymbol()
        {
            FoodSpawn.Instance.StartSpawnFoodWithOneSymbol();
        }

        public void SlowConveyor()
        {
            StartCoroutine("SlowConveyorWithTime");
        }

        IEnumerator SlowConveyorWithTime()
        {
            int currentLevel = FoodManager.Instance.LevelConveyor;
            FoodManager.Instance.LevelConveyor = (FoodManager.Instance.LevelConveyor - Defination.LEVEL_REDUCE_TO_SLOW_CONVEYOR) > 0 ? (FoodManager.Instance.LevelConveyor - Defination.LEVEL_REDUCE_TO_SLOW_CONVEYOR) : 0;
            FoodManager.Instance.IsPowerUpSlowConveyor = true;
            yield return new WaitForSeconds(Defination.TIME_SLOW_CONVEYOR);
            FoodManager.Instance.LevelConveyor = currentLevel;
            FoodManager.Instance.IsPowerUpSlowConveyor = false;
        }

        public void CompleteOrder()
        {
            FoodManager.Instance.PowerupCompleteOrder();
        }
    }
}

