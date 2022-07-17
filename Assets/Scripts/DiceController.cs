using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private GameObject diceObj; // Used for showing rotation
    [SerializeField] private GameObject missileIndicatorObj; // Used for showing direction of shot
    [SerializeField] private int sideUp;
    [SerializeField] private float sideLength;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Vector3 bounds;
    private float missileDistance;
    private float missileCooldownTime = 0f;
    private bool isMoving = false;
    private Vector3 origPos;
    private Vector3 targetPos; // Used for moving between two positions
    private Vector3 targetRotation;
    private float moveTime = 0f;
    private readonly float MOVE_DURATION = 0.15f;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        missileDistance = Vector3.Distance(Vector3.zero, missileIndicatorObj.transform.position);
        SetSideUp(2);
    }

    private void Update()
    {
        missileCooldownTime -= Time.deltaTime;
        missileCooldownTime = Mathf.Max(0f, missileCooldownTime);
        UpdateCooldownText();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireMissile();
        }

        if (isMoving)
        {
            ProcessMove();
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartMove(new Vector3(0, 0, sideLength), new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartMove(new Vector3(0, 0, -sideLength), new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            StartMove(new Vector3(-sideLength, 0, 0), new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartMove(new Vector3(sideLength, 0, 0), new Vector3(0, 0, -1));
        }
    }

    private void FireMissile()
    {
        if (missileCooldownTime > 0f)
        {
            return;
        }

        Instantiate(missilePrefab, missileIndicatorObj.transform.position, missileIndicatorObj.transform.rotation);
        missileCooldownTime = sideUp;
        UpdateCooldownText();
    }

    private void StartMove(Vector3 translationVector, Vector3 rotationVector)
    {
        origPos = gameObject.transform.position;
        targetPos = origPos + translationVector;
        targetRotation = rotationVector;

        if (Mathf.Abs(targetPos.x) - 0.01f <= bounds.x && Mathf.Abs(targetPos.z) - 0.01f <= bounds.z)
        {
            isMoving = true;
            moveTime = 0f;
        }
    }

    private void ProcessMove()
    {
        moveTime += Time.deltaTime;
        float percentOfTimePassed = Mathf.Min(1f, moveTime / MOVE_DURATION);
        gameObject.transform.position = Vector3.Lerp(origPos, targetPos, percentOfTimePassed);

        float overtime = Mathf.Max(0f, moveTime - MOVE_DURATION);
        float percentToRotate = (Time.deltaTime - overtime) / MOVE_DURATION;
        diceObj.transform.RotateAround(diceObj.transform.position, targetRotation, percentToRotate * 90);
        if (percentOfTimePassed >= 1f)
        {
            isMoving = false;

            Vector3 newMissileDirection = Vector3.Normalize(targetPos - origPos);
            missileIndicatorObj.transform.localPosition = missileDistance * newMissileDirection;
            missileIndicatorObj.transform.LookAt(9999 * newMissileDirection);
        }
    }

    // The ground has a box trigger that will invoke this function
    public void SetSideUp(int newSideUp)
    {
        sideUp = newSideUp;
        if (uiManager)
        {
            uiManager.sideUpUi.SetText(string.Format("Side up: {0}", sideUp));
        }
    }

    private void UpdateCooldownText()
    {
        uiManager.cooldownUi.SetText(string.Format("Cooldown: {0:#.00}", missileCooldownTime));
    }
}
