using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public IMovingState currentState;
    private ICommand command;
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
    public bool isMoving = false; // �̵� �� ����

    // ��ü�̵� ����
    public GameObject movingObject; // �̵���ų ��ü
    public Transform objectMovePos; // �̵���ų ��ġ

    public bool autoMoving; // ��� �̵� (�ݺ� �̵�)
    public bool controlMoving; // ���� �̵� (�ѹ��� �����̵���)
    public bool controlAutoMoving; // Ư����ġ ���� �ݺ� �̵�(Ư����ġ�� �ݺ��̵�)
    public bool objectMoving; // ��ü �̵� (�ٸ� ������Ʈ�� �̵���Ű����)

    private void Start()
    {
        currentState = new IdleState();
        currentPosition = transform.localPosition; // �ʱ� ��ġ ����
        targetPosition = CalculateTargetPosition(); // ��ǥ ��ġ ����
    }

    private void Update()
    {
        currentState.Update(this);
    }
    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    private void OnEnable()
    {
        if(gem != null)
        {
            gem.activationChanged += HandleGemActivated;
        }
    }

    private void OnDisable()
    {
        if(gem != null)
        {
            gem.activationChanged -= HandleGemActivated;
        }
    }

    private void HandleGemActivated(bool activate)
    {
        if (activate)
        {
            if (controlMoving)
            {
                StartMoving();
            }
            else if (objectMoving)
            {
                if (CheckObject())
                {
                    StartMoving();
                }
            }
            else if (autoMoving)
            {
                StartRepeatingMove();
            }
            else if (controlAutoMoving && CheckObject())
            {
                StartRepeatingMoveAtPosition(objectMovePos.position);
            }
        }
    }


    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition));
        }
        if (command != null)
        {
            command.Execute(this);
        }
    }

    // �ٸ� ��ü�� �̵�
    public void StartMovingObject()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(movingObject, objectMovePos.position));
        }
    }

    // �ݺ� �̵� ����
    public void StartRepeatingMove()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(RepeatMove(targetPosition));
        }
    }

    // Ư�� ��ġ�� �ݺ� �̵� ����
    public void StartRepeatingMoveAtPosition(Vector3 position)
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(RepeatMove(position));
        }
    }

    // Ư�� ������Ʈ�� Ȱ��ȭ ���� Ȯ��
    public bool CheckObject()
    {
        if (plateObj)
        {
            PlateFunction plateFunction = checkObj.GetComponent<PlateFunction>();
            return plateFunction != null && plateFunction.activate;
        }
        else if (lightObj)
        {
            LightningRod lightingFunction = checkObj.GetComponent<LightningRod>();
            return lightingFunction != null && lightingFunction.activate;
        }
        else if (electrictyObj)
        {
            CheckElectricity electricityFunction = checkObj.GetComponent<CheckElectricity>();
            return electricityFunction != null && electricityFunction.activate;
        }
        else if (digitalLockObj)
        {
            DigitalLock digitalLockFunction = checkObj.GetComponent<DigitalLock>();
            return digitalLockFunction != null && digitalLockFunction.activate;
        }
        else if (keyLockObj)
        {
            KeyLock keyLockFunction = checkObj.GetComponent<KeyLock>();
            return keyLockFunction != null && keyLockFunction.activate;
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

    public IEnumerator MovePosition(GameObject obj, Vector3 targetPos) // �̵�
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
