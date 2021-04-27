using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //attribute
    private float _speed = 5f;
    [SerializeField] //attribute
    private GameObject laserPrefab;
    [SerializeField]
    private float laserOffset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //make current position = new transform.position (0,0,0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space))
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

        /*
        if (transform.position.y >= 4)
        {
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        }

        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }
        */

        // clipping plane to limit player movement
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 4), transform.position.z);

        if (transform.position.x >= 4)
        {
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
        }

        else if (transform.position.x <= -4)
        {
            transform.position = new Vector3(4, transform.position.y, transform.position.z);
        }
    }

    void InstantiateLaser()
    {
        Debug.Log("Space bar pressed.");
        Instantiate(laserPrefab, 
            new Vector3(transform.position.x, transform.position.y + laserOffset, transform.position.z), 
            Quaternion.identity);

        Instantiate(laserPrefab, transform.position + new Vector3(0, laserOffset, 0), Quaternion.identity);
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
