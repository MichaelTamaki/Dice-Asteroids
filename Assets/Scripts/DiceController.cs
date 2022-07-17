using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private int sideUp;
    [SerializeField] private float sideLength;
    private bool isMoving = false;
    private Vector3 origPos;
    private Vector3 targetPos; // Used for moving between two positions
    private Vector3 targetRotation;
    private float moveTime = 0f;
    private float moveDuration = 0.25f;

    private void Update()
    {
        if (isMoving)
        {
            moveTime += Time.deltaTime;
            float percentOfTimePassed = Mathf.Min(1f, moveTime / moveDuration);
            gameObject.transform.position = Vector3.Lerp(origPos, targetPos, percentOfTimePassed);

            float overtime = Mathf.Max(0f, moveTime - moveDuration);
            float percentToRotate = (Time.deltaTime - overtime) / moveDuration;
            gameObject.transform.Rotate(percentToRotate * targetRotation);
            if (percentOfTimePassed >= 1f)
            {
                isMoving = false;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            isMoving = true;
            moveTime = 0f;
            origPos = gameObject.transform.position;
            targetPos = origPos + new Vector3(0, 0, sideLength);
            targetRotation = new Vector3(90, 0, 0);
        }
    }

    // The ground has a box trigger that will invoke this function
    public void SetSideUp(int newSideUp)
    {
        sideUp = newSideUp;
    }
}
