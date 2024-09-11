using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CharacterController controller;

    public GameObject player; // �÷��̾�

    public GameObject[] stageSetting; // �������� �� ����

    private int previousStage; // ���� �������� �� ����

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        controller = player.GetComponent<CharacterController>();
    }

    void Start()
    {
        previousStage = playerMovement.currentStage; // �������� �� �ʱ�ȭ
    }

    void Update()
    {
        if (playerMovement.currentStage != previousStage)
        {
            NextStageSetting(); // ���������� ���� �÷��̾� �������� �� ���� ������Ʈ 
            previousStage = playerMovement.currentStage; // ���� �������� �� ����
        }
    }

    void NextStageSetting() // ���� �������� �̵��� �������� ����
    {
        switch (playerMovement.currentStage)
        {
            case 2:
                MovePlayer(new Vector3(-400f, 4, -375), Quaternion.Euler(0, 180, 0));
                playerMovement.currentStage = 0;
                stageSetting[0].SetActive(false);
                stageSetting[1].SetActive(true);
                break;
             // ���� �������� �߰� �� �� �߰�
        }
    }

    void MovePlayer(Vector3 position, Quaternion rotation)
    {
        if (controller != null)
        {
            controller.enabled = false;
            player.transform.position = position;
            player.transform.rotation = rotation;
            controller.enabled = true;
        }
    }
}
