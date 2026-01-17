using UnityEngine;

public class syokakimove : MonoBehaviour
{
    public GameObject syokaki;
    public int chance = 40;
    public syokaki_occur config;
    private System.Random r = new System.Random();
    private Rigidbody rigi;
    private bool hasTriggered = false;
    public float pushforce = 10f;
    public AudioSource audios;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasTriggered)
        {
            if(r.Next(0,1000) < config.probability)
            {
                rigi = syokaki.GetComponent<Rigidbody>();
                config.ResetProbability();
                Debug.Log("reset");
                smash();       
            }
            else
            {
                config.AddProbability();
                Debug.Log("add");
            }
        }
    }

   void smash()
    {
        hasTriggered = true;
        rigi.isKinematic = false;

        Vector3 forceDirection = rigi.transform.right * pushforce;
        rigi.AddForce(forceDirection,ForceMode.Impulse);
        audios.Play();
    }
}