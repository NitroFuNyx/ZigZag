using UnityEngine;

public class GemHolder : MonoBehaviour
{
    [SerializeField] private GameObject gem;
    [SerializeField] private InteractionHandler interactionHandler;


    public InteractionHandler Handler => interactionHandler;


    public void ShowGem()
    {
        gem.SetActive(true);
    }
}