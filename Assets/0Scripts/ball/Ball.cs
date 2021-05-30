using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MovePower = 7; // The force added to the ball to move it.
        [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField] public float m_JumpPower = 1.5f; // The force added to the ball when it jumps.
        [SerializeField] Material rollerBallMaterial;
        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;
        [SerializeField]
        public bool isKillerBall=false;
      
        public float killerBallTime=15f;
        public float currentKillerBallTime=0f;
        [SerializeField]
        Animator animator;

        public float rocketBallTime=10f;
        [SerializeField]
        float rocketBallIncreaseSpeed=5;
        public bool isRocketBall=false;
        public float currentRocketBallTime = 0f;
        [SerializeField]
        Material rocketBallMaterial;
        public int score=0;

        float originalMovePower;
        float originalJumpPower;
        float originalMaxVelocity;

        [SerializeField] AudioSource bounceSound;
        [SerializeField] AudioSource dieSound;
        [SerializeField] AudioSource KillerBallSound;
        [SerializeField] AudioSource RocketBallSound;

        [SerializeField] GameObject rocketBallBar;
        [SerializeField] GameObject killerBallBar;
        [SerializeField] GameObject GameOverCanvas;
        [SerializeField] GameObject GameCanvas;
        private void Start()
        {
             originalMovePower = m_MovePower;
             originalJumpPower = m_JumpPower;
             originalMaxVelocity = m_MaxAngularVelocity;
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
            animator = GetComponent<Animator>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        void Update()
        {
            animator = GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.Z))
            {
               SetKillerBall();
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                SetRocketBall();
            }


            if (isKillerBall)
            {
                //animator.SetBool("isKillerBall", true);
                if (currentKillerBallTime <= killerBallTime)
                {
                    currentKillerBallTime += Time.deltaTime;
                }
                else
                {
                    currentKillerBallTime = 0f;
                    ResetkillerBall();
                }
            }

            if (isRocketBall)
            {
                
                if (currentRocketBallTime <= rocketBallTime)
                {
                    currentRocketBallTime += Time.deltaTime;
                }
                else
                {
                    currentRocketBallTime = 0f;
                    ResetRocketBall();    
                }
            }
        }


        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection*m_MovePower);
            }

            // If on the ground and jump is pressed...
            if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
            {
                // ... add force in upwards.
                m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
                PlayAudio(bounceSound);
            }
        }

        public void SetKillerBall()
        {
            if (!isKillerBall)
            {
                isKillerBall = true;
                animator.SetBool("isKillerBall", true);
                PlayAudio(KillerBallSound);
                killerBallBar.SetActive(true);
            }
            else
            {
                currentKillerBallTime = 0;
            }
        }

        void ResetkillerBall()
        {
            if (isKillerBall)
            {
                isKillerBall = false;
                animator.SetBool("isKillerBall", false);
                KillerBallSound.Stop();
                currentKillerBallTime = 0f;
                killerBallBar.SetActive(false);
            }
           
        }

        public void SetRocketBall()
        {
            if (!isRocketBall)
            {
                m_MovePower += rocketBallIncreaseSpeed;
                m_MaxAngularVelocity += rocketBallIncreaseSpeed;
                m_JumpPower += 1;
                GetComponent<MeshRenderer>().material = rocketBallMaterial;
                gameObject.GetComponent<TrailRenderer>().enabled = true;
                isRocketBall = true;
                RocketBallSound.Play();
                currentRocketBallTime = 0f;
                rocketBallBar.SetActive(true);
                
            }
            else
            {
                currentRocketBallTime = 0;
            }
        }
        
        void ResetRocketBall()
        {
            if (isRocketBall)
            {
                m_MaxAngularVelocity = originalMaxVelocity;
                m_MovePower = originalMovePower;
                m_JumpPower = originalJumpPower;
                gameObject.GetComponent<TrailRenderer>().enabled = false;
                GetComponent<MeshRenderer>().material = rollerBallMaterial;
                isRocketBall = false;
                RocketBallSound.Stop();
                rocketBallBar.SetActive(false);
            }
           
        }
        
        /*void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                if (!isKillerBall)
                {
                    gameObject.GetComponent<Health>().ApplyDamage(100);
                }
            }
        }*/

        void Die()
        {
            Health health = gameObject.GetComponent<Health>();
            int lives = gameObject.GetComponent<Health>().numberOfLives;
            if (lives <= 0)
            {
                //Destroy(gameObject);
                Time.timeScale = 0f;
                GameCanvas.SetActive(false);
                GameOverCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

            }
            else
            {
                health.Respawn();
                ResetkillerBall();
                ResetRocketBall();
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            }
        }

        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public void PlayAudio(AudioSource source)
        {
            source.Play();
        }

        void PlayDieSound()
        {
            PlayAudio(dieSound);
        }
    }
}

