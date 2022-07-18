using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private Collider firstWallCollider;
    private readonly float spawnAngularVelocity = 1.5f;
    private readonly float spawnVelocity = 5f;
    private readonly float bounceForce = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, false);
            Vector3 bounceDirection = collision.gameObject.GetComponent<BarrierController>().bounceDirection;
            GetComponent<Rigidbody>().AddForce(bounceForce  * bounceDirection, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, false);
        }
        else if (collision.gameObject.CompareTag("Dice"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().TriggerGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Inbounds"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, false);
        }
        else if (other.gameObject.CompareTag("Missile"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().IncrementAsteroidsCleared(gameObject.transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetDirectionAndFirstWall(Vector3 direction, Collider collider)
    {
        GetComponent<Rigidbody>().velocity = spawnVelocity * direction;
        GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0f, spawnAngularVelocity), Random.Range(0f, spawnAngularVelocity), Random.Range(0f, spawnAngularVelocity));
        firstWallCollider = collider;
        Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, true);
    }
}
