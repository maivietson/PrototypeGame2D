using CatCooking.Game.Modules.LoadData;
using CatCooking.Game.Modules.Symbols;
using CatCooking.Objects;
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
        protected Sprite mKitchen;

        protected string mNameTheme;

        protected List<Dish> mDish;
        protected List<FoodIngredients> mFoodIngredients;

        protected List<string> mListSymbol;

        public AThemes()
        {
        }

        public void InitTheme()
        {
            // Load themes
            LoadTheme();

            // Load food for theme
            LoadFoodForTheme();

            // Load symbol for theme
            LoadSymbolForTheme();
        }

        public void LoadFoodForTheme()
        {
            TextAsset foodJson = Resources.Load<TextAsset>("themes/" + mNameTheme + "/dataFood/dataFood.json");
            MenuFood mMenu = JsonUtility.FromJson<MenuFood>(foodJson.text);
            foreach (FoodsInMenu food in mMenu.Menus)
            {
                List<FoodIngredients> listngredients = new List<FoodIngredients>();
                foreach (Ingredients i in food.Ingredients)
                {
                    FoodIngredients fi = new FoodIngredients();
                    Sprite ingredientSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/ingredient/spawn/" + i.Image);
                    Sprite iconSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/ingredient/icon/" + i.Icon);
                    fi.InitIngredients(i.ID, food.Name, ingredientSprite, iconSprite, i.Amount);
                    mFoodIngredients.Add(fi);
                    listngredients.Add(fi);
                }

                Dish dish = new Dish();
                Sprite dishOrderSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/dish/" + food.Image);
                dish.InitDishOrder(food.ID, food.Name, food.TimeOrder, food.PriceOrder, food.PriceMissingOrder, dishOrderSprite, listngredients);

                mDish.Add(dish);
            }
        }

        public void LoadTheme()
        {
            mBackground = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/background");
            mFloor = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/floor");
            mConveyor = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/conveyor");
            mConveyorBelt = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/conveyorBelt");
            mGate = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/gate");
            mKitchen = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/kitchen");
        }

        public void LoadSymbolForTheme()
        {
            TextAsset symbolJson = Resources.Load<TextAsset>("themes/" + mNameTheme + "/dataSymbol/dataSymbol.json");
            Symbols symbols = JsonUtility.FromJson<Symbols>(symbolJson.text);
            foreach (string sy in symbols.AllSymbols)
            {
                mListSymbol.Add(sy);
            }
        }

        public abstract void Initialize();

        // get area
        public Sprite Background { get { return mBackground; } }
        public Sprite Floor { get { return mFloor; } }
        public Sprite Conveyor { get { return mConveyor; } }
        public Sprite ConveyorBelt { get { return mConveyorBelt; } }
        public Sprite Gate { get { return mGate; } }
        public Sprite Kitchen { get { return mKitchen; } }

    }
}

