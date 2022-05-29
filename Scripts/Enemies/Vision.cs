﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Vision : MonoBehaviour
{
    [SerializeField]
    private int rays = 8,distance = 33;
    [SerializeField]
    private float angle = 40;
    [SerializeField]
    private Vector3 offset; // позиция начала всех лучей
    [SerializeField]
    private Transform target; // Это ГГ

    void Update()
    {
        if(target == null)
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (RayToScan())// если один из лучей попадает по ГГ, то игра завершается, управление отключается и анимация приостанавливается
            {
                FindObjectOfType<DeathScreen>().EndGame();
                target.gameObject.GetComponent<PlayerMovement>().enabled = false;
                target.gameObject.GetComponent<Animator>().SetBool("isRun",false);

            }
    }

    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + offset;
        if (Physics.Raycast(pos, dir, out hit, distance))
        {
            if (hit.transform == target)// проверяем попал луч в ГГ или нет
            {
                result = true;// Попал
                Debug.DrawLine(pos, hit.point, Color.green);
            }
            else// иначе: не попал
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.blue);
        }
        return result;
    }

    bool RayToScan()
    {
       bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;//Задаем угол между лучами. Сначала он равен нулю

        for (int i = 0; i < rays; i++)
        {
            var x = Mathf.Sin(j);//синус и косинус от угла j
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;// Прибавляем к нашему градусу угол где должен быть след луч

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));// Определяем напрвление
            if (GetRaycast(dir)) a = true;// по заданному направлению пускаем луч и если луч попадает в ГГ то а = true

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));// определяем напрвление только с другой стороны 
                if (GetRaycast(dir)) b = true;//по заданному направлению пускаем луч и если луч попадает в ГГ то b = true
            }
        }

        if (a || b) result = true;// Если а или b = true то луч попал в ГГ
        return result;
    }
}