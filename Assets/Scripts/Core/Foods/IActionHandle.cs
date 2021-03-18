using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Core.Foods
{
    public interface IActionHandle
    {
        void MatchSymbol(string symbol);
    }
}

