using UnityEngine;

using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;



public class CharacterController1 : MonoBehaviour

{

    [SerializeField] public static float m_JumpForce = 750f;                          // Amount of force added when the player jumps.

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;

    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    



    public static float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    public static bool m_Grounded;            // Whether or not the player is grounded.

    private Rigidbody2D m_Rigidbody2D;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private Vector3 m_Velocity = Vector3.zero;



    [Header("Events")]

    [Space]



    public UnityEvent OnLandEvent;



    [System.Serializable]

    public class BoolEvent : UnityEvent<bool> { }


    

    private bool jumpDelay = false;

    private void Awake()

    {

        m_Rigidbody2D = GetComponent<Rigidbody2D>();



        if (OnLandEvent == null)

            OnLandEvent = new UnityEvent();

        k_GroundedRadius = 0.2f;
    }

    private void Start()
    {
        m_Grounded = false;
        m_JumpForce = 750f;
    }



    private void FixedUpdate()

    {
        bool wasGrounded = m_Grounded;

        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)

        {

            if (colliders[i].gameObject != gameObject)

            {

                m_Grounded = true;
                
            }

        }

    }





    public void Move(float move, bool jump)

    {
        
        //only control the player if grounded or airControl is turned on

        if (m_Grounded || m_AirControl)

        {

            // Move the character by finding the target velocity

            
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            // And then smoothing it out and applying it to the character

            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            
            // If the input is moving the player right and the player is facing left...

            if (move > 0 && !m_FacingRight)

            {

                // ... flip the player.

                Flip();

            }

            // Otherwise if the input is moving the player left and the player is facing right...

            else if (move < 0 && m_FacingRight)

            {

                // ... flip the player.

                Flip();

            }

        }

        // If the player should jump...

        if (m_Grounded && jump && jumpDelay == false)

        {

            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,0);

            m_Grounded = false;
            
            m_Rigidbody2D.AddForce(transform.up *m_JumpForce); //new Vector2(0f, m_JumpForce) * 
   
            jumpDelay = true;

            StartCoroutine(Delay());
        }


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        jumpDelay = false;
    }



    private void Flip()

    {
        // Switch the way the player is labelled as facing.

        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180,0);
    }

    
}