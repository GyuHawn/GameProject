using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private ControlGem controlGem;

    public float moveNum; // �̵� �Ÿ�
    public bool x; // x�� �̵� ����
    public bool y; // y�� �̵� ����
    public bool z; // z�� �̵� ����

    public GameObject gem;

    private Vector3 currentPosition; // �ʱ� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    public float moveDuration; // ���� �ð�
    private bool isMoving = false; // �̵� �� ����

    public bool autoMoving; // ��� �̵�
    public bool controlMoving; // ���� �̵�

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
        // �����̵�, ����o, ���� Ȯ��, �̵� �� ����
        if (controlMoving && gem != null && controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePosition(targetPosition, false));
        }
        // �ݺ��̵�, �̵� �� ����
        if (autoMoving && !isMoving)
        {     
            isMoving = true;
            StartCoroutine(RepeatMove());
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
            yield return MovePosition(targetPosition, false);
            yield return MovePosition(currentPosition, false);
        }
    }

    IEnumerator MovePosition(Vector3 targetPos, bool resetMovingFlag) // �̵�
    {
        Vector3 startPosition = transform.localPosition; // ������ġ ����
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration) // õõ�� �̵�
        {
            transform.localPosition = Vector3.MoveTowards(startPosition, targetPos, (targetPos - startPosition).magnitude * (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos; // ��ǥ ����

        if (resetMovingFlag) // �̵� ����
        {
            isMoving = false;
        }
    }
}
