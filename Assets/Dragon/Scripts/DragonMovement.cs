using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public Transform[] waypoints; // ����� ��������
    public float speed = 5f; // �������� ��������
    public float rotationSpeed = 2f; // �������� ��������
    public float minDistanceToTarget = 0.5f; // ����������, ��� ������� ����� ��������� �����������

    private int currentWaypointIndex = -1; // ������ ������� �����
    private Vector3 targetPosition; // ������� ������� ����
    private bool isAlive = true; // ������ �������

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not set!");
            return;
        }

        ChooseNextWaypoint();
    }

    void Update()
    {
        if (!isAlive) return;

        // ��������� � ������� ����
        MoveTowardsTarget();

        // ���� �������� ����, �������� ���������
        if (Vector3.Distance(transform.position, targetPosition) <= minDistanceToTarget)
        {
            ChooseNextWaypoint();
        }
    }

    private void MoveTowardsTarget()
    {
        // ������� �������� � ����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ������� � ����
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void ChooseNextWaypoint()
    {
        // �������� ��������� �����, �������� �� �������
        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, waypoints.Length);
        } while (nextIndex == currentWaypointIndex);

        currentWaypointIndex = nextIndex;
        targetPosition = waypoints[currentWaypointIndex].position;
    }

    public void Die()
    {
        isAlive = false;
        // ����� ����� �������� ������ ��� ��������� ��� ����������� �������
        Debug.Log("Dragon has died.");
    }
}

