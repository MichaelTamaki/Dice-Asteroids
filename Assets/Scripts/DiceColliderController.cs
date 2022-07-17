using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceColliderController : MonoBehaviour
{
    public int sideUp;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dice collider trigger");
        if (other.gameObject.CompareTag("Ground"))
        {
            DiceController diceController = GameObject.FindGameObjectWithTag("Dice").GetComponent<DiceController>();
            diceController.SetSideUp(sideUp);
        }
    }
}
