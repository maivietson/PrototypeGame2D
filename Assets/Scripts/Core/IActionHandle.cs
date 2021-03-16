using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Core
{
    public interface IActionHandle
    {
        void MatchSymbol(string symbol);
    }
}

