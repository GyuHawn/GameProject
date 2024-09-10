using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject player; // �÷��̾�

    public GameObject[] stageSetting; // �������� �� ����

    private int previousStage; // ���� �������� �� ����

    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();    
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
        if (playerMovement.currentStage == 2)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 newPosition = new Vector3(-400f, 4, -375);
                Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                controller.enabled = false;
                player.transform.position = newPosition;
                player.transform.rotation = newRotation;
                controller.enabled = true; 
            }

            playerMovement.currentStage = 0;
            stageSetting[0].SetActive(false);
            stageSetting[1].SetActive(true);
        }
    }
}
