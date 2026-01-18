using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Vector3 levelMoveDirection;
    public float levelMoveSpeed = 100f;
    public float enemyMoveSpeed = 50f;
    public Transform playerTransform;
    public Transform spawnLocation;

    public TMP_Text points_text;
    private float scoreTimer;
    private int currentScore;
    private float scoreInterval = 0.5f;
    public bool isGameActive = true;

    public GameObject restartButton;

    public List<GameObject> roadPrefabs = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("More than one level manager in the scene");
        }

        
    }

    private void Start(){
        currentScore = 0;
        UpdateScoreUI();
        restartButton.GetComponent<Button>().onClick.AddListener(RestartLevel);
        restartButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)){
            RestartLevel();
        }

        if (playerTransform){
            levelMoveDirection = -playerTransform.forward;
            if (isGameActive){
                UpdateScore();
            }

        }

    }

    private void UpdateScore(){
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= scoreInterval)
        {
            currentScore++;
            scoreTimer -= scoreInterval; 
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        points_text.text = "Points: " + currentScore.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GameOver()
    {
        isGameActive = false;
        restartButton.SetActive(true);
    }

    public void SpawnNewRoad()
    {
        int randomIndex = Random.Range(0, (int)roadPrefabs.Count);
        Instantiate(roadPrefabs[randomIndex], spawnLocation.position, Quaternion.identity);
    }
}

