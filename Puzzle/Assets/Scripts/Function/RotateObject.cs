using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateNum; // ȸ�� �Ÿ�
    public bool x; // x�� ȸ�� ����
    public bool y; // y�� ȸ�� ����
    public bool z; // z�� ȸ�� ����

    void Update()
    {
        if (x) // x�������� ȸ��
        {          
            transform.Rotate(Vector3.right * rotateNum * Time.deltaTime);
        }
        if (y) // y�������� ȸ��
        {
            transform.Rotate(Vector3.up * rotateNum * Time.deltaTime);
        }
        if (z) // z�������� ȸ��
        {
            transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime);
        }
    }
}
