using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class sandstorm : MonoBehaviour
{
    public Volume volume;
    public Volume volume_red;
    private ColorAdjustments coloradjustment;
    private FilmGrain filmgrain;   
    private ColorAdjustments coloradjustment_red;
    private FilmGrain filmgrain_red;
    private List<AudioSource> audiolist = new List<AudioSource>();

    public int chance = 50;
    public int red_chance = 990;
    private System.Random r = new System.Random();
    
    //以下、同時に起こらないスクリプト
    public sakebigoe sakebi;
    public GhostComing Ghost1;
    public anaunce_01 ana;
    void Start()
    {
        AudioSource[] audios = GetComponentsInChildren<AudioSource>(true);
        if(volume.profile.TryGet(out coloradjustment) && volume.profile.TryGet(out filmgrain) && volume_red.profile.TryGet(out coloradjustment_red) && volume_red.profile.TryGet(out filmgrain_red))
        {
            coloradjustment.saturation.value = 0;
            filmgrain.intensity.value = 0;
            foreach(AudioSource l in audios)
            {
                audiolist.Add(l);
            }

            StartCoroutine(screenSandStorm());
        }
    }

    IEnumerator screenSandStorm()
    {
        int rand;
        yield return new WaitForSeconds(70f);
        while (true)
        {
            rand = r.Next(0,1000);
            if(rand < chance)
            {   
                Ghost1.enabled = false;
                ana.enabled = false;
                sakebi.enabled = false;
                while (coloradjustment.saturation.value != -100 && filmgrain.intensity.value != 1.00f)
                {
                    audiolist[0].Play();
                    audiolist[1].Play();
                    coloradjustment.saturation.value -= 1;
                    filmgrain.intensity.value += 0.01f;
                    audiolist[0].volume += 0.01f;
                    audiolist[1].volume += 0.01f;
                    yield return new WaitForSeconds(0.05f);
                }
                Debug.Log("砂嵐後10秒待機");
                yield return new WaitForSeconds(10);
                while (coloradjustment.saturation.value != 0 && filmgrain.intensity.value != 0.00f)
                {
                    coloradjustment.saturation.value += 1;
                    filmgrain.intensity.value -= 0.01f;
                    audiolist[0].volume -= 0.01f;
                    audiolist[1].volume -= 0.01f;
                    yield return new WaitForSeconds(0.05f);
                }
                audiolist[0].Stop();
                audiolist[1].Stop();

                Ghost1.enabled = true;
                ana.enabled = true;
                sakebi.enabled = true;

                chance = -3;
            }
            else if(rand > red_chance)
            {
                Ghost1.enabled = false;
                ana.enabled = false;
                sakebi.enabled = false;
                Debug.Log("redに入りました");
                volume.weight = 0;
                volume_red.weight = 0;
                audiolist[2].Play();
                while(volume_red.weight != 1.00f && filmgrain_red.intensity.value != 1.00f)
                {
                    volume_red.weight += 0.01f;
                    filmgrain_red.intensity.value += 0.01f;   
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(10);
                audiolist[2].Stop();
                audiolist[3].Play();
                while(volume_red.weight != 0.00f && filmgrain_red.intensity.value != 0.00f)
                {
                    volume_red.weight -= 0.01f;
                    filmgrain_red.intensity.value -= 0.01f;   
                    yield return new WaitForSeconds(0.05f);
                }
                volume.weight = 1;
                volume_red.weight = 0;
                filmgrain_red.intensity.value = 0;

                Ghost1.enabled = true;
                ana.enabled = true;
                sakebi.enabled = true;
                chance = -3;
            }
            else chance+=1;
        yield return new WaitForSeconds(10);   
        }
    }
}

