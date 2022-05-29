using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform[] Points;// Точки между которыми будет перемещаться враг
    [SerializeField]
    private float _speed;
    private Animator _animator;
    private Vector3 pastPosition; // Прошлая позиция, на какой точке был враг(Нужна для понимая в какую сторону идти)
    [SerializeField]
    private float timeWait = 1;//время задержки врага во время патрулирования

    void Start()
    {
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);// обнуляю высоту чтобы враг стоял ровно на полу

        navMeshAgent.speed = _speed;

        if(Points.Length == 2)// проверяю сколько точек для патрулирования
        {
            if(transform.position == Points[0].position)// если(текущая позиция = одной из точек патрулирования), то пора двигаться к следующей
            {
                StartCoroutine(Flip(1));
            }
            else if(transform.position == Points[1].position)
            {
                StartCoroutine(Flip(0));
            }
        }
        else // 3 точки патрулирования
        {
            if(transform.position == Points[0].position)
            {
                pastPosition = Points[0].position; // запоминаем позицию где только что был враг, и учитываем чтобы он не свернул в эту же сторону
                StartCoroutine(Flip(1));
            }
            else if(transform.position == Points[2].position)
            {
                pastPosition = Points[2].position;
                StartCoroutine(Flip(1));
            }

            if(transform.position == Points[1].position)
            {
                if(pastPosition == Points[0].position)// перед врагом 2 дороги если прошлая позиция это позиция[0], то идем к позиции[2] 
                StartCoroutine(Flip(2));
                else 
                StartCoroutine(Flip(0));
            }
        }

        if(Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.5f)// анимация выключается если между точкой и им самим < 0.5 
            _animator.SetBool("isRun",false);
        else 
            _animator.SetBool("isRun",true);

    }

    IEnumerator Flip(int i) // напрвляем врага в указанную точку
    {
        yield return new WaitForSeconds(timeWait);

        if(i == 0)
        {
            navMeshAgent.SetDestination(Points[0].position);
        }
        else if(i == 1)
        {
            navMeshAgent.SetDestination(Points[1].position);
        }
        else
        {
            navMeshAgent.SetDestination(Points[2].position);
        }
    }
}
