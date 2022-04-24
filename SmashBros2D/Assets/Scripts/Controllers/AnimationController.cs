using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private PlayerManager   manager    ;
        private Animator        animator   ;
        // private SpriteRenderer  renderer   ;

        // Awake is called before the first frame update
        protected virtual void Awake()
        {
            manager    = GetComponent<PlayerManager>();
            animator   = GetComponent<Animator>();
            // renderer   = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetBool("isJumping", !manager.onGround);
            animator.SetFloat("Speed", Mathf.Abs(manager.body.velocity.x));

            if (manager.input.RetrieveMoveInput() > 0f)
            {
                // renderer.flipX = true;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (manager.input.RetrieveMoveInput() < 0f)
            {
                // renderer.flipX = false;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            
            if (manager.input.RetrieveAttackInput())
            {
                animator.SetTrigger("isAttacking");
            }

        }
    }
}

