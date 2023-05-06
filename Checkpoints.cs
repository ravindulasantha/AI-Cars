using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    [HideInInspector]
    public GameObject car;
    [HideInInspector]
    public GameObject AIcar1;
    [HideInInspector]
    public GameObject AIcar2;
    [HideInInspector]
    public GameObject AIcar3;

    public GameObject lapcountMenu;

    [HideInInspector]
    public int AIcarslap = 0, AICar2Lap = 0, AICar3Lap = 0;
    public int lapcount;


    public GameObject winMsg;
    public GameObject lossMsg;

    [HideInInspector]
    public int lap = 0;
    [HideInInspector]
    public int checkpoint = -1;
    int checkpointCount;
    int nextCheckpoint = 0;
    Dictionary<int, bool> visited = new Dictionary<int, bool>();
    public Text lapText;
    [HideInInspector]
    public bool missed = false;
    public GameObject PrevCheckpoint;

    public int currentCash;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Cash"))
        {
            currentCash = PlayerPrefs.GetInt("Cash");
        }
        car = GameObject.FindGameObjectWithTag("Player");
        AIcar1 = GameObject.FindGameObjectWithTag("AIcar1");
        AIcar2 = GameObject.FindGameObjectWithTag("AIcar2");
        AIcar3 = GameObject.FindGameObjectWithTag("AIcar3");
       
   
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");
        checkpointCount = checkpoints.Length;
        foreach (GameObject chpoint in checkpoints)
        {
            if (chpoint.name == "0")
            {
                PrevCheckpoint = chpoint;
                break;
            }
        }

        foreach (GameObject cp in checkpoints)
        {
            visited.Add(Int32.Parse(cp.name), false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            int checkpointCurrent = int.Parse(other.gameObject.name);
            if (checkpointCurrent == nextCheckpoint)
            {
                PrevCheckpoint = other.gameObject;
                visited[checkpointCurrent] = true;
                checkpoint = checkpointCurrent;

                //**************************************************
                if (checkpoint == 0 && gameObject.tag == "Player")
                {
                    lap++;

                    lapText.text = "Laps:" + lap + "/" + lapcount;
                }
                //*****************************************************
                else if (checkpoint == 0 && gameObject.tag == "AIcar1")
                {
                    AIcarslap++;

                }
                else if (checkpoint == 0 && gameObject.tag == "AIcar2")
                {
                    AICar2Lap++;

                }
                else if (checkpoint == 0 && gameObject.tag == "AIcar3")
                {
                    AICar3Lap++;

                }
                //*****************************************************

                nextCheckpoint++;
                if (nextCheckpoint >= checkpointCount)
                {
                    var keys = new List<int>(visited.Keys);
                    foreach (int key in keys)
                    {
                        visited[key] = false;
                    }
                    nextCheckpoint = 0;
                }
            }
            else if (checkpointCurrent != nextCheckpoint && visited[checkpointCurrent] == false)
            {
                missed = true;
            }
        }

        //**************************************************************************************************
        if (other.gameObject.tag == "Finish")
        {
            if (lap == (lapcount + 0) && gameObject.tag == "Player")
            {
                currentCash += 100;
                PlayerPrefs.SetInt("Cash", currentCash);

                winMsg.SetActive(true);
                lapText.text = "You Won";
                stopcars();
                Time.timeScale = 0;
                AudioListener.volume = 0;
                Pass();

            }
            else if (AIcarslap == (lapcount + 0) && gameObject.tag == "AIcar1")
            {
                currentCash -= 50;
                PlayerPrefs.SetInt("Cash", currentCash);
                lapText.text = "You Loss";
                AudioListener.volume = 0;
                lossMsg.SetActive(true);
                stopcars();
                Time.timeScale = 0;

            }
            else if (AICar2Lap == (lapcount + 0) && gameObject.tag == "AIcar2")
            {
                currentCash -= 50;
                PlayerPrefs.SetInt("Cash", currentCash);
                lapText.text = "You Loss";
                lossMsg.SetActive(true);
                stopcars();
                Time.timeScale = 0;

            }
            else if (AICar3Lap == (lapcount + 0) && gameObject.tag == "AIcar3")
            {
                currentCash -= 50;
                PlayerPrefs.SetInt("Cash", currentCash);
                lapText.text = "You Loss";
                lossMsg.SetActive(true);
                stopcars();
                Time.timeScale = 0;
            }



        }

    }

    public void Pass()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel >= PlayerPrefs.GetInt("levelsUnlocked") && currentlevel<=9)
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentlevel + 1);

        }
        Debug.Log("Level Unlocked");
    }

    public void stopcars()
    {
       // car.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMin = 0;
        //car.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMax = 0;

        /*AIcar1.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMin = 0;
        AIcar1.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMax = 0;

        AIcar2.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMin = 0;
        AIcar2.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMax = 0;

        AIcar3.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMin = 0;
        AIcar3.GetComponent<UnityStandardAssets.Vehicles.Car.CarAudio>().lowPitchMax = 0;
        */
        /*
                car.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
                car.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_ReverseTorque = 0;

                AIcar1.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
                AIcar1.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_ReverseTorque = 0;

                AIcar2.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
                AIcar2.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_ReverseTorque = 0;

                AIcar3.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_Topspeed = 0;
                AIcar3.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().m_ReverseTorque = 0;
        */


    }
    public void lap1()
    {
        lapscount(1);
        Time.timeScale = 1;
        lapcountMenu.SetActive(false);
        AudioListener.volume = 1;
    }
    public void lap2()
    {
        lapscount(2);
        Time.timeScale = 1;
        lapcountMenu.SetActive(false);
        AudioListener.volume = 1;
    }
    public void lap3()
    {
        lapscount(3);
        Time.timeScale = 1;
        lapcountMenu.SetActive(false);
        AudioListener.volume = 1;
    }
    public void lap4()
    {
        lapscount(4);
        Time.timeScale = 1;
        lapcountMenu.SetActive(false);
        AudioListener.volume = 1;
    }

    public int lapscount(int lap)
    {
        lapcount = lap;
        return lapcount;
       
    }
}
