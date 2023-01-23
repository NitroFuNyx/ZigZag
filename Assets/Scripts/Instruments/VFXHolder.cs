using UnityEngine;

public class VFXHolder : MonoBehaviour
{
    [SerializeField] private ParticleSystem crystalVFX;

    public void PlayVFX()
    {
        crystalVFX.Stop();
        crystalVFX.Play();
    }
}
