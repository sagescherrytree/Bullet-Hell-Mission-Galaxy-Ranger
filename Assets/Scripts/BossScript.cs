using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    // Params fixed for boss.
    // Hp.
    private float health = 2000f;
    // Random movement params.
    private Vector3 originalPos;
    public float randomMoveInterval;
    public float randomMoveSpeed;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= randomMoveInterval) {
            // Move randomly.
            transform.position += RandomMovement();
            // Reset timer.
            timer = 0f;
        }
    }

    // Boss movement.
    Vector3 RandomMovement()
    {
        Vector3 randomDir = Random.insideUnitCircle;
        Vector3 destination = originalPos + new Vector3(randomDir.x, randomDir.y, 0f) * 2f;
        Vector3 dir = destination - transform.position;
        dir.Normalize();
        return dir;
    }
    
    // Everytime boss is hit, take away 50f HP.
    void OnTriggerEnter (Collider collider) 
    {
        if (collider.tag == "player bullet") {
            // Subtract HP from boss.
            Debug.Log("Boss HP: " + health);
            health -= 20f;
            // // Destroy bullet object.
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();
            Destroy(bullet);
            if (health <= 0f) {
                // Destroy boss.
                Die();
            }
        }
    }

    // Boss death.
    void Die()
    {
        Debug.Log("Congratulations! You have slayed the boss.");
        Destroy(gameObject);
        // Transition to game over scene.
        SceneManager.LoadScene("VictoryScene");
    }
}
