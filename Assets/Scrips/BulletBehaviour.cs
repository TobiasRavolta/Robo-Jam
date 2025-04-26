using UnityEngine;
using System;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public float travelSpeed;
    public float travelTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TravelTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * travelSpeed);
    }

    private IEnumerator TravelTime()
    {
        yield return new WaitForSeconds(travelTime);
        Destroy(gameObject);
        PlayerManager.instance.currBulletCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.tag == "Player" || collision.tag == "Bullet"))
        {
            Destroy(gameObject);
            PlayerManager.instance.currBulletCount -= 1;
        }
    }
}
