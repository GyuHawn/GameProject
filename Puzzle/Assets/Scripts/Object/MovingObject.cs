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

    private Vector3 crruntPosition; // �ʱ� ��ġ
    private Vector3 targetPosition; // ��ǥ ��ġ
    public float moveDuration; // ���� �ð�
    private bool isMoving = false; // �̵� �� ����

    private void Start()
    {
        controlGem = gem.GetComponent<ControlGem>();

        crruntPosition = transform.localPosition; // �ʱ� ��ġ ����
        targetPosition = CalculateTargetPosition(); // ��ǥ ��ġ ����
    }

    void Update()
    {
        if (controlGem.onControl && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Moving());
        }
    }

    private Vector3 CalculateTargetPosition() // ��ǥ ��ġ ���
    {
        Vector3 target = crruntPosition;

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

    IEnumerator Moving() // �̵�
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 move = targetPosition - startPosition;
        Vector3 movePerSecond = move / moveDuration; // 3�� ���� �̵��ϵ��� �ӵ� ����

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, movePerSecond.magnitude * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition; // ��ǥ ����
        isMoving = false;
    }
}
