using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceOverlayController : MonoBehaviour
{
    [SerializeField] private float width;
    private readonly Vector3 speedVector = new Vector3(1f, 0f, 0f);
    private Vector3 startPosition;

    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Time.deltaTime * speedVector);
        if (gameObject.transform.position.x - startPosition.x >= width)
        {
            gameObject.transform.position = startPosition;
        }
    }
}
