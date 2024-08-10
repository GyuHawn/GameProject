using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemInfor : MonoBehaviour
{
    private GemManager gemManager;

    public string infor; // ���� 
    public GameObject inforUI; // ���� UI
    public TMP_Text inforText; // ���� �ؽ�Ʈ
    public Color textColor; // �� 

    private void Awake()
    {
        if (!gemManager)
            gemManager = GameObject.Find("GemManager").GetComponent<GemManager>();

        infor = infor.Replace(@"\n", "\n"); // �ٹٲ� ���� ����
        inforText.color = textColor; // �� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� �浹 ��
        if (collision.gameObject.CompareTag("Player"))
        {
            gemManager.CollectGem(gameObject.name); // ���� �̸� Ȯ��
            Destroy(gameObject); // ���� ����

            StartCoroutine(ShowInforText(3f)); // ���� ǥ��

        }
    }

    IEnumerator ShowInforText(float time) // ���� ǥ�� �� ��Ȱ��ȭ 
    {
        inforUI.gameObject.SetActive(true);
        inforText.gameObject.SetActive(true);
        inforText.text = infor;

        yield return new WaitForSeconds(time);

        inforUI.gameObject.SetActive(false);
        inforText.gameObject.SetActive(false);
        inforText.text = "";
    }
}
