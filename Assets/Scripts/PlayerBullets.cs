using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    // Bullet to spawn.
    public GameObject bullet;
    // Number of bullets.
    public int numBullets;
    // Speed.
    public float speed;
    // Velocity.
    public Vector3 velocity;
    
    // Cooldown.
    public float cooldown;
    // Timer.
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0f) {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "boss") {
            // Kill the bullet if hit player.
            Die();
        }
    }

    // Kill bullet after life expectancy.
    void Die() {
        Destroy(gameObject);
    }

    // Spawn Bullets.
    public GameObject[] SpawnBullets() {
        GameObject[] spawnedBullets = new GameObject[numBullets];
        for (int i = 0; i < numBullets; i++) {
            // First calculate spawn position.
            Vector3 spawnPosition = transform.position;

            spawnedBullets[i] = Instantiate(bullet, spawnPosition, Quaternion.identity);

            Bullet b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = 0f;
            b.speed = speed;
            b.velocity = velocity;
        }
        return spawnedBullets;
    }
}
