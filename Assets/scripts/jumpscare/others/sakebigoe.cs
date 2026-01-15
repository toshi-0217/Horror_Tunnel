using System.Collections;
using UnityEngine;

public class sakebigoe : MonoBehaviour
{
    public System.Random r = new System.Random();
    public int chance = 5;
    private AudioSource sakebi;
    public float WaitInterval = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sakebi = GetComponent<AudioSource>();
        StartCoroutine(sakebiselection());
    }

    IEnumerator sakebiselection()
    {
        yield return new WaitForSeconds(20f);
        while (true)
        {
            if(r.Next(0,1000) < chance)
            {
                Playing();
                chance = 0;
            }
            else chance += 2;
            
            Debug.Log("10秒おきに叫ぶか決めます");
            yield return new WaitForSeconds(WaitInterval);
        }

    }

    // Update is called once per frame
    void Playing()
    {
        Debug.Log("叫びました");
        sakebi.Play();
    }
}
