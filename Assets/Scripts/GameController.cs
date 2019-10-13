using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private Vector3 _enemyStartPoint = new Vector3(0, 0.5f, 10);
    [SerializeField]
    bool _isGameOver = false, isPaused = false, gameStarted = false;
    [SerializeField]
    int _life = 3;
    [SerializeField]
    int _score = 0;

    public Text scoreUI;
    public Text lifeUI;

    public GameObject pauseMenuUI, gameOverUI, mainScreenUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        CheckGameOver();
        PauseMenu();
        GameOver();
        StartNewGame();
    }

    void StartNewGame()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1f;
            StartCoroutine(SpawnEnemyOverTime());
            mainScreenUI.SetActive(false);
        }
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P) && !_isGameOver)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            isPaused = true;
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.B) && isPaused)
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            isPaused = false;
        }
    }

    public void GameOver()
    {
        if (_isGameOver)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

            if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Return) && _isGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void CheckGameOver()
    {
        if(_life==0)
        {
            _isGameOver = true;
        }
    }

    public void UpdateUI()
    {
        scoreUI.text = "SCORE: "+ _score.ToString();
        lifeUI.text = "VIDAS: " + _life.ToString();
    }
    public void LoseLife()
    {
        _life--;
    }

    public void AddScore()
    {
        _score += 100;
    }

    void SpawnEnemy()
    {
        Instantiate(_enemy, _enemyStartPoint, Quaternion.identity);
    }

    IEnumerator SpawnEnemyOverTime()
    {
        while (!_isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            yield return StartCoroutine(EnemyGroup());
        }
    }

    IEnumerator EnemyGroup()
    {
        var x = 0;
        var qtd = Random.Range(2, 7);
        Debug.Log(qtd);
        while (x < qtd)
        {
            yield return new WaitForSeconds(0.4f);
            x++;
            SpawnEnemy();
        }
    }
}
