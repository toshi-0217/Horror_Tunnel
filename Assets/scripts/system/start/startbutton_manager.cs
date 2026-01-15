using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startbutton_manager : MonoBehaviour
{
    public Image background;
    public AudioSource pushsound;
    public void OnClickStartButton()
    {
        StartCoroutine(feedout());
    }

    IEnumerator feedout()
    {
        pushsound.Play();
        Color color = background.color;
        for(float t = 0; t<3f; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0,1, t/3f);
            background.color = color;
            yield return null;   
            }
            color.a = 1f;
            background.color = color;
            SceneManager.LoadScene("maingame");
    }
}
