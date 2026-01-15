using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class escape_display : MonoBehaviour
{
    [SerializeField] private List<GameObject> Images = new List<GameObject>();
    public AudioSource clearsound;
    public AudioSource message;
    public AudioSource blood;
    void Start()
    {   
        
        foreach(var t in Images) t.SetActive(false);
        StartCoroutine(Result());   
    }

    IEnumerator Result()
    {
        Images[0].SetActive(true);
        clearsound.Play();
        yield return new WaitForSeconds(5f);
        Images[1].SetActive(true);
        message.Play();
        yield return new WaitForSeconds(5f);
        Images[2].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(1f);
        Images[3].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(0.5f);
        Images[4].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(0.3f);
        Images[5].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(0.15f);
        Images[6].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(0.15f);
        Images[7].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(0.1f);
        Images[8].SetActive(true);
        blood.Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Start");
    }
}
