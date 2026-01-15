using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Doll_Crying : MonoBehaviour
{
    public Transform Doll;

    public float waitInterval = 3f;
    private List<AudioSource> cry = new List<AudioSource>();
    private bool flag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] crylist = GetComponentsInChildren<AudioSource>(true);
        foreach(AudioSource l in crylist)
        {
            cry.Add(l);
        }
        StartCoroutine(Crying());
    }

    IEnumerator Crying()
    {
        while(true)
        {
            if (flag)
            {
                Playing(cry[0]);
                flag = false;
            }
            else
            {
                Playing(cry[1]);
                flag = true;
            }
            yield return new WaitForSeconds(waitInterval);
        }
    }

    void Playing(AudioSource cry)
    {
        Debug.Log("Dollが泣いている...");
        cry.Play();   
    }
}
