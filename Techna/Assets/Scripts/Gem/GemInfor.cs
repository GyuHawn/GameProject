using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    public GemManager gemManager;

    public string infor; // ���� 
    public TMP_Text inforText; // ���� �ؽ�Ʈ
    public int textSize; // �ؽ�Ʈ ������
    public Color textColor; // �� 
    public bool showUI; // ǥ�� �� ����
    
    public bool gemInfor; // ���� ���� ����
    public bool gem; // ���� ����

    private void Start()
    {
        // �ٹٲ� �� ���� ����
        if (!string.IsNullOrEmpty(infor))
        {
            infor = infor.Replace(@"\n", "\n");
        }

        if (inforText != null)
        {
            
            inforText.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gemInfor)
            {
                if (!showUI)
                {
                    showUI = true;
                    DisplayInfoUI(); // ���� ǥ��
                }
            }

            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // ���� �̸� Ȯ��
                Destroy(gameObject); // ���� ����
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gemInfor && showUI)
        {
            showUI = false; 
            HideInfoUI(); // ���� �����
        }
    }

    private void DisplayInfoUI() // ���� ǥ��
    {
        inforText.gameObject.SetActive(true);
        inforText.color = textColor; // ���� ����
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // ���� �����
    {
        inforText.gameObject.SetActive(false);
        inforText.color = Color.white;
        inforText.text = "";
    }
}
