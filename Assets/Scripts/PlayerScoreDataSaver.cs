using UnityEngine;

public class PlayerScoreDataSaver : MonoBehaviour
{
    public void SaveData(SavedData data, int value)
    {
        PlayerPrefs.SetInt(data.ToString(), value);
    }
}