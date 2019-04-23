using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovment : MonoBehaviour
{
    //controller
    public CharacterController2D controller;
    public Animator anime;

    //Forced gravity
    public float ForcedGravity = 40f;

    //movement speed
    public float Runspeed = 40f;

    // air born speed
    public float Aribornspeed = 20f;

    // horizontalmovement speed
    float Horizontalmove = 40f;

    // Verticalmovement speed
    float Verticalmove = 20f;

    //jump and crouch variable
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {

        Horizontalmove = Input.GetAxisRaw("Horizontal") * Runspeed;
        Verticalmove = Input.GetAxisRaw("Vertical");

        anime.SetFloat("Speed", Mathf.Abs(Horizontalmove));     

        if (Input.GetButtonDown("Jump"))
        {
            //jump mechanic and start of jump animation
            jump = true;
            anime.SetBool("isjumping", true);
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }

    public void OnLanding()
    {
        //stop the jumping animation
        anime.SetBool("isjumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        //stop the crouch animation
        anime.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {

        // movement to character
        controller.Move(Horizontalmove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
