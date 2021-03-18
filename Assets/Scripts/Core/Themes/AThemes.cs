using CatCooking.Game.Modules.LoadData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Core.Themes
{
    public abstract class AThemes
    {
        protected Sprite mBackground;
        protected Sprite mFloor;
        protected Sprite mConveyor;
        protected Sprite mConveyorBelt;
        protected Sprite mGate;

        protected MenuFood mMenu;

        public abstract void InitTheme();
        public abstract void LoadTheme();
        public abstract void LoadFoodForTheme();
        public abstract void LoadSymbolForTheme();
    }
}

