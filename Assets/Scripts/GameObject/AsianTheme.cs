using CatCooking.Core.Themes;
using CatCooking.Game.Modules.LoadData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.GameObject
{
    public class AsianTheme : AThemes
    {
        public override void InitTheme()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadFoodForTheme()
        {
            TextAsset foodJson = Resources.Load<TextAsset>("themes/asia/dataFood/dataFood.json");
            mMenu = JsonUtility.FromJson<MenuFood>(foodJson.text);
            foreach (Foods food in mMenu.Menus)
            {

            }
        }

        public override void LoadSymbolForTheme()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadTheme()
        {

        }
    }
}

