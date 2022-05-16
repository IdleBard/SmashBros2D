using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "AIController", menuName = "Controller/Input/AIController")]
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