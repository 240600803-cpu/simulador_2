using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game settings")]
    public float levelTime = 120f; // seconds
    private float timeLeft;
    private bool running = false;

    [Header("Score")]
    public int score { get; private set; }

    [Header("UI refs")]
    public Text timerText;
    public Text scoreText;
    public GameObject endPanel;
    public Text endText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        timeLeft = levelTime;
        score = 0;
        running = true;
        if (endPanel != null) endPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {
        if (!running) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            EndLevel();
        }
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void EndLevel()
    {
        running = false;
        if (endPanel != null) endPanel.SetActive(true);
        if (endText != null) endText.text = "Time's up! Score: " + score;
        // Additional end logic here (save results, etc.)
    }
}
