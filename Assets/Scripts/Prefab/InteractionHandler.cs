using System;
using UnityEngine;
using Zenject;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private VFXHolder _vfxHolder;
    [SerializeField] private PickedUpGemAnnouncer _pickedUpGemAnnouncer;
    
    public event Action OnBeingCaptured;

    private void OnBeingCapturedCommand()
    {
        OnBeingCaptured?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBeingCapturedCommand();
        _pickedUpGemAnnouncer.gameObject.SetActive(true);
        _vfxHolder.PlayVFX();
        Destroy(gameObject);
    }
}