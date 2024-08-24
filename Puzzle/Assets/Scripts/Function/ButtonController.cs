using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MovingObject movingObject;
    public PlayerMovement player; // �÷��̾�

    public GameObject[] buttons;  // ��ư��
    public int checkCount;  // Ȯ�� ��
    public int currentCheckCount;

    private void Start()
    {
        currentCheckCount = checkCount;
    }

    private void Update()
    {
        if (currentCheckCount == 0)
        {
            bool allMatch = true;

            foreach (GameObject buttonObj in buttons)
            {
                ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
                if (buttonInfo.trueButton != buttonInfo.currentStatus)
                {
                    allMatch = false;
                    break;
                }
            }

            if (allMatch)
            {
                movingObject.activated = true;
            }
            else
            {
                currentCheckCount = checkCount;
                player.currentHealth -= 5;
            }

            // ��� ��ư�� currentStatus�� false�� �ʱ�ȭ
            foreach (GameObject buttonObj in buttons)
            {
                ButtonInfor buttonInfo = buttonObj.GetComponent<ButtonInfor>();
                buttonInfo.currentStatus = false;
                buttonInfo.renderer.material = buttonInfo.materials[0];
            }
        }
    }
}
