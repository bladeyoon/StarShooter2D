using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject _shield;

    private void Start()
    {
        _shield = GameObject.Find("Shield");
    }

    public void InstantiateShield()
    {
        //Start with Blue Color
        _shield.GetComponent<SpriteRenderer>().color = new Color(0f , 200f, 255f);
    }

    public void ShieldFirstHit()
    {
        //Change Color to Yellow
        _shield.GetComponent<SpriteRenderer>().color = new Color (255f, 200f, 0f);
    }

    public void ShieldSecondHit()
    {
        //Change Color to Red
        _shield.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
    }
}
