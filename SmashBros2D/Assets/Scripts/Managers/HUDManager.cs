using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmashBros2D
{
    public class HUDManager : MonoBehaviour
    {
        //This is a reference to the Text component on the HUD gameobject that will display how much time has passed since the level started
        [SerializeField] private Text timeText = null;
        //This is a float value that represents how long its been since the level started
        private float _timeInLevel;

        // Start is called before the first frame update
        void Start()
        {
            _timeInLevel = 0;
            UpdateTime(_timeInLevel);
        }

        // Update is called once per frame
        void Update()
        {
            // A method that constantly adds to the timeInLevel
            AddTime();
        }

        private void AddTime()
        {
            //Adds time to the timeInLevel float
            _timeInLevel += Time.deltaTime;
            
            UpdateTime(_timeInLevel);
        }

        private void UpdateTime(float timeInLevel)
        {   
            //Sets the text value for the timeText component to whatever the time is and whatever decimal place you want to round to
            // timeText.text = Math.Round(timeInLevel, 2).ToString();
            int cseconds = (int)( timeInLevel * 100f % 100);
            int seconds  = (int)( timeInLevel % 60);
            int minutes  = (int)( timeInLevel / 60);
            timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + cseconds.ToString("00");
        }
    }
}
