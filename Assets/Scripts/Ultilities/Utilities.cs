using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
   public enum PowerUpType
    {        
        COMPLETE_CONVEYOR,
        COMPLETE_FOOD,
        COMPLETE_ORDER,
        SLOW_CONVEYOR,
        ONLY_SYMBOL,
        ADD_LIFE
    }

    public enum PowerUpState
    {
        EMPTY,
        CHARGING,
        FULL
    }
}
