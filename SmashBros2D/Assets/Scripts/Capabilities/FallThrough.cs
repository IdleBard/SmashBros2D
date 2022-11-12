using UnityEngine;

namespace SmashBros2D
{
    public class FallThrough : Movement
    {
        // A delegate event that will notify other scripts that subscribe to this event to run a method whenever this delegate is called.
        public delegate void FallThroughPlatformEvent(int playerID) ;
        public static event FallThroughPlatformEvent FallTroughPlatform ;
        
        // Update is called once per frame
        private void Update()
        {
            if (_manager.input.RetrieveFallInput() && _manager.env.onGround)
            {
                FallTroughPlatform(_manager.playerID) ;
            }
        }
    }
}