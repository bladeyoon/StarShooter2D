using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField] //TripleShot = 0, SpeedUp = 1, Shield = 2
    private int powerUpID;

    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3;
        //when we leave the screen, destroy this object

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    //OnTriggerEnter
    //Only be collected by the player with tag
    //on collected by player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotEnabled();
                        break;
                    case 1:
                        player.SpeedUpEnabled();
                        break;
                    case 2:
                        player.ShieldEnabled();
                        break;
                    default:
                        Debug.Log("unassigned value");
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            Destroy(this.gameObject);
        }
    }

}
