using UnityEngine;
using System.Collections;

public class chaiceController : MonoBehaviour
{
    public Transform Player;
    public GameObject audioObject; 

    public float waitInterval = 30f;  // 足音発生をチェックする間隔
    public int chance = 100;          // 発生確率

    void Start()
    {
        if (Player == null)
        {
            Debug.LogError("Player Transformがアサインされていません！");
            return;
        }
        StartCoroutine(CheckForChaice());
    }

    IEnumerator CheckForChaice()
    {
        yield return new WaitForSeconds(50f);
        while(true)
        {
            int r = Random.Range(0, 1000);
            Debug.Log(r);
            if(r < chance)
            {
                GameObject newChaicer = Instantiate(audioObject, 
                                                    Player.position + new Vector3(0, 0, -20f), 
                                                    Quaternion.identity);
                
                AudioSource chaicesound = newChaicer.GetComponent<AudioSource>();

                if (chaicesound != null)
                {
                    StartCoroutine(MoveAndPlaySound(newChaicer, chaicesound));
                }
                else
                {
                    Debug.LogError("audioObjectプレハブにAudioSourceコンポーネントがありません！");
                    Destroy(newChaicer); // コンポーネントがなければすぐに破棄
                }
                Destroy(newChaicer);
                chance = 0;
            }
            else
            {
                chance += 1;
            }
            yield return new WaitForSeconds(waitInterval);
        }
    }

    // 足音を再生しながらオブジェクトを移動させるコルーチン
    IEnumerator MoveAndPlaySound(GameObject chaicer, AudioSource sound)
    {
        Debug.Log("chaice開始");
        

        Vector3 targetPosition = Player.position + new Vector3(0, 0, -2f);
        float moveSpeed = 5f; 

        while((chaicer.transform.position.z - Player.position.z) < -2f)
        {
            // プレイヤーに向かって移動させる
            Vector3 moveDirection = (targetPosition - chaicer.transform.position).normalized;
            chaicer.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            sound.Play();
            
            // 毎フレーム待機
            yield return null; 
        }

        Debug.Log("chaice終了");
        
        // オブジェクトを破棄
        Destroy(chaicer);

    }
}