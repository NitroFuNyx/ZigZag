using UnityEngine;

public class GemHolder : MonoBehaviour
{
    [SerializeField] private GameObject gem;
    [SerializeField] private InteractionHandler interactionHandler;
    [SerializeField] private PlatformCleaner _platformCleaner;
    public InteractionHandler Handler => interactionHandler;

    public PlatformCleaner Cleaner => _platformCleaner;



    public void ShowGem()
    {
        gem.SetActive(true);
    }
}