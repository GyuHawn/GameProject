using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private ControlGem controlGem;

    // �̵�����
    public float moveNum; // �̵� �Ÿ�
    public bool x; // x�� �̵� ����
    public bool y; // y�� �̵� ����
    public bool z; // z�� �̵� ����

    public GameObject gem;

    private Vector3 currentPosition; // �ʱ� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    public float moveDuration; // ���� �ð�
    private bool isMoving = false; // �̵� �� ����

    // ��ü�̵� ����
    public GameObject movingObject; // �̵���ų ��ü
    public Transform objectMovePos; // �̵���ų ��ġ

    public bool autoMoving; // ��� �̵� (�ݺ����� ������)
    public bool controlMoving; // ���� �̵� (�ѹ��� �����̵���)
    public bool objectMoving; // ��ü �̵� (�ٸ� ������Ʈ�� �̵���Ű����)

    private void Start()
    {
        if(gem != null)
        {
            controlGem = gem.GetComponent<ControlGem>();
        }

        currentPosition = transform.localPosition; // �ʱ� ��ġ ����
        targetPosition = CalculateTargetPosition(); // ��ǥ ��ġ ����
    }

    void Update()
    {
        // �����̵�, ����o, �ڽ�����, �̵� �� ����
        if (controlMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject, targetPosition, false));
        }
        // ��ü�̵�, ����o, ��ü����, �̵� �� ����
        if (objectMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(movingObject, objectMovePos.position, false));
        }
        // �ݺ��̵�, �̵� �� ����
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove());
        }
    }

    public void MoveObject() // Ÿ��ũ��Ʈ ��� �̵��ڵ�
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(gameObject,targetPosition, false));
        }
    }

    private Vector3 CalculateTargetPosition() // ��ǥ ��ġ ���
    {
        Vector3 target = currentPosition;

        if (x) // x�������� �̵�
        {
            target.x += moveNum;
        }
        if (y) // y�������� �̵� 
        {
            target.y += moveNum;
        }
        if (z) // z�������� �̵�
        {
            target.z += moveNum;
        }

        return target;
    }

    IEnumerator RepeatMove() // �ݺ��̵�
    {
        while (true)
        {
            yield return MovePosition(gameObject, targetPosition, false);
            yield return MovePosition(gameObject, currentPosition, false);
        }
    }

    IEnumerator MovePosition(GameObject obj, Vector3 targetPos, bool resetMovingFlag) // �̵�
    {
        Vector3 startPosition = Vector3.zero;

        // ������ġ ����
        if (autoMoving || controlMoving)
        {
            startPosition = transform.localPosition; 
        }
        else if (objectMoving)
        {
            startPosition = obj.transform.localPosition;
        }
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // õõ�� �̵�
        {
            obj.transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPos; // ��ǥ ����

        if (resetMovingFlag) // �̵� ����
        {
            isMoving = false;
        }
    }
}
