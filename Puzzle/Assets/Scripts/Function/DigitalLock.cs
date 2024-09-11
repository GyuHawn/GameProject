using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalLock : MonoBehaviour
{
    public GameObject card; // ������ ī��Ű
    public Transform activatedCard; 

    public bool activate; // Ȱ��ȭ ����

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == card)
        {
            activate = true;
            activatedLock();
        }
    }

    void activatedLock()
    {
        Rigidbody rb = card.GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        card.tag = "Untagged";

        card.transform.position = activatedCard.position;
        card.transform.rotation = Quaternion.Euler(0, 0, 76.5f);
    }
}
