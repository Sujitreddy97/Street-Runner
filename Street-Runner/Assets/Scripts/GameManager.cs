using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private float score;
    private PlayerController playerControllerScript;


    //Game Start Animation
    public Transform startingPoint;
    public float lerpSpeed;

    //Score And GameOver

    public TextMeshProUGUI scorePanel;
    public GameObject gameOverPanel;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        score = 0;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }



    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.gameOver = false;

    }

    public void IncrementScore(int scoreAdd)
    {
        if (!playerControllerScript.gameOver)
        {

            score += scoreAdd;
            scorePanel.text = "SCORE: " + score.ToString();
            // Debug.Log("Score: " + score);
        }
    }

    public void GameOver()
    {


        gameOverPanel.SetActive(true);


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        playerControllerScript.ResetGame();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }
}
