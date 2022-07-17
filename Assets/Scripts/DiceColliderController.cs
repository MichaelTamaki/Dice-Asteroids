using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceColliderController : MonoBehaviour
{
    [SerializeField] private int sideUp;
    [SerializeField] private DiceController diceController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            diceController.SetSideUp(sideUp);
        }
    }
}
