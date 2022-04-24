using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "IdleController", menuName = "InputController/IdleController")]
    public class IdleController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return 0f;
        }

        public override bool RetrieveJumpInput()
        {
            return false;
        }

        public override bool RetrieveHoldJumpInput()
        {
            return false;
        }

        public override bool RetrieveAttackInput()
        {
            return false;
        }
    }
}