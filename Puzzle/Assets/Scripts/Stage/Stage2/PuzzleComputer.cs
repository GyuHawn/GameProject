using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    public LineButton[] line_Button; // �̵� ��ư(���� ��)
    public List<int> lineNum; // ( -1, 0, 1 ) �߽ɱ������� ����

    public LEDNode lightLine;
    public bool activate; // Ȱ��ȭ 

    public LightningRod lightningRod; // �������� ��ġ
    public bool on; // ��ǻ�� ���� ����
    public GameObject barrier; // ���ܸ�

    void Start()
    {
        for (int i = 0; i <= 6; i++)
        {
            lineNum.Add(line_Button[i].curruntNum);
        }     
    }

    private void Update()
    {
        if (on)
        {
            on = false;
            barrier.SetActive(false);
            OnLight();
        }
        else
        {
            if (lightningRod.activate)
            {
                on = true;
            }
        }
    }

    void OnLight()
    {
        // lineNum�� ��� ���� 0���� Ȯ��
        bool allZero = true;
        foreach (int num in lineNum)
        {
            if (num != 0)
            {
                allZero = false;
                break;
            }
        }

        // ��� ���� 0�̸� lightLine.activate�� true�� ����
        lightLine.activate = allZero;
    }

    // ����� ��ġ�� ������Ʈ
    public void UpdateBlockPositions(GameObject[] blocks)
    {
        if (blocks != null)
        {
            blocks[0].transform.position += new Vector3(0, 1.7f, 0);
            blocks[1].transform.position += new Vector3(0, 1.7f, 0);
            blocks[2].transform.position += new Vector3(0, -3.4f, 0);
        }
    }
}