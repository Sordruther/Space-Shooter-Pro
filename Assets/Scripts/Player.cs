using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool isSpeedUpActive = false, isTripleShotActive = false, shielded = false;

    private Vector3 laserOffset;
    public float speed = 5f;
    float speedMultiplier = 1.5f;
    [SerializeField]
    float buffTime = 5f;
    private float horizontalInput;
    [SerializeField]
    private int lives = 3;
    private float verticalInput;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject shieldObj;
    private SpawnManager spawnManager;
    private UIManager uiManager;
    [SerializeField]
    private int score;

    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        transform.position = new Vector3 (0,0,0);

        laserOffset = new Vector3(0, 1.01f, 0);

        shieldObj.SetActive(false);

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

        if(isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }
    }

    public void Damage()
    {   
        if (shielded == true)
        {
            shielded = false;
            shieldObj.SetActive(false);
            return;
        }
        
        lives --;

        uiManager.UpdateLives(lives);

        if(lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }
    private IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(buffTime);
        isTripleShotActive = false;
    }

    public void SpeedUpActive()
    {
        isSpeedUpActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedDown());
    }

    public void ShieldActive()
    {
        shieldObj.SetActive(true);
        shielded = true;
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    private IEnumerator SpeedDown()
    {
        yield return new WaitForSeconds(buffTime);
        speed /= speedMultiplier;
        isSpeedUpActive = false;
    }

    

}
