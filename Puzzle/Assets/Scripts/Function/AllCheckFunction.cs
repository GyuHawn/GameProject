using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCheckFunction : MonoBehaviour
{
    public GameObject[] plates; // Ȯ���� ����

    public bool activate; // Ȱ��ȭ ����
    public bool on; // ���� ���� 

    public bool door; // ��
    public bool stairs; // ���
    public bool controller; // ��ư

    public GameObject targetObj;
    public bool target; // �ڽ�����

    void Start()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                // �� ������ activationChanged �̺�Ʈ ����, Ȱ��ȭ ���� ����
                plateFunction.activationChanged += CheckAllPlatesActivated;
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                // �̺�Ʈ ���� ����
                plateFunction.activationChanged -= CheckAllPlatesActivated;
            }
        }
    }

    void Update()
    {
        CheckAllPlatesActivated(false);
    }

    void CheckAllPlatesActivated(bool dummy) // ��� ���� Ȱ��ȭ �� �� ����
    {
        bool allPlatesActivated = true;

        foreach (GameObject plate in plates) // ��� ���� Ȱ��ȭ Ȯ��
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                if (!plateFunction.activate)
                {
                    // ���� ��Ȱ��ȭ �� ��ü ���� false
                    allPlatesActivated = false;
                    break;
                }
            }
        }

        if (allPlatesActivated) // ��� ���� Ȱ��ȭ �� �� ����
        {
            activate = true;
            CheckObject();
        }
    }

    void CheckObject()
    {
        if (door) // �� - ������
        {
            if (!target)
            {
                MovingObject(gameObject);
            }
            else
            {
                MovingObject(targetObj);
            }
        }
        else if (stairs) // ��� - ��� Ȱ��ȭ
        {
            ActivatedStairs();
        }
        else if (controller)
        {
            ActivatedController();
        }
    }

    void ActivatedStairs() // ��� Ȱ��ȭ
    {
        if (!on)
        {
            on = true;
            ActivatedStairs stairsObj = gameObject.GetComponent<ActivatedStairs>();
            stairsObj.activated = true;
        }
    }
    
    void ActivatedController()
    {
        if (!on)
        {
            on = true;
            ActivatedController buttonsObj = gameObject.GetComponent<ActivatedController>();
            buttonsObj.activated = true;
        }
    }

    void MovingObject(GameObject obj) // �� ����
    {
        activate = false;
        if (!on)
        {
            on = true;
            
            MovingObject move = obj.GetComponent<MovingObject>();
            move.activated = true;
        }
    }
}
