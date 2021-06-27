using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Shield _shield;

    public void ShieldFirstHit()
    {
        _shield.GetComponent<SpriteRenderer>().color = Color.grey;
    }
    public void ShieldSecondHit()
    {
        //_shield.GetComponent<SpriteRenderer>().color = Color.
    }
}
