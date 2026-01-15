using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;

public class Tunnel_Generating : MonoBehaviour
{
    //トンネルの最大値や、どのトンネルを配置するかを決めるための乱数
    public System.Random r = new System.Random();

    [Header("トンネル設定")]
    public GameObject Tunnel_Segment01;
    public GameObject Tunnel_Segment03;
    public GameObject Tunnel_Segment04;
    public GameObject End_Tunnel;
    public int startTunnelCount = 5;
    public float tunnelLength = 100f;
    public int chance = 990;

    [Header("プレイヤ追従設定")]
    public Transform player;
    public float spawnDistance = 150f;

    private List<GameObject> tunnels = new List<GameObject>();
    private float currentEndZ = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int count;
    private int maxTunnel;


    void Start()
    {
        count = 0;
        maxTunnel = r.Next(20, 30);
        // maxTunnel = 6; //テスト用
        Debug.Log($"start max:{maxTunnel}");
        for (int i = 0; i < startTunnelCount; i++)
        {
            AddTunnel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z + spawnDistance > currentEndZ)
        {
            if(count <= maxTunnel)
            {
                AddTunnel();    
            }
            if (tunnels.Count > startTunnelCount)
            {
                Destroy(tunnels[0]);
                tunnels.RemoveAt(0);
            }
        }
    }
    
    void AddTunnel()
    {
        if(count == maxTunnel)
        {
            //出口
            GameObject newTunnel = Instantiate(End_Tunnel, new Vector3(0, 0, currentEndZ - 50f), Quaternion.identity);
            tunnels.Add(newTunnel);
            Debug.Log("end");
            count++;
        }
        else
        {
            int select = 0;
            select = r.Next(1,1000);
            if(select < chance)
            {
                GameObject newTunnel = Instantiate(Tunnel_Segment01, new Vector3(0, 0, currentEndZ), Quaternion.identity);    
                tunnels.Add(newTunnel);
                currentEndZ += tunnelLength;
                count++;
                Debug.Log("01のトンネルが生成された");
                chance-=5;
            }
            else
            {
                if(r.Next(1,1000) < 501)
                {
                    GameObject newTunnel = Instantiate(Tunnel_Segment03, new Vector3(0, 0, currentEndZ), Quaternion.identity);    
                    tunnels.Add(newTunnel);
                    currentEndZ += tunnelLength;
                    count++;
                    Debug.Log("03トンネルが生成された");
                    chance = 1000;   
                }
                else
                {
                    GameObject newTunnel = Instantiate(Tunnel_Segment04, new Vector3(0, 0, currentEndZ), Quaternion.identity);    
                    tunnels.Add(newTunnel);
                    currentEndZ += tunnelLength;
                    count++;
                    Debug.Log("04トンネルが生成された");
                    chance = 1000;   
                }
            }
            Debug.Log($"generate count:{count}");    
        }
        
    }
}
