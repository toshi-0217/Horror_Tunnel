using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class anaunce_01 : MonoBehaviour
{
    private List<AudioSource> audios = new List<AudioSource>();
    private System.Random r = new System.Random();
    public int chance = 5;
    public int WaitInterval = 20;

    //以下、このスクリプトと同時に発動しないスクリプト
    public sandstorm sands;
    public sakebigoe sakebi;
    public GhostComing Ghost1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] audio = GetComponentsInChildren<AudioSource>(true);
        if(audio != null)
        {
            foreach(AudioSource l in audio)
            {
                audios.Add(l);
            }
            StartCoroutine(anaunces());
        }
    }

    IEnumerator anaunces()
    {
        yield return new WaitForSeconds(60f);
        while (true)
        {
            
            if(r.Next(0,1000) < chance)
            {
                sands.enabled = false;
                sakebi.enabled = false;
                Ghost1.enabled = false;
                audios[0].Play();
                yield return new WaitForSeconds(7);
                audios[2].Play();
                yield return new WaitForSeconds(46);
                audios[1].Play();
                yield return new WaitForSeconds(7);
                sands.enabled = true;
                sakebi.enabled = true;
                Ghost1.enabled = true;
                chance = -3;
            }
            else chance+=2;
            yield return new WaitForSeconds(WaitInterval);
        }
    }
}
