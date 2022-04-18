using UnityEngine;

public class DestroyTimer : MonoBehaviour 
{
    [SerializeField] private float destroyTimer;
    [SerializeField] private GameObject particle;
	
	void FixedUpdate () 
    {
        destroyTimer -= Time.fixedDeltaTime;
        if(destroyTimer <= 0)
        {
            if(particle)
                Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
	}
}
