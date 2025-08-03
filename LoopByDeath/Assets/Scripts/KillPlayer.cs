using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPt;

    double numLoops;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !this.gameObject.CompareTag("Player"))
        {

            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            other.gameObject.GetComponent<PlayerMovement>().isDead = true;

            
            StartCoroutine(Respawn());


        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(player, respawnPt.position, Quaternion.identity);
        
    }
}
