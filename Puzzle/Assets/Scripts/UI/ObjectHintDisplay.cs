using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHintDisplay : MonoBehaviour
{
    public string hintText; // ��Ʈ
    public GameObject hintUI; // ��Ʈ UI
    public TMP_Text showText; // ǥ�� �ؽ�Ʈ
    public bool showUI; // ǥ�� �� ����

    void Start()
    {
        showText.gameObject.SetActive(false); // ��Ʈ ��Ȱ��ȭ
    }

    private void OnCollisionStay(Collision collision)
    {
        // �÷��̾� �浹 �� ��Ʈ ���
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!showUI)
            {
                showUI = true;
                hintUI.gameObject.SetActive(true);
                showText.gameObject.SetActive(true);
                showText.text = hintText; // ��Ʈ ����
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �÷��̾� �浹 ����� �浹 ����
        if (collision.gameObject.CompareTag("Player"))
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
