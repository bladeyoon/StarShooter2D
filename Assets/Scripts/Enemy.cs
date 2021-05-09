using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 4f;

    [SerializeField]
    private UIManager _uIManager;

    void Start()
    {
        _uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("The UI_Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move the enemy down at 4 meters per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        
        //destroy when enemy is at -5 or lower in Y direction
        /*
        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
        */

        /*
        //respawn at top with a new random X position
        if (transform.position.y < -5)
        {
            float randomX = Random.Range(-5, 5);
            transform.position = new Vector3(randomX, 5, transform.position.z);
        }
        */
    }
 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null) //null checking to avoid errors/crash when playing.
            {
                player.Damage();
            }

            // destroy Enemy
            Destroy(this.gameObject);

            //other.GetComponent<Player>().Damage();
        }

        if (other.tag == "Laser")
        {
            Debug.Log("hit " + other.transform.name);
            _uIManager.AddScore(Random.Range(5, 15));
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
