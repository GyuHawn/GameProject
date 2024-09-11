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
        if (computer.on && collision.gameObject.CompareTag("Bullet")) // ������ ���� �ְ� Bullet�� ��
        {
            // �迭 ���� ���� 
            if (line_Blocks.Length >= 3)
                RotateArray(line_Blocks);

            // ��� ��ġ ������Ʈ
            computer.UpdateBlockPositions(line_Blocks);

            // �� ������Ʈ
            curruntNum = (curruntNum + 2) % 3 - 1;

            // ��ǻ�� lineNum �� ����
            computer.lineNum[line] = curruntNum;
        }
    }

    void RotateArray(GameObject[] array)
    {
        if (array.Length < 3)
            return; // �迭 3�̸��϶� ���� (���� ���� ����)

        GameObject temp = array[0]; // �迭 ù ��° ��� ����
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1]; // �� ��Ҹ� �������� �̵�
        }
        array[array.Length - 1] = temp; // ����� ù ��° ��Ҹ� �迭�� ������ ��ġ�� ����

    }
}
