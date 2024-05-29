using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float viteza; 
    private Rigidbody2D myCharacter;
    private Vector2 schimba; 

    // Start este apelat inainte de primul frame update
    void Start()
    {
        myCharacter = GetComponent<Rigidbody2D>();

        // verificare caracter kinematic
        if (myCharacter.bodyType != RigidbodyType2D.Kinematic)
        {
            myCharacter.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Update este apelat o data pe frame
    void Update()
    {
        // reseteam vectorul de schimbare la zero
        schimba = Vector2.zero;

        // direcrii de miscare
        schimba.x = Input.GetAxisRaw("Horizontal");
        schimba.y = Input.GetAxisRaw("Vertical");

        // verificam daca exista vreo schimbare in directia de miscare
        if (schimba != Vector2.zero)
        {
            MoveCharacter();
        }
    }

    // functie pentru movement ul caracterului
    void MoveCharacter()
    {
        // calculam noua pozitie pe baza pozitiei curente, directiei de miscare si vitezei
        Vector2 newPosition = myCharacter.position + schimba * viteza * Time.deltaTime;
        myCharacter.MovePosition(newPosition);
    }
}
