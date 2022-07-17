using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameObject.Instantiate(dicePrefab, Vector3.zero, dicePrefab.transform.rotation);
    }

    public void TriggerGameOver()
    {
        uiManager.gameOverUiObj.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
