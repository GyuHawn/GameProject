using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower;
    public Vector3 jumpDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // �������� �����ϱ� ���� Vector3�� �����մϴ�.
                Vector3 jump = jumpDirection * jumpPower;

                // PlayerMovement ��ũ��Ʈ�� �ִٸ�, ���� ���� �������� �� �ֽ��ϴ�.
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.ApplyJump(jump);
                }
            }
        }
    }
}
