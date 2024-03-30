using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI EndText;
    [SerializeField] private GameObject EndScreen;
    public int points = 0;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> enemySpawnLocations = new List<Transform>();
    [SerializeField] private float enemySpawnTimer = 3f;
    public bool keepSpawning = true;
    private float currentEnemySpawnTimer;
    private float currentEnemySpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        EndScreen.SetActive(false);
        highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        pointsText.text = points.ToString();
        currentEnemySpawnTimer = enemySpawnTimer;
        currentEnemySpawnTime = enemySpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemySpawnTime -= Time.deltaTime;

        pointsText.text = points.ToString();
        if (points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", points);
            highscoreText.text = points.ToString();
        }
        if (keepSpawning)
            SpawnEnemies();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void LoadTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }
    public void EndScreenActivate()
    {
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        if (highscore == points)
        {
            EndText.text = "New HighScore! Congratulations!! \n \n Thanks for playing my game :D \n \n \n \n Do you want to play again?";
                
        }
        else
        {
            EndText.text = "Thanks for playing my game :D \n \n \n \n Do you want to play again?";
        }
        

        EndScreen.SetActive(true);
    }
    void SpawnEnemies()
    {
        if(currentEnemySpawnTime <= 0)
        {
            int selectedIndex = Random.Range(0, 2);
            GameObject go = Instantiate(enemyPrefab, enemySpawnLocations[selectedIndex].position, Quaternion.identity);
            if (currentEnemySpawnTimer > .2f)
            {
                currentEnemySpawnTimer -= .1f;
            }
            else
            {
                currentEnemySpawnTimer = .2f;
            }
            currentEnemySpawnTime = currentEnemySpawnTimer;
        }
    }
}
