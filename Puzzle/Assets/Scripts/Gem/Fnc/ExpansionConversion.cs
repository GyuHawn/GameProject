using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionConversion : MonoBehaviour
{
    public GemCombination gem; // ���� ���� ���� Ȯ�ο�
    public GameObject[] state; // ���� ������Ʈ
    public bool plus; // ���� ����

    void Start()
    {
        plus = false;
    }

    void Update()
    {
        if (gem.currentGemNum == 1.3f) // Ȯ�� �����϶� v(����)Ű �Է½� ���� ��ȭ
        {
            if (Input.GetButtonDown("Expansion"))
            {
                state[0].gameObject.SetActive(plus);
                state[1].gameObject.SetActive(!plus);
                plus = !plus;
            }
        }
    }
}
