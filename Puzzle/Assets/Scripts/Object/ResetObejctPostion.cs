using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    public Transform resetPos; // ���� ��ġ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabObject") || other.CompareTag("Player"))
        {     
            if (other.CompareTag("Player"))  // �÷��̾�
            {
                CharacterController characterController = other.GetComponent<CharacterController>();
                characterController.enabled = false;
                other.transform.position = resetPos.position; 
                characterController.enabled = true;
            }
            else // ������Ʈ
            {
                // ������Ʈ �̵�
                other.transform.position = resetPos.position;
            }
        }
    }
}
