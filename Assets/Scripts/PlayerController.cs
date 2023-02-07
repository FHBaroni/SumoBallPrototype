using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float powerUpStrenght = 15;
    public bool hasPowerUp = false;
    private Rigidbody playerRb;
    public float speed = 0.5f;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);


}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
        }
        hasPowerUp = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.rigidbody;
            Vector3 awayFromPlayer = enemyRb.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerUpStrenght, ForceMode.Impulse); 
            Debug.Log("Collided with the object" + collision.gameObject.name + "with power up set to " + hasPowerUp);
        }
    }
}
