using UnityEngine;

public class syokaki_reset : MonoBehaviour
{
    public syokaki_occur config;
    void Start()
    {
        config.probability = 50;
    }
}
