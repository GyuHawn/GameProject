using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; } // �̱��� ����

    public PlayerMovement playerMovement;

    public GameObject[] fullStage; // ��ü ��������

    public GameObject[] stageSetting; // �������� �� ����

    private int previousStage; // ���� �������� �� ����


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        previousStage = playerMovement.currentStage; // �������� �� �ʱ�ȭ
        UpdateStage(); // �ʱ� �������� ����
    }

    void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            UpdateStage(); // �÷��̾� ��ġ�� ���� �������� ��/Ȱ��ȭ
            StageSetting(); // ���������� ���� �÷��̾� �������� �� ���� ������Ʈ 
            previousStage = playerMovement.currentStage; // ���� �������� �� ����
        }
    }

    void UpdateStage() // �÷��̾� ��ġ�� ���� �������� ��/Ȱ��ȭ
    {
        // ��� �������� ��Ȱ��ȭ
        foreach (GameObject stage in fullStage)
        {
            stage.SetActive(false);
        }

        // ���� �������� ���� ���� Ȱ��ȭ ���� ����
        int start = Mathf.Max(0, playerMovement.currentStage - 1);
        int end = Mathf.Min(fullStage.Length, playerMovement.currentStage + 3);

        for (int i = start; i < end; i++)
        {
            fullStage[i].SetActive(true);
        }

        // ���� currentStage�� 4 �̻��̸� �� �̻� ������Ʈ���� ����
        if (playerMovement.currentStage >= 4){}
    }

    void StageSetting() // ���������� ���� �÷��̾� �������� �� ���� ������Ʈ 
    {
        ResetStageSetting();

        if (playerMovement.currentStage >= 0 && playerMovement.currentStage <= 5)
        {
            stageSetting[0].gameObject.SetActive(true);
        }
        else if(playerMovement.currentStage >= 5 && playerMovement.currentStage <= 13)
        {
            stageSetting[1].gameObject.SetActive(true);
        }
    }
    void ResetStageSetting() // �������� �� ���� ������Ʈ �ʱ�ȭ
    {
        foreach (GameObject stage in stageSetting)
        {
            stage.SetActive(false);
        }
    }
}
