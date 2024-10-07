using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Image gaugeBar;
    public int maxGauge;
    public int currnetGauge;

    private bool isDecreasingGauge = false; // �ڷ�ƾ�� ���� ������ Ȯ���ϴ� �÷���

    void Start()
    {
        maxGauge = 100;
        currnetGauge = maxGauge;
    }

    void Update()
    {
        GaugeUpdate(); // �������� ������Ʈ

        if (currnetGauge > 0 && !isDecreasingGauge)
        {
            StartCoroutine(DecreaseGauge()); // �ʴ� ������ ����
        }
        else if (currnetGauge <= 0) // ������ 0 ���� �� ü�� ���� �� ������ �ҷ� ȸ��
        {
            playerMovement.currentHealth -= 5;
            currnetGauge += 5;
        }
    }

    IEnumerator DecreaseGauge()
    {
        isDecreasingGauge = true;
        yield return new WaitForSeconds(1f);

        currnetGauge--;
        isDecreasingGauge = false;
    }

    void GaugeUpdate()  // �������� ������Ʈ
    {
        float healthPercentage = (float)currnetGauge / maxGauge;
        gaugeBar.fillAmount = healthPercentage;
    }
}
