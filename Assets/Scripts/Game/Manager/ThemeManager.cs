using CatCooking.Core.Themes;
using CatCooking.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatCooking.Game.Manager
{
    public enum THEME
    {
        THEME_ASIA = 0,
        THEME_USA = 1,
        THEME_ITALY = 2
    }

    public class ThemeManager : Singleton<ThemeManager>
    {
        [SerializeField] Image UIBackground;
        [SerializeField] Image UIFloor;
        [SerializeField] GameObject OConveyor;
        [SerializeField] GameObject OConveyorBelt;
        [SerializeField] GameObject OGateA;
        [SerializeField] GameObject OGateB;
        [SerializeField] GameObject OKitchen;

        private AThemes mTheme;

        public ThemeManager()
        {
            //LoadAsianTheme();
        }

        public override void Init()
        {
            base.Init();
            StartFirtTime();
        }

        public void StartFirtTime()
        {
            LoadAsianTheme();
            SetupEnv();
        }

        public void ChangeTheme(THEME theme)
        {
            switch(theme)
            {
                case THEME.THEME_USA:
                    break;
                case THEME.THEME_ITALY:
                    break;
                default:
                case THEME.THEME_ASIA:
                    LoadAsianTheme();
                    break;
            }

            MotionChangeTheme();
        }

        public void LoadAsianTheme()
        {
            mTheme = new AsianTheme();
            mTheme.Initialize();
        }

        public void MotionChangeTheme()
        {
            SetupEnv();
        }

        public void SetupEnv()
        {
            UIBackground.sprite = mTheme.Background;
            UIFloor.sprite = mTheme.Floor;
            OConveyor.GetComponent<SpriteRenderer>().sprite = mTheme.Conveyor;
            OConveyorBelt.GetComponent<SpriteRenderer>().sprite = mTheme.ConveyorBelt;
            OGateA.GetComponent<SpriteRenderer>().sprite = mTheme.Gate;
            OGateB.GetComponent<SpriteRenderer>().sprite = mTheme.Gate;
            OKitchen.GetComponent<SpriteRenderer>().sprite = mTheme.Kitchen;
        }

        public AThemes GetCurrentTheme()
        {
            return mTheme;
        }
    }
}

