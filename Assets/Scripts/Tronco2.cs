using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tronco2 : MonoBehaviour
{
    public float speed = 0.1f;
    public float radius = 0.5f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
       //circular movement around radius
         transform.position = new Vector3(-Mathf.Cos(Time.time * speed) * radius,transform.position.y, Mathf.Sin(Time.time * speed) * radius);
         transform.rotation = Quaternion.Euler(0, Time.time * speed * Mathf.Rad2Deg, 0);
    }
}
