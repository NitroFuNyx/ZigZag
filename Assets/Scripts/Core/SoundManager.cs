using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;

    public void PlaySoundCoin()
    {
        audioSource.clip = coinSound;
        audioSource.Play();
    }
}
