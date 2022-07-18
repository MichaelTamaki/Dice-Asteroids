using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject explosionPrefab;
    private int asteroidsCleared = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        asteroidsCleared = 0;
        uiManager.asteroidsClearedUi.SetText(string.Format("Score: {0}", asteroidsCleared));
        GameObject.Instantiate(dicePrefab, Vector3.zero, dicePrefab.transform.rotation);
    }

    public void IncrementAsteroidsCleared(Vector3 explosionPosition)
    {
        GameObject explosionObj = Instantiate(explosionPrefab, explosionPosition, explosionPrefab.transform.rotation);
        StartCoroutine(DeleteExplosion(explosionObj));
        asteroidsCleared++;
        uiManager.asteroidsClearedUi.SetText(string.Format("Score: {0}", asteroidsCleared));
    }

    private IEnumerator DeleteExplosion(GameObject explosionObj)
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(explosionObj);
    }

    public void TriggerGameOver()
    {
        uiManager.gameOverUiObj.SetActive(true);
        uiManager.gameOverScoreUi.SetText(string.Format("Score: {0}", asteroidsCleared));
        audioManager.TriggerGameOverSound();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
