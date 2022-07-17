using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMissileController : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime * Vector3.forward);        
    }
}
