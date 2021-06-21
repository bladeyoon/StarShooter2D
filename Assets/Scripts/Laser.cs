using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    public bool _isEnemyLaser = false;

    private void Start()
    {

    }

    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }

        else
        {
            MoveDown();
        }
    }
    public void EnemyLaser()
    {
        _isEnemyLaser = true;
        Debug.Log("EnemyLaser is true.");
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 5f)
        {
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        Debug.Log("Move down called out.");
        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
