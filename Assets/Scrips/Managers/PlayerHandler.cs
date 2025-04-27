using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int maxBulletCount;
    public int currBulletCount;


    // Player Stats
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;

    public PlayerStats playerStats;

    // Awake is called once before the first execution of Update or Start after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
