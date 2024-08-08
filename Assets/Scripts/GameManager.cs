using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using TreeEditor;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string json;
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    private Player player;
    private Spawner spawner;

    private float score;
    public float Score => score;
    private float hiscore;

    public PlayfabManager playfabManager;
    public PostRequest postRequest; // Reference to the PostRequest component

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            Debug.LogWarning("Another instance of GameManager found and destroyed.");
        }
        else
        {
            Instance = this;
            Debug.Log("GameManager instance created.");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            Debug.Log("GameManager instance destroyed.");
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
        hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        Debug.Log("Game started. Current hiscore: " + hiscore);
    }

    public void NewGame()
    {
        Debug.Log("Starting new game.");
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
            Debug.Log("Destroyed obstacle: " + obstacle.name);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
        playfabManager.SendLeaderboard(Mathf.FloorToInt(hiscore));
        Debug.Log("Leaderboard updated with hiscore: " + hiscore);
    }
    [System.Serializable]
    public class Person
    {
        public string name { get; set; }
        public string regno { get; set; }
        public int score { get; set; }
    }


    public void GameOver()
    {
        Debug.Log("Game over.");
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
        playfabManager.SendLeaderboard(Mathf.FloorToInt(hiscore));
        Debug.Log("Final score: " + Mathf.FloorToInt(score));

        // Send score to the server via PostRequest
        if (postRequest != null)
        {
            string playerName = PlayerPrefs.GetString("PlayerName");
            string registrationNo = PlayerPrefs.GetString("RegistrationNo");
            Debug.Log($"Sending score data to server. Name: {playerName}, RegNo: {registrationNo}, Score: {Mathf.FloorToInt(score)}");
            postRequest.SendScoreData(playerName, registrationNo, Mathf.FloorToInt(score));
        }
        else
        {
            Debug.LogError("PostRequest reference is not assigned.");
        }
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        UpdateHiscore();
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            Debug.Log("New hiscore achieved: " + hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
