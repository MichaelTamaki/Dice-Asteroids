using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private Collider firstWallCollider;
    private readonly float maxTorque = 1.5f;
    private readonly float maxForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, false);
            Vector3 bounceDirection = collision.gameObject.GetComponent<BarrierController>().bounceDirection;
            GetComponent<Rigidbody>().AddForce(maxForce * bounceDirection, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Dice"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().TriggerGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetDirectionAndFirstWall(Vector3 direction, Collider collider)
    {
        GetComponent<Rigidbody>().AddForce(maxForce * direction, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0f, maxTorque), Random.Range(0f, maxTorque), Random.Range(0f, maxTorque)), ForceMode.Impulse);
        firstWallCollider = collider;
        Physics.IgnoreCollision(GetComponent<Collider>(), firstWallCollider, true);
    }
}
