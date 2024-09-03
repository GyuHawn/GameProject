using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    private GemManager gemManager;

    public string infor; // ���� 
    public GameObject inforUI; // ���� UI
    public int inforUIWidthSize; // ��Ʈ UI �ʺ� ������
    public int inforUIHeightSize; // ��Ʈ UI ���� ������
    public TMP_Text inforText; // ���� �ؽ�Ʈ
    public int textSize; // �ؽ�Ʈ ������
    public Color textColor; // �� 
    public bool showUI; // ǥ�� �� ����

    public bool gemInfor; // ���� ���� ����
    public bool gem; // ���� ����

    private void Awake()
    {
        if (!gemManager)
            gemManager = GameObject.Find("GemManager").GetComponent<GemManager>();
    }

    private void Start()
    {
        if (infor != null)
        {
            infor = infor.Replace(@"\n", "\n"); // �ٹٲ� ���� ����
        }
        if (inforText != null)
        {
            inforText.color = textColor; // �� ����
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� �浹 ��
        if (collision.gameObject.CompareTag("Player"))
        {         
            if (gem)
            {
                gemManager.CollectGem(gameObject.name); // ���� �̸� Ȯ��
                Destroy(gameObject); // ���� ����
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �÷��̾� �浹 �� ��Ʈ ���
        if (other.gameObject.CompareTag("Player"))
        {
            if (gemInfor && !showUI)
            {
                showUI = true;
                inforUI.gameObject.SetActive(true);
                inforText.gameObject.SetActive(true);
                inforText.color = textColor;
                inforText.text = infor;

                // UI ������ ����
                RectTransform inforUIRect = inforUI.GetComponent<RectTransform>();
                inforUIRect.sizeDelta = new Vector2(inforUIWidthSize, inforUIHeightSize);

                // ��Ʈ ũ�� ����
                inforText.fontSize = textSize;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾� �浹 ����� �浹 ����
        if (other.gameObject.CompareTag("Player"))
        {
            if (gemInfor && showUI)
            {
                showUI = false;
                inforUI.gameObject.SetActive(false);
                inforText.gameObject.SetActive(false);
                inforText.text = "";
            }
        }
    }
}
