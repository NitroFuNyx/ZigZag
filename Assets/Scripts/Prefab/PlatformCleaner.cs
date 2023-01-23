using System;
using System.Collections;
using UnityEngine;

public class PlatformCleaner : MonoBehaviour
{
    [SerializeField] private Rigidbody platformRigidbody;

    public event Action OnDestroy;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(DestroyPlatform());
    }

    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(1.5f);
        platformRigidbody.isKinematic = false;
        yield return new WaitForSeconds(1.5f);
        OnDestroyCommand();

        Destroy(gameObject);
    }

    protected virtual void OnDestroyCommand()
    {
        OnDestroy?.Invoke();
    }
}