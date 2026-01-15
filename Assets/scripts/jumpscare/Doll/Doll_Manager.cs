using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Doll_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //日本人形のオブジェクト
    public GameObject Doll;
    //プレイヤのオブジェクト(プレイヤ位置把握用)
    public GameObject Player;
    //乱数
    public System.Random r = new System.Random();
    
    public int chance = 5;
    public float waitinterval = 20f;

    void Start()
    {
        if(Doll != null && Player != null)
        {
            StartCoroutine(dollscare());
        }
    }

    IEnumerator dollscare()
    {
        yield return new WaitForSeconds(20f);
        Transform playertransform = Player.transform;
        //GameObject doll = Instantiate(Doll, new Vector3(parentCenter.x,parentCenter.y+0.7f,parentCenter.z),Quaternion.Euler(0f,30f,180f));
        while (true)
        {
            if(r.Next(0,1000) < chance)
            {
                GameObject doll = Instantiate(Doll, playertransform.position + new Vector3(0,0,100f), Quaternion.Euler(0f,30f,180f));
                chance = -6;
            }   
            else chance+=2;
            yield return new WaitForSeconds(waitinterval);
        }
    }

}
