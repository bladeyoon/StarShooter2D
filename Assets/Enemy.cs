using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        //move the enemy down at 4 meters per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //respawn at top with a new random X position
        if (transform.position.y < -5)
        {
            float randomX = Random.Range(-5, 5);
            transform.position = new Vector3(randomX, 5, transform.position.z);
        }
    }
 
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit " + other.transform.name);
            Destroy(this.gameObject);

            //other.GetComponent<Player>().Damage();
            Player player = other.GetComponent<Player>();

            if (player != null) //null checking to avoid errors/crash when playing.
            {
                player.Damage();
            }
        }

        if (other.tag == "Laser")
        {
            Debug.Log("hit " + other.transform.name);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
