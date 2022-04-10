using UnityEngine;

namespace smash_bros
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
        public abstract bool  RetrieveJumpInput();
        public abstract bool  RetrieveHoldJumpInput();
        public abstract bool  RetrieveAttackInput();
    }
}