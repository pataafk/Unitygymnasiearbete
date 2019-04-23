using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog_script : MonoBehaviour
{

    public Animator anime;

    private float Jumpforce = 300f;                 //Jump power for the enemy frog
    private float Horizontalforce = 40f;            //Horizontal movement
    private bool Jump = false;                      //checking if enemy jump
    private bool Enemyhit = false;                  //if enemy hit the player
    private bool Playerhit = false;                 //if player hit enemy
    private bool EnemyIsAlive = true;               //check if enemy is dead
    private Rigidbody2D m_Rigidbody2D;              //Rigidbody 
    private Vector3 localScale;      // Velocity
    private bool m_grounded;
    private bool m_facingright = true;
    private float DirX;




    private void Start()
    {
        localScale = transform.localScale;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        DirX = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -9f)
            DirX = 1f;
        if (transform.position.x > 9f)
            DirX = -1;
 
    }

    void FixedUpdate()
    {
        m_Rigidbody2D.velocity = new Vector2(DirX * Horizontalforce, m_Rigidbody2D.velocity.y);


        /*bool wasGrounded = m_grounded;
        m_grounded = false;

        if (m_grounded || Jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, Jumpforce));
        }*/
    }

    /* IEnumerator Animation()
     {
         for (float f = 1f; f >= 0f; f -= 0.1f)
         {
             if (f == 0 &&  m_grounded)
             {
                 Jump = true;
                 anime.SetBool("isjumping", true);
             }
             else if (f > 0 && !m_grounded)
             {
                 anime.SetBool("isjumping", false);
             }
             yield return new WaitForSeconds(0f);

         }
     }*/

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (DirX > 0)
            m_facingright = true;
        else if (DirX < 0)
            m_facingright = false;

        if (((m_facingright) && (localScale.x < 0)) || ((!m_facingright) && (localScale.x > 0)))
            localScale.x *= -1f;

        transform.localScale = localScale;
    }
}
