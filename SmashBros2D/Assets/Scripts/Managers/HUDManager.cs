using System;
using UnityEngine;
using UnityEngine.UI;

namespace SmashBros2D
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private Text _timeText         = null ;

        private float _timeInLevel;

        // Start is called before the first frame update
        void Start()
        {
            _timeInLevel = 0;
            UpdateTimeText(_timeInLevel);
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
            
            UpdateTimeText(_timeInLevel);
        }

        private void UpdateTimeText(float timeInLevel)
        {   
            //Sets the text value for the timeText component to whatever the time is and whatever decimal place you want to round to
            // timeText.text = Math.Round(timeInLevel, 2).ToString();
            int cseconds = (int)( timeInLevel * 100f % 100) ;
            int seconds  = (int)( timeInLevel % 60) ;
            int minutes  = (int)( timeInLevel / 60) ;
            _timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + cseconds.ToString("00") ;
        }
    }
}
