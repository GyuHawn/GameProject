using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    private GemCombination gemCombination;
    private GemManager gemManager;

    public GameObject bulletUI; // �Ѿ� ���� UI  
    public GameObject bulletMenuUI; // �Ѿ� �޴� UI  
    public GameObject bulletGemUI; // ���� �Ѿ� ���� UI  

    public GameObject attributeUI; // �Ӽ� ����
    public GameObject attributeMenuUI; // �Ӽ� �޴� UI
    public GameObject[] attributeGemUI; // ���� �Ӽ� ����

    public GameObject functionUI; // ��� ����
    public GameObject functionMenuUI; // ��� �޴� UI
    public GameObject[] functionGemUI; // ���� ��� ����

    public int selectGemNum;

    // �� Ȱ��ȭ ����
    private bool selectBullet;

    private void Awake()
    {
        if (!gemCombination)
            gemCombination = FindObjectOfType<GemCombination>();
        if (!gemManager)
            gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        if (!selectBullet) // �Ѿ�, �Ӽ�, ��� �޴� �� ����
        {
            // Ű �Է¿� ���� UI ���� �ݱ�
            OpenGemUI("1", bulletMenuUI, 1); // 1��Ű, �޴�, ���ð�
            OpenGemUI("2", attributeMenuUI, 2);
            OpenGemUI("3", functionMenuUI, 3);
        }
        else if(selectBullet) // ������ �޴� �ȿ��� ������ ����
        {
            SelectFunction(); // ��� ����
        }
    }

    public void ActivateGemUI() // ���¿� ���� UI Ȱ��ȭ
    {
        Debug.Log("Ȱ��ȭ");
        bulletGemUI.SetActive(gemManager.onLarge);

        attributeGemUI[0].SetActive(gemManager.onControl);
        attributeGemUI[1].SetActive(gemManager.onFire);
        attributeGemUI[2].SetActive(gemManager.onWater);
        attributeGemUI[3].SetActive(gemManager.onElectricity);

        functionGemUI[0].SetActive(gemManager.onDestruction);
        functionGemUI[1].SetActive(gemManager.onPenetrate);
        functionGemUI[2].SetActive(gemManager.onDiffusion);
        functionGemUI[3].SetActive(gemManager.onUpgrade);
        functionGemUI[4].SetActive(gemManager.onQuick);
    }

    private void OpenGemUI(string KeyNum, GameObject menuUI, int menuNum) // Ű �Է¿� ���� UI ���� �ݱ�
    {
        if (Input.GetButtonDown(KeyNum)) // Ű�Է� ��
        {
            selectBullet = true;
            ResetUIStates(); // ��� UI ���� �ʱ�ȭ �� �ݱ�
            menuUI.SetActive(true); // �޴� ����
            selectGemNum = menuNum; // ������ �޴� ��
        }
    }

    private void SelectFunction() // ��� ����
    {
        if (selectGemNum == 1) // �Ѿ� �޴�
        {
            SelectFunctionValue(1, ref gemCombination.selectBulletNum, 2); // ���� �޴�, ������ ����, ������
        }
        else if (selectGemNum == 2) // �Ӽ� �޴�
        {
            SelectFunctionValue(2, ref gemCombination.selectAttributeNum, 4);
        }
        else if (selectGemNum == 3) // ��� �޴�
        {
            SelectFunctionValue(3, ref gemCombination.selectFunctionNum, 5);
        }
    }

    private void SelectFunctionValue(int menuNum, ref int selectedFunction, int choiceNum) // �޴� �� ������ ó��
    {
        if (menuNum == 1)
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
        }
        else if(menuNum == 2)
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // ������ �������� 0���� ó��
            {
                selectedFunction = 0;
                ConfirmSelection();
            }
        }
        else if(menuNum == 3) 
        {
            for (int i = 1; i <= choiceNum; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    selectedFunction = i;
                    ConfirmSelection();
                    return;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1 + choiceNum)) // ������ �������� 0���� ó��
            {
                selectedFunction = 0;
                ConfirmSelection();
            }
        }
    }

    private void ConfirmSelection() // ���� Ȯ�� ó��
    {
        selectBullet = false;
        ResetUIStates();
    }

    private void ResetUIStates()  // ��� UI ���� �ʱ�ȭ �� �ݱ�
    {
        bulletMenuUI.SetActive(false);
        attributeMenuUI.SetActive(false);
        functionMenuUI.SetActive(false);
    }
}
