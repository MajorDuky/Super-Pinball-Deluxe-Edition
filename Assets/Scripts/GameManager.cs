using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text lostBalls;
    [SerializeField] private TMP_Text ballsLeftText;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text tempScoreText;
    [SerializeField] private TMP_Text inGameScore;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject welcomeUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private Animator launcherAnimator;
    [SerializeField] private int ballsLeft;
    [SerializeField] private GameObject launchAccessObject;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] private AudioClip newBall;
    [SerializeField] private AudioClip ballLost;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip womboCombo;
    public bool isGameActive;
    private int ballsLost = 0;
    private int tempScore;
    private int score;
    public float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void LoseBall()
    {
        multiplier = 1;
        multiplierText.SetText($"X{multiplier}");
        tempScore = 0;
        tempScoreText.SetText(tempScore.ToString());
        ballsLost++;
        lostBalls.SetText($"Balls Lost = {ballsLost}");
        audioSource.PlayOneShot(ballLost);
        if (ballsLeft == 0)
        {
            isGameActive = false;
            inGameUI.SetActive(false);
            gameOverUI.SetActive(true);
            finalScore.SetText($"Final Score = {score}");
            StartCoroutine(PlaySoundRoutine("game_over"));
        }
        else
        {
            GenerateNewBall();
        }
        
    }

    public void GenerateNewBall()
    {
        launchAccessObject.SetActive(false);
        Instantiate(ballPrefab, initialPos, ballPrefab.transform.rotation);
        StartCoroutine(PlaySoundRoutine("new_ball"));
        ballsLeft--;
        ballsLeftText.SetText(ballsLeft.ToString());
        if (ballsLeft == 0)
        {
            ballsLeftText.color = Color.red;
        }
        StartCoroutine(nameof(TriggerAnimationRoutine));
    }

    IEnumerator TriggerAnimationRoutine()
    {
        yield return new WaitForSeconds(2);
        launcherAnimator.SetTrigger("FireNewBall");
        yield return new WaitForSeconds(2);
        launchAccessObject.SetActive(true);
    }

    public void StartGame()
    {
        welcomeUI.SetActive(false);
        inGameUI.SetActive(true);
        isGameActive = true;
        GenerateNewBall();
        ballsLeftText.SetText(ballsLeft.ToString());
        tempScore = 0;
        tempScoreText.SetText(tempScore.ToString());
        score = 0;
        inGameScore.SetText($"Score = {score}");
        multiplier = 1;
        multiplierText.SetText($"X{multiplier}");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void increaseMultiplier()
    {
        multiplier += 0.25f;
        multiplierText.SetText($"X{multiplier}");
        if (multiplier % 2 == 0)
        {
            audioSource.PlayOneShot(womboCombo);
        }
    }
    public void increaseTempScore(int amount)
    {
        tempScore += (int)(amount * multiplier);
        tempScoreText.SetText(tempScore.ToString());
    }

    public void AddToScore()
    {
        score += tempScore;
        inGameScore.SetText($"Score = {score}");
        multiplier = 1;
        multiplierText.SetText($"X{multiplier}");
        tempScore = 0;
        tempScoreText.SetText(tempScore.ToString());
    }
    IEnumerator PlaySoundRoutine(string type)
    {
        yield return new WaitForSeconds(2);
        if (type == "game_over")
        {
            audioSource.PlayOneShot(gameOver);
        }
        else if (type == "new_ball")
        {
            audioSource.PlayOneShot(newBall);
        }
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
