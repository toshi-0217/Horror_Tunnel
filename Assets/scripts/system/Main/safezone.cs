using UnityEngine;

public class safezone : MonoBehaviour
{
    public GameObject safe;
    public Transform Player;
    private Transform safetrans;
    private Collider coll;
    void Start()
    {
        safetrans = safe.GetComponent<Transform>();
        coll = safe.GetComponent<Collider>();
        coll.enabled = false;
    }
    void Update()
    {
        if(Player.transform.position.z-2 > safetrans.position.z)
        {
            coll.enabled = true;
        }
    }
}
