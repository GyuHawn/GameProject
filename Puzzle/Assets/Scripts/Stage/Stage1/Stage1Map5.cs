using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Map5 : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject[] plates;

    public bool activate;

    public GameObject player;

    void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                plateFunction.OnActivationChanged += CheckAllPlatesActivated;
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
                plateFunction.OnActivationChanged -= CheckAllPlatesActivated;
            }
        }
    }

    void Update()
    {
        // Stage�� �������� ���� �� Ȱ��ȭ Ȯ��
        CheckAllPlatesActivated(false); // �ʱ�ȭ������ false�� �����մϴ�.
    }

    void CheckAllPlatesActivated(bool dummy)
    {
        bool allPlatesActivated = true;

        foreach (GameObject plate in plates)
        {
            PlateFunction plateFunction = plate.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                if (!plateFunction.activate)
                {
                    allPlatesActivated = false;
                    break;
                }
            }
            else
            {
                allPlatesActivated = false;
                break;
            }
        }

        if (allPlatesActivated)
        {
            activate = true;
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        activate = false;
        Destroy(gameObject);
    }
}
