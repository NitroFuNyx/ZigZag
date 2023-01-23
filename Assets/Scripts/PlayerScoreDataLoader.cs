using UnityEngine;

public class PlayerScoreDataLoader : MonoBehaviour
{
    public int LoadData(SavedData data)
    {
        return PlayerPrefs.GetInt(data.ToString());
    }
}