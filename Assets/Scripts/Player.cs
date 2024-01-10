using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 laserOffset;
    public float speed = 5f;
    private float horizontalInput;
    [SerializeField]
    private int lives = 3;
    private float verticalInput;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField]
    private GameObject laserPrefab;
    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        transform.position = new Vector3 (0,0,0);

        laserOffset = new Vector3(0, 1.01f, 0);

        if(spawnManager == null)
        {
            Debug.Log("Spawn Manager is Null");
        }
    }

    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire){
        
            Shoot();
        }
        
    }

    void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3 (transform.position.x, Mathf.Clamp(transform.position.y, -4f, 6), 0);
        
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3 (-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3 (11.3f, transform.position.y, 0);
        }
    }
    void Shoot()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
    }

    public void Damage()
    {
        lives --;

        if(lives < 1)
        {
            spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }
}
