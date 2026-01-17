using UnityEngine;

[CreateAssetMenu(fileName = "OccurConfig", menuName = "ScriptableObject/OccurConfig")]
public class occurconfig : ScriptableObject
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
