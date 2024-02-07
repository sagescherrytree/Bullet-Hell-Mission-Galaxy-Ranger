using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Bullet to spawn.
    public GameObject bullet;
    // Number of bullets.
    public int numBullets;
    // Speed.
    public float speed;
    // Velocity.
    public Vector3 velocity;

    // Different types of bullet patterns.
    [SerializeField] private PatternType pattern;
    
    // Cooldown.
    public float cooldown;
    // Timer for firing rate.
    private float timer = 0f;
    // Timer for attack change intervals.
    private float atkIntervalTimer = 2f;
    // Attack interval.
    // Boss changes attack patterns every 10 seconds.
    private float atkInterval = 2f;
    // Angles for rotation of bullets.
    float[] rotations;
    // Direction vectors for 3D bullets.
    private Vector3[] directions;
    // Spawn position of bullets in an array.
    private Vector3[] spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        rotations = new float[numBullets];
        // Set directions array.
        directions = new Vector3[numBullets];
        // Set spawnPositions array.
        spawnPositions = new Vector3[numBullets];
        if (pattern == PatternType.Random) {
            RandomRotations();
        }
        else if (pattern == PatternType.Spin) {
            SpiralPattern();
        }
        else {
            RadialPattern();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check attack intervals.
        if (atkIntervalTimer <= 0f) {
            // Generate random pattern type.
            int newPattern = Random.Range(1, 4);
            if (newPattern == 1) {
                // Choose straight/radial.
                Debug.Log("Straight pattern bullets chosen.");
                RadialPattern();
            }
            else if (newPattern == 2) {
                Debug.Log("Random pattern bullets chosen.");
                RandomRotations();
            }
            /*
            else if (newPattern == 3) {
                Debug.Log("Random pattern bullets chosen.");
                RandomRotations();
            }*/
            atkIntervalTimer = atkInterval;
        }
        if (timer <= 0f) {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
        atkIntervalTimer -= Time.deltaTime;
    }

    // Radial/Normal Distribution pattern.
    void RadialPattern()
    {
        for (int i = 0; i < numBullets; i++) {
            float fraction = (float) i/ ((float) numBullets);
            float difference = 360f;
            float fractDiff = fraction * difference;
            float radians = fractDiff * Mathf.Deg2Rad;

            Vector3 calcDir = new Vector3(0f, Mathf.Cos(radians), Mathf.Sin(radians));
            calcDir.Normalize();

            rotations[i] = fractDiff;
            directions[i] = calcDir;
            spawnPositions[i] = transform.position + calcDir;
        }
    }

    // Spin/Spiral pattern.
    void SpiralPattern()
    {
        float spiralDensity = 1f;
        float spiralFactor = 0.1f;
        float angleOffset = 45f;
        for (int i = 0; i < numBullets; i++) {
            float angle = i * spiralDensity;
            float radius = spiralFactor * angle;
            angle += angleOffset;

            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 calcDir = new Vector3(x, y, 0f);
            calcDir.Normalize();

            rotations[i] = angle;
            directions[i] = calcDir;
            spawnPositions[i] = transform.position + new Vector3(x, y, 0f);
        }
    }

    // Random rotation per bullet.
    public float[] RandomRotations() 
    {
        for (int i = 0; i < numBullets; i++) {
            rotations[i] = Random.Range(1f, 360f);
        }
        return rotations;
    }

    // Spawn Bullets.
    public GameObject[] SpawnBullets() {
        GameObject[] spawnedBullets = new GameObject[numBullets];
        for (int i = 0; i < numBullets; i++) {
            Vector3 direction = directions[i];
            Vector3 spawnPosition = spawnPositions[i];

            spawnedBullets[i] = Instantiate(bullet, spawnPosition, Quaternion.identity);

            Bullet b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = speed;
            b.velocity = direction * speed;
            b.transform.LookAt(spawnPosition + direction);
        }
        return spawnedBullets;
    }
}
