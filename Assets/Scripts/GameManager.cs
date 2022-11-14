using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Map map;
    public static GameManager GM;
    [SerializeField] private GameObject Ui;
    [SerializeField] private Text endTitle;
    private bool menuing;
    private List<CratePlate> plates;
    private bool win;
    
    void Awake()
    {
        Time.timeScale = 1;
        GM = this;
        endTitle.text = "";
        map = new Map(transform, 20); 
        plates = map.GetPlates();
        map = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
