using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introduction : MonoBehaviour
{
    public Canvas introcanvas;
    public Image background;

    private List<Text> text = new List<Text>();
    public PlayerMove move;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    public Canvas mainCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text[] textlist = GetComponentsInChildren<Text>();
        foreach(Text l in textlist)
        {
            text.Add(l);
        }
        move.enabled = false;
        Debug.Log("コルーチンに入る");
        StartCoroutine(intro());
    }

    IEnumerator intro()
    {
        Debug.Log("コルーチンに入った");
        Color color = background.color;
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        yield return new WaitForSeconds(3f);
        text1.SetActive(true);
        yield return new WaitForSeconds(6f);
        text1.SetActive(false);
        yield return new WaitForSeconds(3f);
        text2.SetActive(true);
        yield return new WaitForSeconds(6f);
        text2.SetActive(false);
        yield return new WaitForSeconds(3f);
        text3.SetActive(true);
        yield return new WaitForSeconds(6f);
        text3.SetActive(false);
        yield return new WaitForSeconds(3f);
        for(float t = 0; t<4f; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1,0, t/4f);
            background.color = color;
            yield return null;   
        }
        color.a = 0;
        background.color = color;
        introcanvas.enabled = false;
        move.enabled = true;
        mainCanvas.enabled = true;
    }
}
