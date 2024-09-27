using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunctionInfor : MonoBehaviour
{
    public string infor; // ����
    public TMP_Text inforText; // ���� �ؽ�Ʈ
    public int textSize; // �ؽ�Ʈ ������
    public Color textColor; // �ؽ�Ʈ ��
    public bool showUI; // ǥ�� �� ����

    void Start()
    {
        // �ٹٲ� �� ���� ����
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }

        if (inforText != null)
        {
            inforText.color = textColor; // ���� ����
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                DisplayInfoUI(); // ���� ǥ��
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && showUI)
        {
            showUI = false;
            HideInfoUI(); // ���� �����
        }
    }

    private void DisplayInfoUI() // ���� ǥ��
    {
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // ���� �����
    {
        inforText.text = "";
    }
}
