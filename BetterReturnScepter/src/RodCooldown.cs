﻿using System.Collections.Specialized;

namespace BetterReturnScepter
{
    public class RodCooldown
    {
        private byte countdown;
        private bool canWarp;

        public void IncrementTimer()
        {
            // Increment our timer.
            countdown++;
            
            // First, if the timer is above our threshold... 
            if (countdown > 140)
            {
                // We mark that the player can return to the previous sceptre point, and reset the timer.
                canWarp = true;
                countdown = 0;
            }
        }

        public void ResetCountdown()
        {
            canWarp = false;
            countdown = 0;
        }
        
        public byte Countdown
        {

            get { return countdown; }
        }

        public bool CanWarp
        {

            get { return canWarp; }
        }
    }
}