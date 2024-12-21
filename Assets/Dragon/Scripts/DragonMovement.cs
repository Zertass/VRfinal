using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public Transform[] waypoints; // Точки маршрута
    public float speed = 5f; // Скорость движения
    public float rotationSpeed = 2f; // Скорость поворота
    public float minDistanceToTarget = 0.5f; // Расстояние, при котором точка считается достигнутой

    private int currentWaypointIndex = -1; // Индекс текущей точки
    private Vector3 targetPosition; // Позиция текущей цели
    private bool isAlive = true; // Статус дракона

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

        // Двигаемся к текущей цели
        MoveTowardsTarget();

        // Если достигли цели, выбираем следующую
        if (Vector3.Distance(transform.position, targetPosition) <= minDistanceToTarget)
        {
            ChooseNextWaypoint();
        }
    }

    private void MoveTowardsTarget()
    {
        // Плавное движение к цели
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Поворот к цели
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void ChooseNextWaypoint()
    {
        // Выбираем случайную точку, отличную от текущей
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
        // Здесь можно добавить логику для остановки или уничтожения дракона
        Debug.Log("Dragon has died.");
    }
}

