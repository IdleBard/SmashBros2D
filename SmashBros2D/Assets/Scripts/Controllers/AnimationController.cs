using UnityEngine;

namespace smash_bros
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private Manager         manager    ;
        private Animator        animator   ;
        // private SpriteRenderer  renderer   ;

        // Awake is called before the first frame update
        protected virtual void Awake()
        {
            manager    = GetComponent<Manager>();
            animator   = GetComponent<Animator>();
            // renderer   = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetBool("isJumping", !manager.onGround);
            animator.SetFloat("Speed", Mathf.Abs(manager.body.velocity.x));

            if (manager.body.velocity.x > 0f)
            {
                // renderer.flipX = true;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (manager.body.velocity.x < 0f)
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

