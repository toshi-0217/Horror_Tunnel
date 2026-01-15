using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class JumpScareController : MonoBehaviour
{

    public GameObject ScareCanvas;
    public AudioSource  ScareSound_1;
    public AudioSource  ScareSound_2;
    public float displaytime = 2f;

    private bool hasTriggered = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") && !hasTriggered)
    {
        Debug.Log("プレイヤーがエリアに入りました！Canvasをオンにします。");
        
        if (ScareCanvas == null)
        {
            Debug.LogError("Canvasがインスペクターで設定されていません！");
            return;
        }

        StartCoroutine(displayimage());
    }
}

    IEnumerator displayimage()
    {
        hasTriggered = true;

        ScareCanvas.SetActive(true);

        if (ScareSound_1 != null && ScareSound_2 != null) {
            ScareSound_1.Play();
            ScareSound_2.Play();
        }
        yield return new WaitForSeconds(displaytime);
        ScareCanvas.SetActive(false);
        //日本人形を削除
        Destroy(transform.parent.GameObject());
    

    }
 
}
