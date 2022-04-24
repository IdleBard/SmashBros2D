using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private PlayerManager   _manager    ;
        private Animator        _animator   ;
        // private SpriteRenderer  renderer   ;

        // Awake is called before the first frame update
        protected virtual void Awake()
        {
            _manager    = GetComponent<PlayerManager>();
            _animator   = GetComponent<Animator>();
            // renderer   = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            _animator.SetBool("isJumping", !_manager.env.onGround);
            _animator.SetFloat("Speed", Mathf.Abs(_manager.body.velocity.x));

            if (_manager.input.RetrieveMoveInput() > 0f)
            {
                // renderer.flipX = true;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (_manager.input.RetrieveMoveInput() < 0f)
            {
                // renderer.flipX = false;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            
            if (_manager.input.RetrieveAttackInput())
            {
                _animator.SetTrigger("isAttacking");
            }

        }
    }
}

