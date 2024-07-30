using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    private GemUI gemUI;

    public GameObject player;

    // �Ѿ� ȹ�� ����
    public bool onLarge;

    // �Ӽ� ȹ�� ����
    public bool onControl;
    public bool onFire;
    public bool onWater;
    public bool onElectricity;

    // ��� ȹ�� ����
    public bool onDestruction;
    public bool onPenetrate;
    public bool onDiffusion;
    public bool onUpgrade;
    public bool onQuick;

    private void Awake()
    {
        if (!gemUI)
            gemUI = FindObjectOfType<GemUI>();
    }

    public void CollectGem(string gemName)
    {
        switch (gemName)
        {
            case "Large":
                onLarge = true;
                break;
            case "Control":
                onControl = true;
                break;
            case "Fire":
                onFire = true;           
                break;
            case "Water":
                onWater = true;
                break;
            case "Electricity":
                onElectricity = true;
                break;
            case "Destruction":
                onDestruction = true;
                break;
            case "Penetrate":
                onPenetrate = true;
                break;
            case "Diffusion":
                onDiffusion = true;
                break;
            case "Upgrade":
                onUpgrade = true;
                break;
            case "Quick":
                onQuick = true;
                break;
        }
        gemUI.ActivateGemUI();
    }
}
