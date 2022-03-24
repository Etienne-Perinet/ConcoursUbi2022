using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // movement variables
    private float x;
    private float z;
    private float y;
    Vector3 currentMove;

    // constantes
    public float gravity = -9.8f;
    public float speed = 12f;

    // jump variables
    private bool space_pressed = false;
    public float max_jump_hauteur = 1f;
    private bool isjumping = false;
    private float secondes = 1;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal") * speed;
        z = Input.GetAxis("Vertical") * speed;

        controller.center = new Vector3(0f, 0f, 0f);

        verifiesaut();
        handlejump();

        currentMove = (transform.right * x + transform.up * y + transform.forward * z) * Time.deltaTime;

        controller.Move(currentMove);
    }

    private void FixedUpdate()
    {
        if (controller.collisionFlags == CollisionFlags.Above)
        {
            y = -0.5f;
        }
    }

    // gere les variables de saut

    // fait le saut
    void handlejump()
    {
        if (!isjumping && space_pressed && controller.isGrounded)
        {
            isjumping = true;
            y += max_jump_hauteur;
            space_pressed = false;
        }
    }

    // verifie si le joueur a demander de sauter
    void verifiesaut()
    {

        if (controller.isGrounded)
        {
            isjumping = false;
            secondes = 1;
            y = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                space_pressed = true;
            }
        }
        else
        {
            isjumping = true;
            y += gravity * secondes * Time.deltaTime;
            secondes = secondes + 1 * Time.deltaTime;
        }
    }
}
