using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;

    public GameObject bulletUI; // �Ѿ� ���� UI  
    public GameObject bulletMenuUI; // �Ѿ� �޴� UI  
    public GameObject attributeUI; // �Ӽ� ����
    public GameObject attributeMenuUI; // �Ӽ� �޴� UI
    public GameObject functionUI; // ��� ����
    public GameObject functionMenuUI; // ��� �޴� UI

    // �� Ȱ��ȭ ����
    private bool onBulletUI;
    private bool onAttributeUI;
    private bool onFunctionUI;

    private void Awake()
    {
        if (!gemCombination)
            gemCombination = FindObjectOfType<GemCombination>();
    }

    void Update()
    {
        // Ű �Է¿� ���� UI ���� �ݱ�
        OpenGemUI("1", ref onBulletUI, bulletMenuUI);
        OpenGemUI("2", ref onAttributeUI, attributeMenuUI);
        OpenGemUI("3", ref onFunctionUI, functionMenuUI);
    }

    private void OpenGemUI(string KeyNum, ref bool uiState, GameObject menuUI) // Ű �Է¿� ���� UI ���� �ݱ�
    {
        if (Input.GetButtonDown(KeyNum)) // Ű�Է� ��
        {
            if (!uiState) // Ȱ��ȭ ���� Ȯ��
            {
                ResetUIStates(); // ��� UI ���� �ʱ�ȭ �� �ݱ�
                uiState = true; // ���� Ȱ��ȭ
                menuUI.SetActive(true); // �޴� ����
            }
            else
            {
                uiState = false;
                menuUI.SetActive(false);
            }
        }
    }

    private void ResetUIStates()  // ��� UI ���� �ʱ�ȭ �� �ݱ�
    {
        onBulletUI = false; 
        bulletMenuUI.SetActive(false);

        onAttributeUI = false;
        attributeMenuUI.SetActive(false);

        onFunctionUI = false;
        functionMenuUI.SetActive(false);
    }
}
