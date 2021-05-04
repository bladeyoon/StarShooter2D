using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //attribute
    private float _speed = 5.5f;

    [SerializeField] //attribute
    private GameObject laserPrefab;

    private float laserOffset = 1.2f;
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    //Assign 
    [SerializeField]
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //make current position = new transform.position (0,0,0)
        transform.position = new Vector3(0, 0, 0);

        //
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        //Null check
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            InstantiateLaser();
        }
    }

    void PlayerMovement()
    {
        // using InputManager system
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // clipping plane to limit player movement
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), transform.position.z);

        // moving the player to the other side
        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(-9, transform.position.y, transform.position.z);
        }

        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z);
        }
    }

    void InstantiateLaser()
    {
        Debug.Log("Space bar pressed.");

        _canFire = Time.time + _fireRate;

        Instantiate(laserPrefab, transform.position + new Vector3(0, laserOffset, 0), Quaternion.identity);

        /* Instantiate(laserPrefab, 
             new Vector3(transform.position.x, transform.position.y + laserOffset, transform.position.z), 
             Quaternion.identity);
        */
    }

    public void Damage()
    {
        //when collided with an enemy, lose 1 life.
        _lives -= 1; //_lives = _lives - 1 //_lives--;
        Debug.Log("Lives = " + _lives);
        
        //lives reach 0, destroy the player
        if (_lives <= 0)
        {
            //communicate with Spawn Manager
            //Let them know to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}












// Move an object to right direction in real time
//transform.Translate(Vector3.right * _speed * Time.deltaTime);

//Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
//transform.Translate(direction * _speed * Time.deltaTime);

/*
if (Input.GetKey(KeyCode.D))
{ 
    transform.Translate(Vector3.right * speed * Time.deltaTime);
    //transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
}

if (Input.GetKey(KeyCode.A))
{
    transform.Translate(Vector3.left * speed * Time.deltaTime);
}

if (Input.GetKey(KeyCode.W))
{
    transform.Translate(Vector3.up * speed * Time.deltaTime);
}

if (Input.GetKey(KeyCode.S))
{
    transform.Translate(Vector3.down * speed * Time.deltaTime);
}
*/
