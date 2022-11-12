using UnityEngine;

namespace SmashBros2D
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
        public abstract bool  RetrieveFallInput();
        public abstract bool  RetrieveJumpInput();
        public abstract bool  RetrieveHoldJumpInput();
        public abstract bool  RetrieveAttackInput();
    }
}