using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class goal : MonoBehaviour
{
    private bool hasFallen = false;
    public Image background;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFallen)
        {
            StartCoroutine(goaleffect());
        }
    }

    IEnumerator goaleffect()
    {
        Color color = background.color;
        hasFallen = true;
        for(float t = 0; t<4f; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0,1, t/4f);
            background.color = color;
            yield return null;   
        }
        color.a = 1;
        background.color = color;
        SceneManager.LoadScene("clear");

    }
}
