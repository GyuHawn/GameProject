using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineButton : MonoBehaviour
{
    public PuzzleComputer computer;

    public GameObject[] line_Blocks; // ������ ���
    public int curruntNum;
    public int line;

    private void OnCollisionEnter(Collision collision)
    {
        if (computer.on) // ������ ����������
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // �迭�� ������ �ٲߴϴ�
                RotateArray(line_Blocks);

                // ����� ��ġ ������Ʈ�� ȣ���մϴ�
                computer.UpdateBlockPositions(line_Blocks);

                // �� ������Ʈ
                if (curruntNum == -1)
                {
                    curruntNum = 0;
                }
                else if (curruntNum == 0)
                {
                    curruntNum = 1;
                }
                else if (curruntNum == 1)
                {
                    curruntNum = -1;
                }

                computer.lineNum[line] = curruntNum;
            }
        }
    }
    void RotateArray(GameObject[] array)
    {
        if (array.Length < 3)
            return; // �迭 ���̰� 3���� ���� ��� ó������ ����

        GameObject temp = array[0]; // �迭�� ù ��° ��Ҹ� �����մϴ�
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1]; // �� ��Ҹ� �������� �̵��մϴ�
        }
        array[array.Length - 1] = temp; // ����� ù ��° ��Ҹ� �迭�� ������ ��ġ�� �����մϴ�
    }

}
