using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject spawnerDoor; // ������ �Ա�
    public GameObject checkObj; // �۵� ���� ������Ʈ(���� ��..)
    
    public bool open; // Ȱ��ȭ ����

    void Start()
    { 
        // �ϴ� �����϶��� (��Ȳ�� ���� ���� ����)
        PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
        if (plateFunction != null)
        {
            // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
            plateFunction.activationChanged += OpenSpawnerCheck;
        }
    }
    
    void Update()
    {
        OpenSpawnerCheck(false); // ������ ���� Ȯ��
    }

    void OpenSpawnerCheck(bool dummy)
    {
        bool activated = true;

        PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
        if (!plateFunction.activate)
        {
            // ���� ��Ȱ��ȭ �� ��ü ���� false
            activated = false;
        }


        if (activated) // ����
        {
            open = true;
            Open();
        }
        
    }

    void Open()
    {
        spawnerDoor.SetActive(false);
    }
}
