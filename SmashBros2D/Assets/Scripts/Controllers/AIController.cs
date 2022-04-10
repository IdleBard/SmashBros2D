using UnityEngine;

namespace smash_bros
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return 1f;
        }

        public override bool RetrieveJumpInput()
        {
            return true;
        }

        public override bool RetrieveHoldJumpInput()
        {
            return true;
        }

        public override bool RetrieveAttackInput()
        {
            return false;
        }
    }
}