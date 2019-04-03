using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text WinText;

    private bool gameOver;
    private bool Restart;
    private int score;


    void Start()
    {
        gameOver = false;
        Restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        SceneManager.LoadScene("Space Shooter");
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            if (Input.GetKey("escape"))
                Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'Q' for Restart";
                Restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            WinText.text = "You win!";
            gameOver = true;
            Restart = true;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "GAME CREATED BY ROMAN STARNER";
        gameOver = true;
    }
}