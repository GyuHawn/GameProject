using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public ActivateGem gem; // ���� Ȯ��

    // �̵�����
    public float moveNum; // �̵� �Ÿ�
    public bool x; // x�� �̵� ����
    public bool y; // y�� �̵� ����
    public bool z; // z�� �̵� ����

    public bool activated; // �׳� �̵� �غ�
    public GameObject checkObj; // üũ�� ������Ʈ

    public bool plateObj; // ���� ����
    public bool lightObj; // ����Ʈ ����
    public bool electrictyObj; // ���� ����
    public bool digitalLockObj; // ���� �����ġ ����
    public bool keyLockObj; // Ű �����ġ ����

    private Vector3 currentPosition; // �ʱ� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    public float moveDuration; // ���� �ð�
    private bool isMoving = false; // �̵� �� ����

    // ��ü�̵� ����
    public GameObject movingObject; // �̵���ų ��ü
    public Transform objectMovePos; // �̵���ų ��ġ

    public bool autoMoving; // ��� �̵� (�ݺ� �̵�)
    public bool controlMoving; // ���� �̵� (�ѹ��� �����̵���)
    public bool controlAutoMoving; // Ư����ġ ���� �ݺ� �̵�(Ư����ġ�� �ݺ��̵�)
    public bool objectMoving; // ��ü �̵� (�ٸ� ������Ʈ�� �̵���Ű����)

    private void Start()
    {
        currentPosition = transform.localPosition; // �ʱ� ��ġ ����
        targetPosition = CalculateTargetPosition(); // ��ǥ ��ġ ����
    }

    void Update()
    {
        // �����̵�, ����o, �ڽ�����, �̵� �� ����
        if (gem != null && gem.activate && !isMoving)
        {
            if (controlMoving) // ���� �̵�
            {
                isMoving = true;
                StartCoroutine(MovePosition(gameObject, targetPosition));
            }
            else if (objectMoving) // ��ü �̵�
            {
                if (objectMoving && gem != null && gem.activate && !isMoving)
                {
                    isMoving = true;
                    StartCoroutine(MovePosition(movingObject, objectMovePos.position));
                }
            }
        }

        // ��ü�̵�, ������Ʈ üũ, �̵� �� ����
        if(objectMoving && !isMoving)
        {
            if (checkObj != null)
            {
                bool activate = CheckObject();

                if (activate)
                {
                    isMoving = true;
                    StartCoroutine(MovePosition(gameObject, targetPosition));
                }
            }
            else if(checkObj == null && activated)
            {
                isMoving = true;
                StartCoroutine(MovePosition(gameObject, targetPosition));
            }
        }

        // �ݺ��̵�, �̵� �� ����
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove(targetPosition));
        }

        // Ư����ġ ���� �ݺ� �̵�, �̵� �� ����
        if (controlAutoMoving && checkObj != null && !isMoving)
        {
            bool activate = CheckObject();
            
            if (activate)
            {
                isMoving = true;
                StartCoroutine(RepeatMove(objectMovePos.position));
            }
        }
    }

    bool CheckObject() // Ư�� ������Ʈ�� Ȱ��ȭ ���� Ȯ��
    {
        if (plateObj) // ����
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            if (plateFunction != null)
            {
                return plateFunction.activate;
            }
        }
        else if (lightObj) // ����Ʈ
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            if (lightingFunction != null)
            {
                return lightingFunction.activate;
            }
        }
        else if (electrictyObj) // ����
        {
            CheckElectricity electricityFunction = checkObj.GetComponent<CheckElectricity>();
            if (electricityFunction != null)
            {
                return electricityFunction.activate;
            }
        }
        else if (digitalLockObj) // ���� �����ġ
        {
            DigitalLock digitalLockFunction = checkObj.GetComponent<DigitalLock>();
            if(digitalLockFunction != null)
            {
                return digitalLockFunction.activate;
            }

        }
        else if (keyLockObj) // Ű �����ġ
        {
            KeyLock keyLockFunction = checkObj.GetComponent<KeyLock>();
            if (keyLockFunction != null)
            {
                return keyLockFunction.activate;
            }
        }
        return false;
    }


    public void MoveObject() // Ÿ��ũ��Ʈ ��� �̵��ڵ�
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
    }

    private Vector3 CalculateTargetPosition() // ��ǥ ��ġ ���
    {
        return currentPosition + new Vector3(x ? moveNum : 0, y ? moveNum : 0, z ? moveNum : 0);
    }

    IEnumerator RepeatMove(Vector3 targetPosition) // �ݺ��̵�
    {
        while (true)
        {
            yield return MovePosition(gameObject, targetPosition);
            yield return MovePosition(gameObject, currentPosition);
        }
    }

    IEnumerator MovePosition(GameObject obj, Vector3 targetPos) // �̵�
    {
        Vector3 startPosition = obj.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // õõ�� �̵�
        {
            obj.transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPos; // ��ǥ ����
        isMoving = false;
    }
}
