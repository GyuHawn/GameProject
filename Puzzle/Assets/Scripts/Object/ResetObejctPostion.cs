using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // ���� ��ġ

    private void OnTriggerEnter(Collider other)
    {
        // ���� ������Ʈ, �÷��̾ �浹�� ���� ��ġ�� �̵�
        if (other.CompareTag("GrabObject") || other.CompareTag("Player"))
        {
            // �÷��̾ CharacterController�� ����ϰ� �ִ� ���
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false; // CharacterController ��Ȱ��ȭ
                other.transform.position = resetPos.position; // ��ġ �̵�
                characterController.enabled = true; // CharacterController ��Ȱ��ȭ
            }
            else
            {
                // �Ϲ� ������Ʈ�� transform.position���� �̵�
                other.transform.position = resetPos.position;
            }
        }
    }
}
