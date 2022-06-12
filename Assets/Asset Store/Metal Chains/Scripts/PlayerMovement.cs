using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController1 controller;

    public static float horizontalMove = 0f;

    public float runSpeed = 40f;
    public static bool jump = false;
    private bool jumpCheck = false;

    private Rigidbody2D player;

    public AudioSource[] chainSounds;
    private int chainSoundChoice = 0;

    private bool onSwing = false; //Determines if on swing or not


    private void Start()
    {
        horizontalMove = 0f;
        jump = false;
        player = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpCheck = true;
            if (onSwing)
            {
                onSwing = false; //When jumping, you are not on the swing anymore
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }
        else if (!Input.GetButton("Jump") && jumpCheck)
        {
            jump = false;
            jumpCheck = false;
        }

    }




    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);

        if (player.velocity.y < 0)
        {
            //when falling, adds a force of gravity times multiplier

            player.velocity += new Vector2(0, -10) * Time.fixedDeltaTime; //new Vector2 (transform.up.x,transform.up.y)

        }

        else if (player.velocity.y > 0 && jump == false)
        {
            player.AddForce(new Vector2(0, -2000) * Time.fixedDeltaTime);
        }

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chain"))
        {
            chainSoundChoice = Random.Range(0, chainSounds.Length);
            chainSounds[chainSoundChoice].Play();
        }

        if (collision.gameObject.CompareTag("Swing"))
        {
            onSwing = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Swing"))
        {
            //This just determines if the player is trying to move horizontally by pressing the arrow keys etc/
            //It may be different depending on your character controller
            if(Mathf.Abs(horizontalMove) > 1)
            {
                onSwing = false; //stops the process so that the character can move freely
                player.velocity = new Vector2(player.velocity.x, player.velocity.y);
            }
            else
            {
                //This makes the player move in the same direction as the platform and so doesn't fall off
                player.velocity = new Vector2(collision.rigidbody.velocity.x, player.velocity.x);
                onSwing = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Swing"))
        {
            onSwing = false; //Because you are no longer on the swing
        }
    }




}