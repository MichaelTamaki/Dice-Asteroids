using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    private GameObject diceObj;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        diceObj = GameObject.Instantiate(dicePrefab, Vector3.zero, dicePrefab.transform.rotation);
    }
}
