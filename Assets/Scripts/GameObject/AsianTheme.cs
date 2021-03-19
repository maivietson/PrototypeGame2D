using CatCooking.Core.Themes;
using CatCooking.Game.Modules.LoadData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Objects
{
    public class AsianTheme : AThemes
    {
        public AsianTheme()
        {
            mNameTheme = "asian";
        }

        public override void Initialize()
        {
            InitTheme();
        }
    }
}

