using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ObjectHintDisplay : MonoBehaviour
{
    public string hintText; // ��Ʈ
    public Image hintUI; // ��Ʈ UI
    public int hintUIWidthSize; // ��Ʈ UI �ʺ� ������
    public int hintUIHeightSize; // ��Ʈ UI ���� ������
    public TMP_Text showText; // ǥ�� �ؽ�Ʈ
    public int textSize; // �ؽ�Ʈ ������
    public bool showUI; // ǥ�� �� ����

    void Start()
    {
        if (hintText != null)
        {
            hintText = hintText.Replace(@"\n", "\n"); // �ٹٲ� ���� ����
        }

        showText.gameObject.SetActive(false); // ��Ʈ ��Ȱ��ȭ
    }

    private void OnTriggerStay(Collider other)
    {
        // �÷��̾� �浹 �� ��Ʈ ���
        if (other.gameObject.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                hintUI.gameObject.SetActive(true);
                showText.gameObject.SetActive(true);
                showText.text = hintText; // ��Ʈ ����

                // UI ������ ����
                RectTransform hintUIRect = hintUI.GetComponent<RectTransform>();
                hintUIRect.sizeDelta = new Vector2(hintUIWidthSize, hintUIHeightSize);

                // ��Ʈ ũ�� ����
                showText.fontSize = textSize;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾� �浹 ����� �浹 ����
        if (other.gameObject.CompareTag("Player"))
        {
            if (showUI)
            {
                showUI = false;
                hintUI.gameObject.SetActive(false);
                showText.gameObject.SetActive(false);
                showText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
            }
        }
    }
}
