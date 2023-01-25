using UnityEngine;

public class GemHolder : MonoBehaviour
{
    [SerializeField] private GameObject gem;
    [SerializeField] private InteractionHandler interactionHandler;
    [SerializeField] private PlatformCleaner platformCleaner;
    public InteractionHandler Handler => interactionHandler;

    public PlatformCleaner Cleaner => platformCleaner;


    public void ShowGem()
    {
        gem.SetActive(true);
    }
}