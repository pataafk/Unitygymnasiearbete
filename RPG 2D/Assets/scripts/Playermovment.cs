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

    //combat
    public int health = 3;
    public float invinsible = 2;


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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gem-1"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("holla");
        }
    }


    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(HurtBlinker(hurtTime));
    }

    IEnumerator HurtBlinker(float hurtTime)
    {
        //ignore collision with enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
        foreach (Collider2D collider in CharacterController2D.instance.myColl)
        {
            collider.enabled = false;
            collider.enabled = true;
        }

        //wait for invincebility to end
        yield return new WaitForSeconds(hurtTime);

        //re-eneable collision
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    }

    void Hurt()
    {
        health--;
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);
        else
            TriggerHurt(invinsible);
    }


    void onCollisionEnter2D(Collision2D collision)
    {
        enemy_test enemy = collision.collider.GetComponent<enemy_test>();
        if(enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if ( point.normal.y >= 0.9f)
                {
                    enemy.Hurt();
                }
                else
                {
                    Hurt();
                }
            }

        }
    }
}

