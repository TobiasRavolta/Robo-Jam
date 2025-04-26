using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 movement;
    public Vector2 forward;

    public Rigidbody2D player;

    public float horizontal;

    public GameObject bulletPrefab;

    float currSpeed;
    float playerWalkSpeed;
    float playerSprintSpeed;
    float playerJumpForce;

    public const float gravity = 10;

    public PlayerManager playerManager;

    [Header("Box Cast Variables")]
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerManager.instance != null)
        {
            playerManager = PlayerManager.instance;
        }

        ApplyStats();
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
        horizontal = Input.GetAxisRaw("Horizontal");

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
            player.AddForce(new Vector2(0, playerJumpForce), ForceMode2D.Impulse);
        }

        // Handling sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded())
        {
            currSpeed = playerSprintSpeed;
        }
        else if (isGrounded())
        {
            currSpeed = playerWalkSpeed;
        }

        // Handling shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    // ShootBullet() shoots a bullet in the direction the player is facing if the maximum active bullet count isn't reached already. Adds 1 to the current active bullet count.
    public void ShootBullet()
    {
        if (playerManager.currBulletCount < playerManager.maxBulletCount)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            playerManager.currBulletCount += 1;
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

    public void ApplyStats()
    {
        if (PlayerManager.instance.playerStats != null)
        {
            PlayerStats playerStats = playerManager.playerStats;

            playerStats.stats.TryGetValue(PlayerStatTypes.walkSpeed, out float walkSpeed);
            playerWalkSpeed = walkSpeed;

            playerStats.stats.TryGetValue(PlayerStatTypes.sprintSpeed, out float sprintSpeed);
            playerSprintSpeed = sprintSpeed;

            playerStats.stats.TryGetValue(PlayerStatTypes.jumpForce, out float jumpForce);
            playerJumpForce = jumpForce;

            playerStats.stats.TryGetValue(PlayerStatTypes.size, out float _size);

            gameObject.transform.localScale *= _size;
        }
    }


#if UNITY_EDITOR
private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
#endif
}