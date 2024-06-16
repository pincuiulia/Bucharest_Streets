using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float viteza;
    private Rigidbody2D myCharacter;
    private Vector2 schimba; // directie de miscare
    private Animator animator; // referinta pentru animator

    // Start este apelat inainte de primul frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myCharacter = GetComponent<Rigidbody2D>();

        // ma asigur ca rigitbody2d este dynamic
        if (myCharacter.bodyType != RigidbodyType2D.Dynamic)
        {
            myCharacter.bodyType = RigidbodyType2D.Dynamic;
        }

        // setam schimbarea directiei la 0
        schimba = Vector2.zero;
        myCharacter.gravityScale = 0; // gravitatie 0 pentru caracter
    }

    // Update este apelat o data pe frame
    void Update()
    {
        // setam directiile de miscare
        schimba.x = Input.GetAxisRaw("Horizontal");
        schimba.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationMove(); 
    }

    // FixedUpdate este apelat la un interval fix de frame-uri pentru calcule fizice
    //void FixedUpdate()
    //{
    //    // verificam daca exista vreo schimbare in directia de miscare
    //    if (schimba != Vector2.zero)
    //    {
    //        MoveCharacter();
    //        animator.SetFloat("moveX", schimba.x);
    //        animator.SetFloat("moveY", schimba.y);
    //        animator.SetBool("moving", true);
    //    }
    //    else
    //    {
    //        // oprim caracterul atunci cand nu i dam vreun input
    //        myCharacter.velocity = Vector2.zero;
    //        animator.SetBool("moving", false);
    //    }
    //}

    void UpdateAnimationMove()
    {
        if (schimba != Vector2.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", schimba.x);
            animator.SetFloat("moveY", schimba.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // oprim caracterul atunci cand nu i dam vreun input
            myCharacter.velocity = Vector2.zero;
            animator.SetBool("moving", false);
        }
    }

    // functie pentru movement-ul caracterului
    void MoveCharacter()
    {
        // setam viteza caracterului pe baza directiei de miscare si vitezei
        myCharacter.velocity = schimba * viteza;
    }
}
