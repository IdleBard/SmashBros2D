using UnityEngine;

namespace smash_bros
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }

        public override bool RetrieveHoldJumpInput()
        {
            return Input.GetButton("Jump");
        }

        public override bool RetrieveAttackInput()
        {
            return Input.GetButtonDown("Fire1");
        }
    }
}