using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletBehaviour : MonoBehaviour
{
    WeaponManager weaponManager = WeaponManager.instance;

    public float bulletTravelSpeed;
    public float bulletTravelTime;
    public float bulletDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ApplyStats();
        StartCoroutine(TravelTime());
    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
        transform.Translate(Vector2.right * Time.deltaTime * bulletTravelSpeed);
    }

    private void PauseCheck()
    {
        if (GameManager.instance.isPaused || GameManager.instance.isMenuOpen)
        {
            Time.timeScale = 0;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            Time.timeScale = 1;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    private IEnumerator TravelTime()
    {
        yield return new WaitForSeconds(bulletTravelTime);
        Destroy(gameObject);
        PlayerManager.instance.currBulletCount -= 1;
    }

    // Takes the stats out of the statObject ScriptableObject and applies them to local stats.
    private void ApplyStats()
    {
        if (WeaponManager.instance.weaponStats != null)
        {
            WeaponStats weaponStats = weaponManager.weaponStats;

            weaponStats.stats.TryGetValue(WeaponStatTypes.projectileSpeed, out float travelSpeed);
            bulletTravelSpeed = travelSpeed;

            weaponStats.stats.TryGetValue(WeaponStatTypes.travelTime, out float travelTime);
            bulletTravelTime = travelTime;

            weaponStats.stats.TryGetValue(WeaponStatTypes.damageValue, out float damage);
            bulletDamage = damage;

            weaponStats.stats.TryGetValue(WeaponStatTypes.projectileSize, out float size);

            gameObject.transform.localScale *= size;

            GetComponent<SpriteRenderer>().sprite = weaponStats.projectileSprite;
        }
    }

    // private void ApplyDamage();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.tag == "Player" || 
            collision.tag == "Bullet"))
        {
            Destroy(gameObject);
            PlayerManager.instance.currBulletCount -= 1;
        }
    }
}
