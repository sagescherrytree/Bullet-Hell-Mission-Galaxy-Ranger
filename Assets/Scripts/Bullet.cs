using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Vector3 velocity;
    public float speed;
    public float rotation;
    public float lifeExpectancy;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > lifeExpectancy) {
            Die();
        }
        timer += Time.deltaTime;
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    // void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.tag == "player") {
    //         // Kill the bullet if hit player.
    //         Die();
    //     }
    // }

    // Kill bullet after life expectancy.
    void Die() {
        Destroy(gameObject);
    }

}
