using UnityEngine;

public class sararyman_voice : MonoBehaviour
{
    public GameObject man;

    private bool sensor = false;

    public AudioSource audios;
    public Animator manani;
    private void OnTriggerEnter(Collider other)
    {
        // audios = man.GetComponentInChildren<AudioSource>(true);
        if (other.CompareTag("Player") && !sensor)
            {
                sakebi();
            }
    }

    void sakebi()
    {
        Debug.Log("叫んだ");
        sensor = true;
        audios.Play();
        manani.speed = 0f;
    }
}
