using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 4f;

    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private AudioClip _ExplosionClip;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _SingleLaserPrefab;

    private float _laserOffset = -1.5f;
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private Animator _enemyVFX;

    [SerializeField]
    private bool _isEnemyDying = false;

    void Start()
    {
        _uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("The UI_Manager is NULL.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL.");
        }
        else
        {
            _audioSource.clip = _ExplosionClip;
        }

        _enemyVFX = GetComponent<Animator>();
        {
            if (_enemyVFX == null)
            {
                Debug.LogError("The Enemy Animator is NULL.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject laser = Instantiate(_SingleLaserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
            laser.GetComponent<Laser>().EnemyLaser();
        }
    }
 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (_isEnemyDying == false)
        {
            if (other.tag == "Player")
            {
                Player player = other.GetComponent<Player>();
                if (player != null) //null checking
                {
                    player.Damage();
                }
                _enemyVFX.SetTrigger("OnEnemyDeath");
                _isEnemyDying = true;
                _enemySpeed = 2f;
                _audioSource.Play();
                Destroy(gameObject.GetComponent<Collider2D>());
                Destroy(this.gameObject, 3f); // destroy Enemy
            }

            if (other.tag == "Laser")
            {
                Debug.Log("hit " + other.transform.name);
                _uIManager.AddScore(Random.Range(5, 15));
                Destroy(other.gameObject);
                _enemyVFX.SetTrigger("OnEnemyDeath");
                _isEnemyDying = true;
                _enemySpeed = 2f;
                _audioSource.Play();
                Destroy(gameObject.GetComponent<Collider2D>());
                Destroy(this.gameObject, 3f);
            }
        }
    }
}
