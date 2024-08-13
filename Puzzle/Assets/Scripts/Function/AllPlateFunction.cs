using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlateFunction : MonoBehaviour
{
    public GameObject[] plates; // Ȯ���� ����

    public bool activate; // Ȱ��ȭ ����
    public bool open; // ���� ���� 

    public bool door; // ��
    public bool stairs; // ���

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
            OpenDoor();
        }
        else if (stairs) // ��� - ��� Ȱ��ȭ
        {
            ActivatedStairs();
        }
    }

    void ActivatedStairs() // ��� Ȱ��ȭ
    {
        if (!open)
        {
            open = true;
            ActivatedStairs stairsObj = gameObject.GetComponent<ActivatedStairs>();
            stairsObj.activated = true;
        }
    }

    void OpenDoor() // �� ����
    {
        activate = false;
        if (!open)
        {
            open = true;
            StartCoroutine(MovingDoor());
        }

        IEnumerator MovingDoor()
        {
            float elapsedTime = 0f;
            float duration = 3f;
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + new Vector3(0, 16, 0);

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
        }
    }
}
