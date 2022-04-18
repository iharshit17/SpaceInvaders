using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public float speed;
    public float changeTimer;
    public int scoreReward;
    float maxTimer;
    public MapLimits limits;
    public GameObject particleEffect;
    public GameObject bullet;
    public bool canShoot;
    public Transform bulletPos;
    public float shotPower;
    float shotTimer;
    float maxshotTimer;
    private int _hp = 100;
    public int HP
    {
        get => _hp;
        set
        {
            if (_hp <= 0)
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber < 30) Instantiate(powerup, transform.position, powerup.transform.rotation);
                if (randomNumber > 80) Instantiate(powerdown, transform.position, powerdown.transform.rotation);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().score += scoreReward;
                Destroy(gameObject);
            }
            _hp = value;
        }
    }
    public GameObject powerup;
    public GameObject powerdown;
    Rigidbody rig;
    public bool changeDirection;
    // Use this for initialization

	void Start () {
        rig = GetComponent<Rigidbody>();
        maxTimer = changeTimer;
        shotTimer = Random.Range(1,5);
        maxshotTimer = shotTimer;
	}
	
	// Update is called once per frame
	void Update () {
        

        Movement();
        if (canShoot)
        {
            Shooting();
        }
        SwitchTimer();
        if (transform.position.x == limits.MinimumX || transform.position.x == limits.MaximumX)
            switchDir(changeDirection);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, limits.MinimumX, limits.MaximumX),
            Mathf.Clamp(transform.position.y,limits.MinimumY, limits.MaximumY),0.0f);
	}

    void Movement()
    {
        if (changeDirection)
        {
            rig.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0.0f);
           
        }
        else
        {
            rig.velocity = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0.0f);
           
        }
    }

    void SwitchTimer()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            switchDir(changeDirection);
            changeTimer = maxTimer;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "friendlyBullet")
        {
            Destroy(col.gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
            HP--;
            
        }
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().HP--;
            Instantiate(particleEffect, transform.position, transform.rotation);
            HP--;
            if (HP <= 0)
            {
                col.gameObject.GetComponent<PlayerController>().score += scoreReward;
                Destroy(gameObject);
            }
        }
    }

    bool switchDir(bool dir)
    {
        if (dir)
            changeDirection = false;
        else
            changeDirection = true;
        return changeDirection;
    }
    void Shooting()
    {
        shotTimer -= Time.deltaTime;
        if(shotTimer <= 0)
        {
            GameObject newBullet = Instantiate(bullet, bulletPos.position, bullet.transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = Vector3.up * -shotPower;
            shotTimer = maxshotTimer;
        }
    }
}
