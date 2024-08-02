using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveNum; // �̵� �Ÿ�
    public bool x; // x�� �̵� ����
    public bool y; // y�� �̵� ����
    public bool z; // z�� �̵� ����

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (x) // x�������� �̵�
        {
            move.x = moveNum * Time.deltaTime;
        }
        if (y) // y�������� �̵� 
        {
            move.y = moveNum * Time.deltaTime;
        }
        if (z) // z�������� �̵�
        {
            move.z = moveNum * Time.deltaTime;
        }

        transform.Translate(move);
    }
}
