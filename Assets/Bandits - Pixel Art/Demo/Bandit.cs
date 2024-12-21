using UnityEngine;

namespace Bandits___Pixel_Art.Demo
{
    public class Bandit : MonoBehaviour
    {
        [SerializeField] private float mSpeed = 4.0f;
        [SerializeField] private float mJumpForce = 7.5f;

        private Animator mAnimator;
        private Rigidbody2D mBody2d;
        private SensorBandit mGroundSensor;
        private bool mGrounded = false;
        private bool mCombatIdle = false;
        private bool mIsDead = false;

        // Use this for initialization
        private void Start()
        {
            mAnimator = GetComponent<Animator>();
            mBody2d = GetComponent<Rigidbody2D>();
            mGroundSensor = transform.Find("GroundSensor").GetComponent<SensorBandit>();
        }

        // Update is called once per frame
        private void Update()
        {
            //Check if character just landed on the ground
            if (!mGrounded && mGroundSensor.State())
            {
                mGrounded = true;
                mAnimator.SetBool("Grounded", mGrounded);
            }

            //Check if character just started falling
            if (mGrounded && !mGroundSensor.State())
            {
                mGrounded = false;
                mAnimator.SetBool("Grounded", mGrounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (inputX < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            mBody2d.velocity = new Vector2(inputX * mSpeed, mBody2d.velocity.y);

            //Set AirSpeed in animator
            mAnimator.SetFloat("AirSpeed", mBody2d.velocity.y);

            // -- Handle Animations --
            //Death
            if (Input.GetKeyDown("e"))
            {
                if (!mIsDead)
                    mAnimator.SetTrigger("Death");
                else
                    mAnimator.SetTrigger("Recover");

                mIsDead = !mIsDead;
            }

            //Hurt
            else if (Input.GetKeyDown("q"))
                mAnimator.SetTrigger("Hurt");

            //Attack
            else if (Input.GetMouseButtonDown(0))
            {
                mAnimator.SetTrigger("Attack");
            }

            //Change between idle and combat idle
            else if (Input.GetKeyDown("f"))
                mCombatIdle = !mCombatIdle;

            //Jump
            else if (Input.GetKeyDown("space") && mGrounded)
            {
                mAnimator.SetTrigger("Jump");
                mGrounded = false;
                mAnimator.SetBool("Grounded", mGrounded);
                mBody2d.velocity = new Vector2(mBody2d.velocity.x, mJumpForce);
                mGroundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                mAnimator.SetInteger("AnimState", 2);

            //Combat Idle
            else if (mCombatIdle)
                mAnimator.SetInteger("AnimState", 1);

            //Idle
            else
                mAnimator.SetInteger("AnimState", 0);
        }
    }
}
