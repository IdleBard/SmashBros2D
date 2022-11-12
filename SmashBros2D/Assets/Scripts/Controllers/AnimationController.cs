using UnityEngine;

namespace SmashBros2D
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        private PlayerManager   _manager    ;
        private Animator        _animator   ;

        private float nextAttackTime;

        // Awake is called before the first frame update
        void Awake()
        {
            _manager    = GetComponent<PlayerManager>();
            _animator   = GetComponent<Animator>();
        }

        void Start()
        {
            nextAttackTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (_manager.input.RetrieveMoveInput() > 0f)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (_manager.input.RetrieveMoveInput() < 0f)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            _animator.SetBool("isJumping", !( _manager.env.onGround || _manager.env.onWall ));
            _animator.SetFloat("Speed"   , Mathf.Abs(_manager.input.RetrieveMoveInput()));

            if (Time.time >= nextAttackTime)
            {
                if (_manager.input.RetrieveAttackInput())
                {
                    _animator.SetTrigger("isAttacking");
                    nextAttackTime = Time.time + _manager.stats.minNextAttackTime;
                }
            }

        }
    }
}

