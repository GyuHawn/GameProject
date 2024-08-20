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
        plus = true;
        state[0].gameObject.SetActive(true);
        state[1].gameObject.SetActive(false);
    }

    void Update()
    {
        if (gem.currentGemNum == 1.3f) // Ȯ�� �����϶� v(����)Ű �Է½� ���� ��ȭ
        {
            if (Input.GetButtonDown("Expansion"))
            {
                if (plus)
                {
                    plus = false;
                    state[0].gameObject.SetActive(false);
                    state[1].gameObject.SetActive(true);
                }
                else
                {
                    plus = true;
                    state[0].gameObject.SetActive(true);
                    state[1].gameObject.SetActive(false);
                }
            }
        }
    }
}
