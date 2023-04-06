using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numberOfSentences;//total number of sentence in one level
    public int score;//score of user
    public GameObject lblScore;//object to instantiate score effect
    public Text labelScore;// UI text to display score 

    public GameObject gameCompleteUI;//canvas object to show level complete

    public GameObject gameOverUI;//canvas object to show level fail
    public GameObject gameOverOwnUI;//canvas object to show level fail

    public int userLevel;//stores user level
      public float  parachuteSpeed=1f;//stores spped of parachute to fall down increase it do increase falling speed

    public int lives= 3;//lives of player
    public Text lblLives;

    public GameObject confettiParticle;//particle to instantiate when correct tap
    public GameObject confettiParticleLevelComplete;//particle to instantiate when game completes
    public GameObject gameoverParticle;

    public parachuteSpawner spawn;//parachute spawner class

    public int countDestroyedParachutes;


    void Awake()
     {


        // Use this line if you need the object to persist across scenes

        //if (!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") == 1)
        //{
        //    // Set and save all your PlayerPrefs here.
        //    // Now set the value of isFirstTime to be false in the PlayerPrefs.
        //    PlayerPrefs.SetInt("isFirstTime", 1);
        //    PlayerPrefs.SetInt("userLevel", 0);
        //    PlayerPrefs.SetInt("userScore", 0);
        //    PlayerPrefs.SetFloat("parachuteSpeed", 1f);
        //    PlayerPrefs.Save();
        //}
    }

void Start()
    {
        //set default values


        userLevel = PlayerPrefs.GetInt("userLevel",0);//get from playerprefs
        parachuteSpeed = PlayerPrefs.GetFloat("parachuteSpeed",1f);
        score = PlayerPrefs.GetInt("userScore");
        labelScore.text = score.ToString();
        lblLives.text = lives.ToString();
    }
    public void decreaseLives()//called when user taps on correct sentence
    {
        lives--;
        lblLives.text = lives.ToString();
        
    }
    public void gameComplete()//call when game is completed
    {
        parachute[] para = FindObjectsOfType<parachute>();

        for (int i = 0; i < para.Length; i++)
        {
            Destroy(para[i].gameObject);
        }
        Instantiate(confettiParticleLevelComplete, new Vector3(0,3f,0), Quaternion.identity);
        gameCompleteUI.SetActive(true);
        
    }
    public void gameOver(bool byOwn)//call when game is failed
    {
        parachute[] para = FindObjectsOfType<parachute>();

        for (int i = 0; i < para.Length; i++)
        {
            Destroy(para[i].gameObject);
        }
        spawn.StopAllCoroutines();
        Instantiate(gameoverParticle, new Vector3(0, 1f, 0), Quaternion.identity);

        if (!byOwn)
            gameOverUI.SetActive(true);
        else
            gameOverOwnUI.SetActive(true);
        //Time.timeScale = 0;

    }
    public void startNewLevel()//called when continue button pressed from gamecompleteUI
    {
        countDestroyedParachutes = 0;
        lives = 3;
        lblLives.text = lives.ToString();
        gameCompleteUI.SetActive(false);
        //reset number of sentence and increase level and speed
        numberOfSentences = 0;
        userLevel++;
        parachuteSpeed++;
        if (userLevel > 3)
        {
            userLevel = 0;
            parachuteSpeed = 1f;
        }

        PlayerPrefs.SetInt("userlevel", userLevel);
        PlayerPrefs.SetFloat("parachuteSpeed", parachuteSpeed);
        spawn.startNewLevel();
    }
    public void replayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void increaseScore(Vector3 position)//when player taps on wrong sentence
    {
       // Debug.Log(position);
        Instantiate(confettiParticle, position, Quaternion.identity);//create particle for correct answer

        score += 10;
        PlayerPrefs.SetInt("userScore",score);
        GameObject lbl_Score = Instantiate(lblScore, position, Quaternion.identity);
        StartCoroutine(SmoothLerp(0.5f,lbl_Score));//moving effect of score
    }

    private IEnumerator SmoothLerp(float time,GameObject lbl)
    {
        Vector3 startingPos = lbl.transform.position;
        Vector3 finalPos = new Vector3(7.13f,5.52f,0f);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            
            lbl.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(lbl);
        labelScore.text = score.ToString();
    }

   

}
