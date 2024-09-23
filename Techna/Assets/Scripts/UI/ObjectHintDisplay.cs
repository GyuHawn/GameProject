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
        if (!string.IsNullOrEmpty(hintText))
        {
            hintText = hintText.Replace(@"\n", "\n"); // �ٹٲ� ���� ����
        }

        ToggleHintUI(false); // ��Ʈ UI ��Ȱ��ȭ
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !showUI)
        {
            ToggleHintUI(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && showUI)
        {
            ToggleHintUI(false);
        }
    }

    // UI Ȱ��ȭ/��Ȱ��ȭ �Լ�
    private void ToggleHintUI(bool show)
    {
        showUI = show;
        hintUI.gameObject.SetActive(show);
        showText.gameObject.SetActive(show);

        if (show)
        {
            showText.text = hintText; // ��Ʈ ����
            // UI ũ�� ����
            RectTransform hintUIRect = hintUI.GetComponent<RectTransform>();
            hintUIRect.sizeDelta = new Vector2(hintUIWidthSize, hintUIHeightSize);
            showText.fontSize = textSize; // ��Ʈ ũ�� ����
        }
        else
        {
            showText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        }
    }
}
