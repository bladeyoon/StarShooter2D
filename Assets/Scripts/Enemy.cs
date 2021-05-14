using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 4f;

    [SerializeField]
    private UIManager _uIManager;

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
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
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
                Destroy(this.gameObject, 3f);
            }
        }
    }
}
