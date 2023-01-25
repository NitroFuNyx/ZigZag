using System;
using UnityEngine;
using Zenject;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private VFXHolder vfxHolder;
    [SerializeField] private PickedUpGemAnnouncer pickedUpGemAnnouncer;
    
    public event Action OnBeingCaptured;

    private void OnBeingCapturedCommand()
    {
        OnBeingCaptured?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBeingCapturedCommand();
        pickedUpGemAnnouncer.gameObject.SetActive(true);
        vfxHolder.PlayVFX();
        Destroy(gameObject);
    }
}