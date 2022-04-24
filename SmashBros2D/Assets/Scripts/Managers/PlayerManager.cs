using UnityEngine;

namespace SmashBros2D
{   
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerManager : CharacterManager
    {
        [SerializeField] internal PlayerHitResponder hitResponder ;
        
        internal Rigidbody2D     body   ;
        internal GroundCollision ground ;

        internal bool onGround    { get; private set; }
        internal bool onWall      { get; private set; }
        internal bool onRightWall { get; private set; }
        internal bool onLeftWall  { get; private set; }
        internal int  wallSide    { get; private set; }
        internal int  oldWallSide { get; private set; }

        internal bool isAttacking = false;

        // Start is called before the first frame update
        private void Awake()
        {
            body       = GetComponent<Rigidbody2D>();
            ground     = GetComponent<GroundCollision>();
        }

        // Update is called once per frame
        private void Update()
        {
            onGround    = ground.GetOnGround();
            onWall      = ground.GetOnWall();
            wallSide    = ground.GetWallSide();
        }

        public void Attack()
        {
            hitResponder.Attack();
        }


    }
}