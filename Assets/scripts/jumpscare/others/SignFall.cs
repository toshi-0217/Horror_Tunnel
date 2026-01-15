using UnityEngine;

public partial class SignFall : MonoBehaviour
{
    [SerializeField] private Rigidbody signRigidbody; // 倒れる標識のRigidbody
    [SerializeField] private float pushForce = 5f;    // 倒れる時の勢い

    private bool hasFallen = false;
    private AudioSource sound;

    private void OnTriggerEnter(Collider other)
    {
        sound = GetComponent<AudioSource>();
        // プレイヤーが触れた、かつ、まだ倒れていない場合
        if (other.CompareTag("Player") && !hasFallen)
        {
            Fall();
        }
    }

    void Fall()
    {
        hasFallen = true;

        // Kinematicをオフにして物理演算を有効にする
        signRigidbody.isKinematic = false;

        // 前方向に力を加える
        Vector3 forceDirection = signRigidbody.transform.right * (-pushForce);
        signRigidbody.AddForce(forceDirection, ForceMode.Impulse);
        new WaitForSeconds(0.5f);
        sound.Play();
    }
}