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

    public void Activate() => Activate(false);
    public void Deactivate() => Activate(true);
    private void Activate(bool _reverse)
    {
        switch (type)
        {
            case Constant.Type.MOVEMENT:
                if (NotMoveOnce == true) return;

                strangeObject.SetActive(!_reverse);
                realObject.SetActive(_reverse);
                break;
            case Constant.Type.SWAP:
                strangeObject.SetActive(!_reverse);
                realObject.SetActive(_reverse);
                break;
            case Constant.Type.EXTRA:
                strangeObject.SetActive(!_reverse);
                break;
            case Constant.Type.MISSING:
                realObject.SetActive(_reverse);
                break;
        }
        HasBeenUsed = !_reverse;
    }
}
