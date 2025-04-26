using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 movement;
    public Vector2 forward;

    public Rigidbody2D player;

    public float currSpeed;
    public float currJumpForce;

    public float horizontal;

    public GameObject bulletPrefab;

    public const float walkSpeed = 5;
    public const float runSpeed = 8;
    public const float jumpForce = 25;

    public const float gravity = 10;

    [Header("Box Cast Variables")]
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();

        if (isGrounded())
        {
            player.gravityScale = 0;
        }
        else
        {
            player.gravityScale = gravity;
        }

        movement = new Vector3(horizontal, 0, 0);
        player.linearVelocityX = movement.x * currSpeed;

    }

    private void InputCheck()
    {
        // Getting the input for horizontal movement
        horizontal = Input.GetAxis("Horizontal");

        // Determining the direction the character is facing
        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            GetComponent<SpriteRenderer>().flipY = true;
        }

        // Handling vertical movement (jumps)
        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            player.AddForce(new Vector2(0, currJumpForce), ForceMode2D.Impulse);
        }

        // Handling sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded())
        {
            currSpeed = runSpeed;
        }
        else if (isGrounded())
        {
            currSpeed = walkSpeed;
        }

        // Handling shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation); 
        }
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
#endif
}