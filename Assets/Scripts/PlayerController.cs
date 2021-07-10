using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private float movementSpeed;
    private float shotPower;
    public AudioClip shotSound;
    public GameObject bullet;
    public Transform pos1;
    public Transform posL;
    public Transform posR;
    public GameObject particleEffect;
    public int hp;
    public int score;
    public Text scoreText;
    public MapLimits Limits;
    AudioSource audioS;
    int power;
	// Use this for initialization
	void Start () 
    {
        power = 1;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
            Destroy(gameObject);
        Movement();
        Shooting();
        scoreText.text = score.ToString();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, Limits.MinimumX, Limits.MaximumX),
                                        Mathf.Clamp(transform.position.y, Limits.MinimumY, Limits.MaximumY),
                                        0.0f);
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, movementSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -movementSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-movementSpeed * Time.deltaTime, 0, 0));
        }
    }
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioS.PlayOneShot(shotSound);
            switch (power)
            {
                case 1:
                    {
                        GameObject newbullet = Instantiate(bullet, pos1.position, transform.rotation);
                        newbullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
                case 2:
                    {
                        GameObject bullet1 = Instantiate(bullet, posL.position, transform.rotation);
                        bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject bullet2 = Instantiate(bullet, posR.position, transform.rotation);
                        bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
                case 3:
                    {
                        GameObject newbullet = Instantiate(bullet, pos1.position, transform.rotation);
                        newbullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject bullet1 = Instantiate(bullet, posL.position, transform.rotation);
                        bullet1.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                        GameObject bullet2 = Instantiate(bullet, posR.position, transform.rotation);
                        bullet2.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
                default:
                    {
                        GameObject newbullet = Instantiate(bullet, pos1.position, transform.rotation);
                        newbullet.GetComponent<Rigidbody>().velocity = Vector3.up * shotPower;
                    }
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "powerUp")
        {
            if (power < 3)
                power++;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "powerDown")
        {
            if(power > 1) 
                power--;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "enemyBullet")
        {
            Destroy(col.gameObject);
            Instantiate(particleEffect, transform.position, transform.rotation);
            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
     
}
