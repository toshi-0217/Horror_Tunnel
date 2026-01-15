using UnityEngine;
using System.Collections.Generic;

public class LightController : MonoBehaviour
{
    // Lightオブジェクトのリスト
    private List<Light> lights = new List<Light>();
    private List<AudioSource> down = new List<AudioSource>();
    
    // public float blinkInterval = 10f; // LightManagerで制御するため不要
    // public float checkInterval = 1f;   // LightManagerで制御するため不要

    void Start()
    {
        Light[] childLights = GetComponentsInChildren<Light>(true);
        AudioSource[] lightdown = GetComponentsInChildren<AudioSource>(true);

        // 取得したコンポーネントをリストに格納
        foreach (Light l in childLights)
        {
            lights.Add(l);
        }
        foreach (AudioSource m in lightdown)
        {
            down.Add(m);
        }

        if (lights.Count == 0)
        {
            Debug.LogWarning(gameObject.name + "の子からLightコンポーネントが見つかりませんでした。");
        }
    }

    void OnEnable()
    {
        LightManager.OnLightsOff += TurnOffLights; // 停電イベントにメソッドを登録
        LightManager.OnLightsOn += TurnOnLights;   // 復旧イベントにメソッドを登録
    }

    void OnDisable()
    {
        // オブジェクトが非アクティブ化されたら必ず登録を解除
        LightManager.OnLightsOff -= TurnOffLights;
        LightManager.OnLightsOn -= TurnOnLights;
    }

    
    // LightManagerから呼ばれる停電処理
    private void TurnOffLights()
    {
        SetLightsEnabled(false);
    }
    
    // LightManagerから呼ばれる復旧処理
    private void TurnOnLights()
    {
        SetLightsEnabled(true);
    }

    private void SetLightsEnabled(bool state)
    {
        // Lightの有効/無効を切り替え
        foreach (Light l in lights)
        {
            if (l != null)
            {
                l.enabled = state;
            }
        }
        
        foreach (AudioSource audioSource in down)
        {
            if (audioSource != null && !state)
            {
                audioSource.Play();
            }
        }
    }
}
