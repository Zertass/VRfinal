using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Ссылка на игрока

    void LateUpdate()
    {
        if (player != null)
        {
            // Поворачиваем объект так, чтобы он смотрел на игрока
            transform.LookAt(player);

            // Инвертируем поворот по оси Y, чтобы текст не был зеркальным
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        }
    }
}
