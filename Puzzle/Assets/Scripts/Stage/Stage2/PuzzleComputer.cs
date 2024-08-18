using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleComputer : MonoBehaviour
{
    public LineButton[] line_Button; // �̵� ��ư(���� ��)
    public List<int> lineNum; // ( -1, 0, 1 ) �߽ɱ������� ����

    public LEDNode computerLightLine; // ��ǻ�� ���⼱
    public LEDNode activateLightLine; // Ȱ��ȭ ���⼱
    public bool activate; // Ȱ��ȭ 

    public LightningRod lightningRod; // �������� ��ġ
    public bool on; // ��ǻ�� ���� ����
    public GameObject barrier; // ���ܸ�

    public RotateObject[] gears; // ȸ�� ���
    public GameObject jumpPad; // �����е�
    public RotateObject[] RotateFloors; // Ŭ���� ����

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

        // ��� ���� 0�̸� Ȱ��ȭ 
        computerLightLine.activate = allZero; // ��ǻ�� ���⼱ Ȱ��ȭ
        activateLightLine.activate = allZero; // Ȱ��ȭ ���⼱ Ȱ��ȭ
        ActivateGear(allZero); // ��� ��� Ȱ��ȭ
        RotateFloor(allZero); // Ŭ���� ���� ȸ��
        jumpPad.SetActive(allZero); // �����е� Ȱ��ȭ
    }

    void ActivateGear(bool on)
    {
        if (on)
        {
            foreach (var gear in gears)
            {
                gear.activate = true;
            }
        }
    }
    
    void RotateFloor(bool on)
    {
        if (on)
        {
            foreach (var flooor in RotateFloors)
            {
                flooor.activate = true;
            }
        }
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