using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Laser : MonoBehaviour
{   
    [SerializeField]
    float laserSpeed = 8;
    void Update()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);

        if(transform.position.y >= 6.7){
            
            Destroy(gameObject);
        }
    }

}
