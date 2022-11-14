using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Map map;
    public static GameManager GM;
    [SerializeField] private GameObject Ui;
    [SerializeField] private Text endTitle;
    [SerializeField] private Text timerText;
    private bool menuing;
    private List<CratePlate> plates;
    private bool win;
    private bool gameover;
    private float sec;
    private int min;
    
    void Awake()
    {
        Time.timeScale = 1;
        GM = this;
        endTitle.text = "";
        map = new Map(transform, 20);
        if (!map.GetSuccess())
        {
            SceneManager.LoadScene("Game");
        }
        plates = map.GetPlates();
        map = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menuing && !win)
            {
                Ui.SetActive(false);
                Time.timeScale = 1;
                menuing = false;
            }
            else if (!win)
            {
                menuing = true;
                Time.timeScale = 0;
                Ui.SetActive(true);
            }
        }

        if (gameover)
        {
            Time.timeScale = 0;
            endTitle.text = "You Lose !";
            Ui.SetActive(true);
        }

        sec += Time.deltaTime;
        if (sec >= 59)
        {
            if (min >= 59)
            {
                gameover = true;
            }
            else
            {
                min += 1;
                sec = 0; 
            }
        }
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    public void CheckWin()
    {
        foreach (var plate in plates)
        {
            if (!plate.GetUsed())
            {
                return;
            }
        }
        Time.timeScale = 0;
        win = true;
        endTitle.text = "You Win !";
        Ui.SetActive(true);
    }
}
