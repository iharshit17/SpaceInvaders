using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    public MapLimits limits;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject powerDown;
    [SerializeField] private float spawnTimer;
    private float maxTimer;
    [SerializeField] private float spawnPowerTimer;
    private float maxPowerTimer;
    private int enemynum;
    [SerializeField] private PlayerController player;

    void Start ()
    {
        SpawnEnemy();
        maxTimer = spawnTimer;
        maxPowerTimer = spawnPowerTimer;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        spawnTimer -= Time.fixedDeltaTime;
        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = maxTimer;
        }
        spawnPowerTimer -= Time.fixedDeltaTime;
        if(spawnPowerTimer <= 0)
        {
            SpawnPowers();
            spawnPowerTimer = maxPowerTimer;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limits.MinimumX, limits.MaximumX),
                                         Mathf.Clamp(transform.position.y, limits.MinimumY, limits.MaximumY), 
                                         0.0f);
        GameOver();
    }

    void SpawnEnemy()
    {
        enemynum = Random.Range(1, 4);
        switch (enemynum)
        {
            case 1:
                {
                    Instantiate(enemy1, 
                        new Vector3(Random.Range(limits.MinimumX, limits.MaximumX),
                                    Random.Range(limits.MinimumY, limits.MaximumY), 
                                    0.0f), 
                        enemy1.transform.rotation);

                }
                break;
            case 2:
                {
                    Instantiate(enemy2, 
                        new Vector3(Random.Range(limits.MinimumX, limits.MaximumX),
                                    Random.Range(limits.MinimumY, limits.MaximumY), 
                                    0.0f), 
                        enemy2.transform.rotation);
                }
                break;
            case 3:
                {
                    Instantiate(enemy3,
                        new Vector3(Random.Range(limits.MinimumX, limits.MaximumX),
                                    Random.Range(limits.MinimumY, limits.MaximumY)
                                    , 0.0f),
                        enemy3.transform.rotation);
                }
                break;
        }
    }

    void SpawnPowers()
    {
        int powernum = Random.Range(1, 3);
        switch (powernum)
        {
            case 1:
                {
                    Instantiate(powerUp,
                                new Vector3(Random.Range(limits.MinimumX, limits.MaximumX),
                                            Random.Range(limits.MinimumY, limits.MaximumY),
                                            0.0f), transform.rotation);
                }
                break;
            case 2:
                {
                    Instantiate(powerDown,
                                new Vector3(Random.Range(limits.MinimumX, limits.MaximumX),
                                            Random.Range(limits.MinimumY, limits.MaximumY),
                                            0.0f), transform.rotation);
                }
                break;
        }
    }

    void GameOver()
     {
         if(player.HP <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }
}
