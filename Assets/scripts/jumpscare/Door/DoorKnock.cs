using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class DoorKnock : MonoBehaviour
{
    public Transform Player;
    public Transform Door;
    public occurconfig config;

    public System.Random r = new System.Random();
    private int knock_rnd;
    public float waitInterval = 3f;

    //public int knockchance = 10;
    private List<AudioSource> knocks = new List<AudioSource>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] knocklist = GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource l in knocklist)
        {
            knocks.Add(l);
        }
        if(knocks.Count > 0 && r.Next(0,1000) < config.probability)
        {
            config.ResetProbability();
            //Debug.Log($"Reset:{Door.transform.position.z}");
            StartCoroutine(DoorKnocks());
        }
        else
        {
            config.AddProbability(); 
            //Debug.Log("Add");
        }
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    IEnumerator DoorKnocks()
    {
        int flag = 0;
        while (true)
        {
            if (Math.Abs(Player.position.z - Door.position.z) < 5)
            {
                flag = 1;
            }
            if (flag == 1)
            {
                playknocks();
            }
            yield return new WaitForSeconds(waitInterval);
        }
    }
    
    private void playknocks()
    {
        knock_rnd = r.Next(0, 4);
        Debug.Log($"knock発動:{knock_rnd}");
        knocks[knock_rnd].Play();
    }

}
