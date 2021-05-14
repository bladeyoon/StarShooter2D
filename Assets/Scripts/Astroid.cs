using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    float _rotatingSpeed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotatingSpeed * Time.deltaTime);
    }
}
