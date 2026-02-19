using System.Runtime.CompilerServices;
using UnityEngine;

public class StrangeTrigger : MonoBehaviour
{
    [SerializeField] private Constant.Type type;

    [SerializeField] private GameObject strangeObject; 
    [SerializeField] private GameObject realObject; 

    [SerializeField] private bool NotMoveOnce;

    public bool HasBeenUsed, HasBeenSolved;

    private void Start()
    {
        HasBeenUsed = false;
    }

    public void Activate()
    {
        switch (type)
        {
            case Constant.Type.MOVEMENT:
                if (NotMoveOnce == true) return;

                strangeObject.SetActive(true);
                realObject.SetActive(false);
                break;
            case Constant.Type.SWAP:
                strangeObject.SetActive(true);
                realObject.SetActive(false);
                break;
            case Constant.Type.EXTRA:
                strangeObject.SetActive(true);
                break;
            case Constant.Type.MISSING:
                realObject.SetActive(false);
                break;
        }
        HasBeenUsed = true;
    }
}
