using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] //attribute
    private float _speed = 5.5f;

    [SerializeField] //attribute
    private GameObject _singleLaserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private GameObject _shieldPrefab;

    [SerializeField]
    private AudioClip _laserSoundClip;

    [SerializeField]
    private AudioSource _audioSource;

    private float _laserOffset = 1.2f;
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private bool _isBoosterEnabled = false;

    [SerializeField]
    private GameObject _rightEngineDamage, _leftEngineDamage;

    [SerializeField]
    private BoostBar _boostBar;

    [SerializeField]
    private int _boostMultiplier;

    [SerializeField]
    private int _multiplierValue = 3;

    public Shield _shieldScript;

    private int _shieldHits;

    // Start is called before the first frame update
    void Start()
    {
        //make current position = new transform.position (0,0,0)
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

        _uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL.");
        }

        _shieldScript = _shieldPrefab.GetComponent<Shield>();
        if (_shieldScript == null)
        {
            Debug.LogError("Shield script is NULL.");
        }

        _rightEngineDamage.SetActive(false);
        _leftEngineDamage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        UsingBoost();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void PlayerMovement()
    {
        // using InputManager system
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime * _boostMultiplier);

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

    void FireLaser()
    {
        Debug.Log("Space bar pressed.");

        _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_singleLaserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
            }
        _audioSource.clip = _laserSoundClip;
        _audioSource.Play();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Debug.Log("touched laser.");
            Damage();
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _shieldHits++;

            switch (_shieldHits)
            {
                case 1:
                    _shieldScript.ShieldFirstHit();
                    break;
                case 2:
                    _shieldScript.ShieldSecondHit();
                    break;
                case 3:
                    _shieldPrefab.SetActive(false);
                    _isShieldActive = false;
                    break;
            }
            return; //must return; otherwise, player will loose a life after shield is deactivated.
        }

        if (_isShieldActive == false)
        {
            _lives -= 1; //_lives = _lives - 1 //_lives--;
            Debug.Log("Lives = " + _lives);
            _uIManager.UpdateLives(_lives);

            if (_lives == 2)
            {
                _rightEngineDamage.SetActive(true);
            }

            else if (_lives == 1)
            {
                _leftEngineDamage.SetActive(true);
            }

            //lives reach 0, destroy the player
            if (_lives <= 0)
            {
                //communicate with Spawn Manager & Let them know to stop spawning
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject, .25f);
            }
        }
    }

    public void TripleShotEnabled()
    {
        _isTripleShotActive = true;
        StartCoroutine(PowerUpCountDown());
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedUpEnabled()
    {
        //Gain 5 second boostpower
        _boostBar.IncreaseBoostValue(5f);
        _isBoosterEnabled = true;
    }

    public void SpeedUpDisabled()
    {
        _isBoosterEnabled = false;
    }

    public void UsingBoost()
    {  
        if (Input.GetKey(KeyCode.LeftShift) && (_isBoosterEnabled == true))
        {
            _boostMultiplier = _multiplierValue;
            _boostBar.ReduceBoostValue();
        }
        else
        {
            _boostMultiplier = 1;
        }
    }

    public void ShieldEnabled()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);
        _shieldHits = 0;
        //StartCoroutine(ShieldCountDown());
    }

    /*
    IEnumerator ShieldCountDown()
    {
        yield return new WaitForSeconds(10f);
        _isShieldActive = false;
        _shieldPrefab.SetActive(false);
    }
    */
}
