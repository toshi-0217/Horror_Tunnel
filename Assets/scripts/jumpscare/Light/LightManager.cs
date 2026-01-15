using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{
    public static event System.Action OnLightsOff;

    public static event System.Action OnLightsOn;


    public float blinkDuration = 10f; // 停電/復旧の持続時間
    public float checkInterval = 10f;   // 乱数チェックの間隔
    public int outageChance = 10;      // 停電発生の確率

    private System.Random r = new System.Random();
    private static LightManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        StartCoroutine(CheckForOutage());
    }

    IEnumerator CheckForOutage()
    {
        yield return new WaitForSeconds(60f);
        while (true)
        {
            int flag = r.Next(0, 1000); 
            Debug.Log($"LightManager: flag:{flag}");

            // 確率判定 
            if (flag < outageChance)
            {
                Debug.Log("LightManager: Light Down!! (全照明に通知)");
                
                // イベントに登録されているすべてのメソッドが実行される
                if (OnLightsOff != null)
                {
                    OnLightsOff.Invoke();
                }
                
                yield return new WaitForSeconds(blinkDuration);

                // すべての照明に復旧を通知 ---
                if (OnLightsOn != null)
                {
                    OnLightsOn.Invoke();
                }
                
                yield return new WaitForSeconds(blinkDuration); // 復旧後の待機
                outageChance = -3;
            }
            else
            {
                outageChance+=1;
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}
