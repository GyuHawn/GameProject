using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObejctPostion : MonoBehaviour
{
    public Transform resetPos; // ���� ��ġ

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ������Ʈ, �÷��̾ �浹�� ���� ��ġ�� �̵�
        if (collision.gameObject.CompareTag("GrabObject") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = resetPos.position;
        }
    }
}
