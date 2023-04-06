using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parachuteSpawner : MonoBehaviour
{

    public float leftPosition;//left position of screen for random generation
    public float rightPosition;//right position of screen for random generation
    public GameObject parachutePrefab;//prefab of parachute to instantiate

    public float spawnRate = 1f;//timing betwwen spawning of new parachute
    //public int amountPerSpawn = 1;
    string correctSentence= "";//senetence to show on parachute
    //[Serializable]
    //public class Sentences//class which stores sentences as per level
    //{
    //    public List<string> name = new List<string>();
        
    //}
    //public Sentences[] sentence;//levels of sentences
    public addSentences sentence;//scriptable objects of levels

    GameManager gamemanager;


    private void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnParachute(spawnRate));//start spawning parachutes
    }

    public void startNewLevel()//used when level is increased
    {
        StartCoroutine(spawnParachute(spawnRate));
    }

    IEnumerator spawnParachute(float time)
    {
        while(gamemanager.numberOfSentences<10)//it creates parachutes untill all sentences created
        {
            Debug.Log("create" + gamemanager.numberOfSentences);
            float xPosition = UnityEngine.Random.Range(leftPosition, rightPosition);
            GameObject parachute = Instantiate(parachutePrefab, new Vector3(xPosition, 7f, 0), Quaternion.identity);//create parachute at random position
            parachute.GetComponent<parachute>().fallSpeed = gamemanager.parachuteSpeed;//set speed of falling parachute                                                                                           //parachute.GetComponent<Rigidbody2D>().velocity = new Vector3(0, Random.Range(0, -0));
            if (UnityEngine.Random.Range(0, 2) == 1)//randomly generates wrong or right sentences
            {
                correctSentence = sentence.sentence[gamemanager.userLevel].name[gamemanager.numberOfSentences];
                parachute.name = "incorrect";
                //Debug.Log(SetRandomWord());
                parachute.GetComponent<parachute>().sentence.text = SetRandomWord();//it shuffle sentence and generates random sentence
               
            }
            else
            {
                correctSentence = sentence.sentence[gamemanager.userLevel].name[gamemanager.numberOfSentences];
                parachute.GetComponent<parachute>().sentence.text = correctSentence;
                parachute.name = "correct";
            }
            gamemanager.numberOfSentences++;
            yield return new WaitForSeconds(time);
        }
        yield return new WaitForSeconds(time);
        {
            Debug.Log("CAME"+gamemanager.numberOfSentences);
            StopAllCoroutines();
            //gamemanager.gameComplete();//calls level complete
        }
    }
   

    string SetRandomWord()
    {
        string[] words = correctSentence.Split(' ');//get array of sentence
        string rndSentence="";
        string tempGo="";
        for (int i = 0; i < words.Length; i++)//shuffle array
        {
            int rnd = UnityEngine.Random.Range(0, words.Length);
            tempGo = words[rnd];
            words[rnd] = words[i];
            words[i] = tempGo;
        }

        foreach (string value in words)//create string from array
        {
            //Debug.Log(value);
            rndSentence += value + " ";
        }

        if(rndSentence==correctSentence)//if it does not shuffle set random string
        {
            return "are you Where going?";
        }
        else
        {
            return rndSentence;//return random sentence
        }
       
        
    }
}
