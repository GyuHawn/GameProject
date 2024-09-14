using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    public GemManager gemManager;

    public string infor; // ���� 
    public GameObject inforUI; // ���� UI
    public int inforUIWidthSize; // ���� UI �ʺ� ������
    public int inforUIHeightSize; // ���� UI ���� ������
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
            inforText.color = textColor; // ���� ����
            inforText.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ
        }

        if (inforUI != null)
        {
            RectTransform inforUIRect = inforUI.GetComponent<RectTransform>();
            inforUIRect.sizeDelta = new Vector2(inforUIWidthSize, inforUIHeightSize); // UI ũ�� ����
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
        inforUI.SetActive(true);
        inforText.gameObject.SetActive(true);
        inforText.text = infor;
        inforText.fontSize = textSize;
    }

    private void HideInfoUI() // ���� �����
    {
        inforUI.SetActive(false);
        inforText.gameObject.SetActive(false);
        inforText.text = "";
    }
}
