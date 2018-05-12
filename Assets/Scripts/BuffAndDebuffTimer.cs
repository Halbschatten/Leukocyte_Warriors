using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class BuffAndDebuffTimer : MonoBehaviour
    {
        private float secondsTimer;
        private bool timerEnabled = false;
        private void Tick()
        {
            secondsTimer = secondsTimer + Time.deltaTime;
        }
        public bool StartTimer(float seconds)
        {
            timerEnabled = true;
            while (seconds < secondsTimer);
            return true;
        }
        private void Update()
        {
            if (timerEnabled == true)
            {
                Tick();
            }
        }
    }
}