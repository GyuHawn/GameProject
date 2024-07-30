using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class GemCombination : MonoBehaviour
{
    private GemManager gemManager;

    public int selectBulletNum; // ���õ� �Ѿ� ��
    public int selectAttributeNum; // ���õ� �Ӽ� ��
    public int selectFunctionNum; // ���õ� ��� ��
    public GameObject[] B_Gems; // �Ѿ�
    public GameObject[] B_A_Gems; // �Ѿ� + �Ӽ�
    public GameObject[] B_F_Gems; // �Ѿ� + ���
    public GameObject[] B_A_F_Gems; // �Ѿ� + �Ӽ� + ���
    public float currentGemNum; // ���� ���õ� ���� ��

    public Sprite[] crossHair; // ������ �迭

    private void Awake()
    {
        if (!gemManager)
            gemManager = FindObjectOfType<GemManager>();
    }

    private void Start()
    {
        currentGemNum = 1; // ���� ó���� �⺻ź ����
    }

    void Update()
    {
        // ������ ����
        SelectGem(selectBulletNum, selectAttributeNum, selectFunctionNum);
    }

    void ResetGem() // ��� ���� ��Ȱ��ȭ
    {
        DeactivateAllGems(B_Gems);
        DeactivateAllGems(B_A_Gems);
        DeactivateAllGems(B_F_Gems);
        DeactivateAllGems(B_A_F_Gems);
    }
    void DeactivateAllGems(GameObject[] gems) // ���� ��Ȱ��ȭ
    {
        foreach (var gem in gems)
        {
            gem.SetActive(false);
        }
    }

    void SelectGem(int bullet, int attribute, int function) // ���� ����
    {
        ResetGem(); // ��� ���� ��Ȱ��ȭ

        int gemIndex = GetGemIndex(bullet, attribute, function); // ���� �ε��� ���
        currentGemNum = CalculateCurrentGem(bullet, attribute, function); // ������ ���� �� ����

        if (bullet == 1) // �⺻ź
        {
            if (attribute == 0 && function == 0) // �Ӽ�x, ���x
                B_Gems[0].SetActive(true);
            else if (attribute == 0) // �Ӽ�x, ���o
                B_F_Gems[function - 1].SetActive(true);
            else if (function == 0) // ���x, �Ӽ�o
                B_A_Gems[attribute - 1].SetActive(true);
            else // �Ӽ�o, ���o
                B_A_F_Gems[gemIndex].SetActive(true);
        }
        else if (bullet == 2) // ����ź
        {
            if (attribute == 0 && function == 0) // �Ӽ�x, ���x
                B_Gems[1].SetActive(true);
            else if (attribute == 0) // �Ӽ�x, ���o
                B_F_Gems[function + 4].SetActive(true);
            else if (function == 0) // ���x, �Ӽ�o
                B_A_Gems[attribute + 3].SetActive(true);
            else // �Ӽ�o, ���o
                B_A_F_Gems[gemIndex + 20].SetActive(true);
        }
    }

    int GetGemIndex(int bullet, int attribute, int function) // ���� �ε��� ���
    {
        return (attribute - 1) * 5 + (function - 1);
    }

    float CalculateCurrentGem(int bullet, int attribute, int function) // ������ ���� �� ����
    {
        float baseValue = bullet;
        float attributeValue = attribute * 0.1f;
        float functionValue = function * 0.01f;
        return baseValue + attributeValue + functionValue;
    }
}
