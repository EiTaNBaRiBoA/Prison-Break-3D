using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Player Movement
    public CharacterController controller;
    private float speed= 6f;
    private float gravity =-30f;
    private float groundDistance = 0.4f;
    private float jumpHeight = 1f;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    #endregion
    #region Player Crouch
    private float playerOriginalHeight;
    private float playerCrouchHeight = 0.01f;
    public bool isCrouch;
    #endregion
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerOriginalHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.LeftControl)) {isCrouch = !isCrouch;}
        if(Input.GetKeyUp(KeyCode.LeftControl)) {isCrouch = !isCrouch;}
        Crouch();
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Crouch()
    {
        controller.height = isCrouch ? playerCrouchHeight:  playerOriginalHeight;
        controller.radius = isCrouch ? 0.33f : 0.6f;
        speed = isCrouch ? 3f : 6f;
    }
}