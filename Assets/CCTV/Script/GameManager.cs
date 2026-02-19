using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;

    [SerializeField] private List<StageRoom> cctvs;

    int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIndex = 0;
        ChangeCCTV();

        btnLeft.onClick.AddListener(()=>Increase(1));
        btnRight.onClick.AddListener(() => Increase(-1));
    }
    void Increase(int _modifier)
    {
        int change = currentIndex + _modifier;
        if (change < 0) {
            change = cctvs.Count - 1;
        }
        else if ( change >= cctvs.Count)
        {
            change = 0;
        }
        currentIndex = change;

        ChangeCCTV();
    }

    void ChangeCCTV()
    {
        for (int i = 0; i < cctvs.Count; i++)
        {
            if (currentIndex.Equals(i))
            {
                cctvs[i].Priority = 1;
            }
            else cctvs[i].Priority = 0;
        }
    }
}
