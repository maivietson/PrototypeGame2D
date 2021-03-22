using PrototypeGame2D.Core;
using PrototypeGame2D.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeGame2D.Game
{
    public enum THEME
    {
        THEME_JAPAN = 0,
        THEME_USA = 1,
        THEME_ITALY = 2
    }

    public class ThemesManager : MonoBehaviour
    {
        #region Singleton class: ThemeManager
        public static ThemesManager Instance;

        private void Awake()
        {
           if(Instance == null)
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField] private Image mBackground;
        [SerializeField] private Image mFloor;
        [SerializeField] private GameObject mConveyor;
        [SerializeField] private GameObject mConveyorBelt;
        [SerializeField] private GameObject mGateA;
        [SerializeField] private GameObject mGateB;
        [SerializeField] private GameObject mKitchen;

        private string mNameTheme;

        private List<FoodOrder> mListDishMenu = new List<FoodOrder>();

        private List<string> mListSymbol;

        private bool bCompleteLoadTheme;

        public bool CreateTheme(THEME theme)
        {
            mListDishMenu.Clear();
            switch(theme)
            {
                case THEME.THEME_USA:
                    mNameTheme = "usa";
                    break;
                case THEME.THEME_ITALY:
                    mNameTheme = "italy";
                    break;
                case THEME.THEME_JAPAN:
                default:
                    mNameTheme = "asia";
                    break;
            }
            InitTheme();

            return true;
        }

        public void InitTheme()
        {
            // Load themes
            LoadTheme();

            // Load food for theme
            LoadFoodForTheme();

            // Load symbol for theme
            LoadSymbolForTheme();

            bCompleteLoadTheme = true;
        }

        public void LoadFoodForTheme()
        {
            TextAsset foodJson = Resources.Load<TextAsset>("themes/" + mNameTheme + "/dataFood/dataFood");
            Menus menusFood = JsonUtility.FromJson<Menus>(foodJson.text);
            foreach (Orders od in menusFood.OrdersFood)
            {
                List<FoodInfo> foodResource = new List<FoodInfo>();
                foreach (ResourceFood rf in od.ResourceFoodOrder)
                {
                    FoodInfo fi = new FoodInfo();
                    List<string> symbol = new List<string>();
                    Sprite ingredientSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/ingredient/spawn/" + rf.Image);
                    Sprite iconSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/ingredient/icon/" + rf.Icon);
                    fi.SetFoodInfo(rf.ID, od.Name, ingredientSprite, iconSprite, rf.Amount, symbol);
                    foodResource.Add(fi);
                }

                FoodOrder fo = new FoodOrder();
                Sprite dishOrderSprite = Resources.Load<Sprite>("themes/" + mNameTheme + "/sprites/dish/" + od.Image);
                fo.Name = od.Name;
                fo.SetOrderFood(od.Name, od.TimeOrder, od.PriceOrder, od.PriceMissingOrder, dishOrderSprite, foodResource);

                mListDishMenu.Add(fo);
            }
        }

        public void LoadTheme()
        {
            Sprite background = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/background");
            mBackground.GetComponent<Image>().sprite = background;

            Sprite floor = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/floor");
            mFloor.GetComponent<Image>().sprite = floor;

            Sprite conveyor = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/conveyor");
            mConveyor.GetComponent<SpriteRenderer>().sprite = conveyor;

            Sprite conveyorBelt = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/conveyorBelt");
            mConveyorBelt.GetComponent<SpriteRenderer>().sprite = conveyorBelt;

            Sprite gate = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/gate");
            mGateA.GetComponent<SpriteRenderer>().sprite = gate;
            mGateB.GetComponent<SpriteRenderer>().sprite = gate;

            Sprite kitchen = Resources.Load<Sprite>("themes/" + mNameTheme + "/env/kitchen");
            mKitchen.GetComponent<SpriteRenderer>().sprite = kitchen;
        }

        public void LoadSymbolForTheme()
        {
            Symbols.LoadSymbolForTheme(mNameTheme);
        }

        // get area

        public List<string> Symbol { get { return mListSymbol; } }

        public List<FoodOrder> ListDishMenu { get { return mListDishMenu; } }

        public bool CompleteLoadTheme { get { return bCompleteLoadTheme; } }
    }
}

