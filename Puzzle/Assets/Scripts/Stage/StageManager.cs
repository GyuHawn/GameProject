using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject player; // �÷��̾�

    public GameObject[] fullStage; // ��ü ��������

    public GameObject[] stageSetting; // �������� �� ����

    private int previousStage; // ���� �������� �� ����

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();    
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
            NextStageSetting(); // ���������� ���� �÷��̾� �������� �� ���� ������Ʈ 
            previousStage = playerMovement.currentStage; // ���� �������� �� ����
        }
    }

    void NextStageSetting() // ���� �������� �̵��� �������� ����
    {
        if (playerMovement.currentStage == 5)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 newPosition = new Vector3(1.5f, 4, 25);
                Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                controller.enabled = false;
                player.transform.position = newPosition;
                player.transform.rotation = newRotation;
                controller.enabled = true; 
            }

            playerMovement.currentStage = 6;
            stageSetting[0].SetActive(false);
            stageSetting[1].SetActive(true);
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

        // currentStage�� 15 �̻��̸� �� �̻� ������Ʈ ���� �ʵ��� (���� �ִ밪 15)
        if (playerMovement.currentStage >= 15){}
    }
}
