using UnityEngine;

[CreateAssetMenu(fileName = "syokaki_occur", menuName = "ScriptableObject/syokaki_occur")]
public class syokaki_occur : ScriptableObject
{
    public int probability = 50;
    public void AddProbability()
    {
        probability += 3;
    }
    public void ResetProbability()
    {
        probability = -3;
    }
}
