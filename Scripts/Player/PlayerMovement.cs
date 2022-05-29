using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform _mousePosition;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject Point;//Сфера которая будет указателем
    private GameObject PointMouse;// Сам указатель
    private Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();   
        PointMouse = Instantiate(Point,new Vector3(0,-1,0),transform.rotation); // создаем указатель под картой чтобы не было видно, позднее будем менять только положение указателя
    }

    void Update()
    {
        navMeshAgent.speed = _speed;

        if(Input.GetMouseButtonDown(0))//по нажатию начинается движение
        MovePlayer();

        if(Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.5f) // если расстояние меньше 0.5 то анимация выключается
        _animator.SetBool("isRun",false);
    }

    void MovePlayer()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);//луч считывает положение курсора
        RaycastHit hit;
        

        if(Physics.Raycast(r, out hit))
        {
            if(hit.collider.name == "Floor")//если луч попал в "Пол" то начинается анимация, ставится цель куда надо идти и для наглядности ставится указатель
            {
                _animator.SetBool("isRun",true);
                navMeshAgent.SetDestination(hit.point);
                PointMouse.transform.position = hit.point;
            }

        }
    }
}
