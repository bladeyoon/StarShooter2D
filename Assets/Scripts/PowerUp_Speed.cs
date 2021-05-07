using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Speed : MonoBehaviour
{
    private float _speed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //get player component
            //then get method that will speed up
            Player player = collision.GetComponent<Player>();
            player.SpeedUpEnabled();
            Destroy(this.gameObject);
        }
    }

}
