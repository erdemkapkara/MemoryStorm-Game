﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController: MonoBehaviour
{
    //-----------------------
    int mainGoal;
    int firstChooseValue;
    int instantAchievement;    
    //-----------------------
    GameObject choosenButton;
    GameObject selfButton;
    //-----------------------
    public Sprite defaultSprite;
    public AudioSource[] voices;
    public GameObject[] buttons;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI levelText;
    public GameObject[] endPannels;    
    public GameObject Grid;    
    public GameObject pool;
    public GameSettings[] Levels;
    //-----------------------
    public float totalTime = 180;
    public static int levelCount =1;
    public static int sceneCount = 1;
    float minute;
    float second;
    bool timer;
    bool addStation;
    int addCount;
    int totalImageCount;
    string isDifficult;

    //-----------------------  

    void Start()
    {
        mainGoal =pool.transform.childCount/2;
        firstChooseValue = 0;
        timer = true;
        addStation = true;
        addCount = 0;
        totalImageCount =pool.transform.childCount;
        StartCoroutine(Create());
        levelText.text = "Level" + " " + levelCount.ToString();
        totalTime = Levels[levelCount-1].time;
        isDifficult = Levels[levelCount-1].difficulty;
        totalImageCount = Levels[levelCount-1].imageCount;
        //if (levelCount < 4)
        //{
        //    sceneCount = 1;
        //}
        //if (levelCount >= 4 && levelCount<8)
        //{
        //    sceneCount = 2;
        //}
        //if (levelCount >8)
        //{
        //    sceneCount = 3;
        //}
    }

    private void Update()
    {
        if (timer && totalTime>1)
        {
            totalTime -= Time.deltaTime;

            minute = Mathf.FloorToInt(totalTime / 60);      
            second = Mathf.FloorToInt(totalTime % 60);

            counter.text = string.Format("{0:00}:{1:00}", minute, second);
            if (second == 6)
            {
                voices[2].Play();
            }

        }
        else
        {
            timer = false;
            GameOver();
        }

    }
    public void StopGame()
    {
        endPannels[2].SetActive(true);
        Time.timeScale = 0;

    }
    public void ResumeGame()
    {
        endPannels[2].SetActive(false);
        Time.timeScale = 1;

    }
    void GameOver()
    {
        endPannels[0].SetActive(true);
         
    }
    void Win()
    {
        endPannels[1].SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    
    public void NextLevel()
    {
        //foreach (var item in Levels)
        //{
        //    totalTime = item.time;
        //    isDifficult = item.difficulty;
        //    totalImageCount = item.imageCount;
        //}

        levelCount++;
        totalTime = Levels[levelCount-1].time;
        isDifficult = Levels[levelCount-1].difficulty;
        totalImageCount = Levels[levelCount-1].imageCount;

        if (isDifficult == "Easy")
        {
            SceneManager.LoadScene(1);
        }

        else if (isDifficult == "Medium")
        {
            SceneManager.LoadScene(2);
        }

        else if (isDifficult == "Hard")
        {
            SceneManager.LoadScene(3);
        }
    }

    public void GiveObject(GameObject myObject) 
    {
        selfButton = myObject;
        selfButton.GetComponent<Image>().sprite = selfButton.GetComponentInChildren<SpriteRenderer>().sprite;
        selfButton.GetComponent<Image>().raycastTarget = false;
        voices[0].Play();
    }

    void ButtonSit(bool situation)
    {
        foreach (var item in buttons)
        {
            if (item!=null)
            {
                item.GetComponent<Image>().raycastTarget = situation;

            }            
        }

    }
    
    public void ButtonClicked(int value)
    {

        Control(value);
        
    }   

    void Control(int value)
    {

        if (firstChooseValue == 0)
        {
            firstChooseValue = value;
            choosenButton = selfButton;
        }
        else
        {
            StartCoroutine(ControlMain(value));
        }


    }
    IEnumerator ControlMain(int value)
    {
        ButtonSit(false);
        yield return new WaitForSeconds(1);

        if (firstChooseValue == value)
        {
            instantAchievement++;
            choosenButton.GetComponent<Image>().enabled = false;
            selfButton.GetComponent<Image>().enabled = false;
            firstChooseValue = 0;
            choosenButton = null;
            ButtonSit(true);

            if (mainGoal == instantAchievement)
            {
                Win();
            }
        }
        else
        {
            voices[1].Play();
            choosenButton.GetComponent<Image>().sprite = defaultSprite;
            selfButton.GetComponent<Image>().sprite = defaultSprite;            
            firstChooseValue = 0;
            choosenButton = null;
            ButtonSit(true);
        }

    }
    IEnumerator Create()
    {
        int i = 0;
        yield return new WaitForSeconds(.1f);
        while (addStation)
        {
            int randomNumber = Random.Range(0, pool.transform.childCount - 1);
            if (pool.transform.GetChild(randomNumber).gameObject != null)
            {
                pool.transform.GetChild(randomNumber).transform.SetParent(Grid.transform);
                addCount++;

                if (addCount == totalImageCount)
                {
                    addStation = false;
                }
                Grid.transform.GetChild(i++).localScale = new Vector3(1, 1, 1);
            }
           
        }
    }
}
