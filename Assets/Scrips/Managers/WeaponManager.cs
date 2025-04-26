using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    // Bullet Properties
    public float travelSpeed;
    public float travelTime;
    public float damage;
    public Sprite bulletSprite;

    public WeaponStats weaponStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
