using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CatSpawner : MonoBehaviour
{
    [Header("Объекты")]
    public GameObject rareCatPrefab;
    public GameObject catPrefab;
    public GameObject playAgainButton;
    public BoxCollider2D GridArea;

    [Header("Настройка счёта")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    private int score = 0;

    [Header("Настройки спавна")]
    [SerializeField] private float rareCatChance = 0.1f;
    [SerializeField] private float minSpawnInterval = 0.3f;
    [SerializeField] private float maxSpawnInterval = 0.7f;
    private float nextSpawnTime;

    [Header("Настройки игры")]
    public float gameTime = 60f;
    public bool isGameActive = true;

    private void Start()
    {
        playAgainButton.SetActive(false);
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        UpdateScoreText();
        UpdateTimerText(); // Добавил вызов обновления таймера
    }

    private void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            UpdateScoreText();
            UpdateTimerText(); // Теперь таймер обновляется

            if (Time.time >= nextSpawnTime)
            {
                SpawnCat();
                nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
        else if (isGameActive)
        {
            isGameActive = false;
            playAgainButton.SetActive(true);
            enabled = false;
        }
    }

    private void SpawnCat()
    {
        Bounds bounds = GridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 spawnPosition = new Vector3(x, y, 0);

        // Определяем, какую кошку спавнить (редкую или обычную)
        GameObject catToSpawn = Random.value < rareCatChance ? rareCatPrefab : catPrefab;

        // Создаём кошку
        Instantiate(catToSpawn, spawnPosition, Quaternion.identity);
    }

    private void UpdateScoreText()
    {
        _scoreText.text = $"Score: {score}";
    }

    private void UpdateTimerText()
    {
        timerText.text = $"Time: {Mathf.CeilToInt(gameTime)}";
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
