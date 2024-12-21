using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // ������ �� ������

    void LateUpdate()
    {
        if (player != null)
        {
            // ������������ ������ ���, ����� �� ������� �� ������
            transform.LookAt(player);

            // ����������� ������� �� ��� Y, ����� ����� �� ��� ����������
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        }
    }
}
