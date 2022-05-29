using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject Player, particle; // Обьекты: ГГ и Система частиц
    [SerializeField]
    private Transform PointSpawn;//Точка появления

    void Start()
    {
        StartCoroutine(Spawn());// Появляется ГГ через 1 секунду, вместе с ним и система частиц
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Player,PointSpawn.position,transform.rotation);
        Instantiate(particle,PointSpawn.position,new Quaternion(0,0,0,0));
    }
}
