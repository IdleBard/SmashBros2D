using UnityEngine;

namespace SmashBros2D
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "Controller/Input/PlayerController")]
    public class PlayerController : InputController
    {
        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool  RetrieveFallInput()
        {
            return Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0f;
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