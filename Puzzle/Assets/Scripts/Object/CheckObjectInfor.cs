using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckObjectInfor : MonoBehaviour
{
    public int expansValue; // �ִ� Ȯ�尪
    public int reducedValue; // �ִ� Ȯ�尪
    public int currentValue; // ���簪

    public float weight; // ����

    public bool expansion; // ũ�� ���� ���� ������Ʈ����

    public string[] collidingTag = {"Floor", "Wall"}; // �浹 Ȯ���� �±�
    public bool colliding; // �浹������

    private void OnCollisionStay(Collision collision)
    {
        // �±� Ȯ�� �� �浹 Ȯ��
        if (System.Array.Exists(collidingTag, tag => tag == collision.gameObject.tag))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
