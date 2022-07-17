using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public Vector3 bounceDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Missile") && other.gameObject.GetComponent<DiceMissileController>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
