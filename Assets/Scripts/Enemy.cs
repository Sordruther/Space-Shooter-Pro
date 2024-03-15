using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float enemySpeed = 4f;
    private Player _player;
    

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
        
        if(transform.position.y <= -5.65)
        {
            float randomX = UnityEngine.Random.Range(-9, 9); 
            transform.position = new Vector3(randomX, 7.6f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
            
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if(_player != null)
            {
               _player.AddScore(25);
            }
            Destroy(this.gameObject);
        }

    }
}
