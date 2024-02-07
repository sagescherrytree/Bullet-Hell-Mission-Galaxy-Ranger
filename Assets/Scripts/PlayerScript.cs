using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    // Translation speed.
    public float translationSpeed;
    // Health.
    private float health = 200f;

    // Start is called before the first frame update
    void Start()
    {

        translationSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f) * translationSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider) 
    {
        // If hitbox hit.
        if (collider.tag == "bullet") {
            health -= 20f;
            Debug.Log("Player HP: " + health);
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();
            Destroy(bullet);
            if (health <= 0f) {
                // Destroy player object.
                Die();
            }
        }
        // If fall over edge.
        if (collider.tag == "game over") {
            Die();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // If boundary hit.
        if (collider.tag == "boundary") {
            // Reset transform position.
            Debug.Log("Boundary surpassed.");
            transform.position = new Vector3(0f, 0.988f, 0f);
        }
    }

    // Kills player object.
    void Die()
    {
        Debug.Log("Game over. Player defeated.");
        Destroy(gameObject);
        // Load game over scene.
        SceneManager.LoadScene("GameOver");
    }
}
