using CatCooking.Game.Modules.LoadData;
using CatCooking.GameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCooking.Game.Manager
{
    public class Loading : Singleton<Loading>
    {
        [SerializeField] private TextAsset dataJson;

        private MenuFood menu;

        public override void Init()
        {
            base.Init();

            // load data food
            LoadDataFood();
        }

        public void LoadDataFood()
        {
            menu = JsonUtility.FromJson<MenuFood>(dataJson.text);
            foreach(Foods food in menu.Menus)
            {
                List<FoodIngredients> foodIngredients = new List<FoodIngredients>();
                foreach(Ingredients fi in food.Ingredients)
                {
                    FoodIngredients ingredients = new FoodIngredients();
                    Sprite ingredientSprite = Resources.Load<Sprite>("foodSprite/small/" + fi.Image);
                    Sprite iconSprite = Resources.Load<Sprite>("orderSprite/food/" + fi.Icon);
                    ingredients.InitIngredients(fi.ID, food.Name, ingredientSprite, iconSprite, fi.Amount);
                    foodIngredients.Add(ingredients);
                }
            }
        }

        
    }
}

