using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    public int points = 0;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> enemySpawnLocations = new List<Transform>();
    [SerializeField] private float enemySpawnTimer = 3f;
    private float currentEnemySpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        pointsText.text = points.ToString();
        currentEnemySpawnTimer = enemySpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemySpawnTimer -= Time.deltaTime;

        pointsText.text = points.ToString();
        if (points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", points);
        }

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        if(currentEnemySpawnTimer <= 0)
        {
            GameObject go = Instantiate(enemyPrefab, enemySpawnLocations[Random.Range(0,2)].position, Quaternion.identity);
            currentEnemySpawnTimer = enemySpawnTimer;
        }
    }
}
