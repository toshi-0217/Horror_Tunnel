using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GhostComing : MonoBehaviour
{
    public GameObject Ghost;
    public Transform Player;
    public Volume GhostVolume;
    private ColorAdjustments Color;
    private FilmGrain Grain;
    private LensDistortion Lens;
    private Bloom Bloom;
    private List<AudioSource> noise = new List<AudioSource>();
    public int chance = 50;
    private System.Random r = new System.Random();
    public int waitinterval = 20;
    
    public PlayerMove playermove;
    public Camera playercamera;

    //以下、このスクリプトと同時に発動しないスクリプト
    public sandstorm sands;
    public sakebigoe sakebi;
    public anaunce_01 ana;

    public Rigidbody playerrigi;
    void Start()
    {
        if(GhostVolume.profile.TryGet(out Color) && GhostVolume.profile.TryGet(out Grain) && GhostVolume.profile.TryGet(out Bloom) && GhostVolume.profile.TryGet(out Lens))
        {
            Color.active = false;
            Grain.intensity.value = 0;
            Lens.intensity.value = 0;
            Bloom.intensity.value = 0;
            AudioSource[] noises = GetComponentsInChildren<AudioSource>(true);
            foreach(AudioSource l in noises)
            {
                noise.Add(l);
            }

            StartCoroutine(ghostcome());
        }
    }

    IEnumerator ghostcome()
    {
        yield return new WaitForSeconds(90f);
        while(true)
        {
            if(r.Next(0,1000) < chance)
            {
                sands.enabled = false;
                sakebi.enabled = false;
                ana.enabled = false;

                playermove.enabled = false;
                GhostVolume.weight = 1;
                Grain.intensity.value = 1;
                noise[0].Play();
                yield return new WaitForSeconds(0.1f);
                
                noise[0].Stop();
                noise[1].Play();
                GameObject newGhost = Instantiate(Ghost,Player.transform.position + new Vector3(0,0f,50f),Quaternion.Euler(0f,-90f,0f));
                Player.LookAt(newGhost.transform);
                Player.GetChild(0).LookAt(newGhost.transform);
                playercamera.transform.LookAt(newGhost.transform);
                playerrigi.linearVelocity = Vector3.zero;
                Grain.intensity.value = 0;
                Color.active = true;
                Lens.intensity.value = 1;
                Bloom.intensity.value = 5;
                Vector3 Lastposition  = Player.GetChild(0).transform.position + Player.GetChild(0).transform.forward * 5f;
                yield return new WaitForSeconds(7);
                newGhost.transform.position = Lastposition;
                newGhost.transform.LookAt(Player);
                Lens.intensity.value = 0;
                yield return new WaitForSeconds(1);
                // Vector3 rigiforce = playerrigi.transform.forward * 23f;
                // playerrigi.AddForce(rigiforce,ForceMode.Impulse);
                // yield return new WaitForSeconds(3);
                // playerrigi.AddForce(rigiforce,ForceMode.Impulse);
                // yield return new WaitForSeconds(5);


                noise[1].Stop();

                Color.active = false;
                Grain.intensity.value = 1;
                noise[0].Play();
                noise[2].Play();
                yield return new WaitForSeconds(0.1f);
                noise[0].Stop();

                Destroy(newGhost);
                Grain.intensity.value = 0;
                Lens.intensity.value = 0;
                Bloom.intensity.value = 0;
                GhostVolume.weight = 0;

                playermove.enabled = true;
                sands.enabled = true;
                sakebi.enabled = true;
                ana.enabled = true;

                chance = -3;
            }
            else chance += 2;
            yield return new WaitForSeconds(waitinterval);
        }
    }
}
