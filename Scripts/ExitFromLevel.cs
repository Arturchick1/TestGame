using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromLevel : MonoBehaviour//для перехода на след уровень в нужном месте
{
    [SerializeField]
    private GameObject ScreenNextLevel;//экран на переход уровня

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            ScreenNextLevel.SetActive(true);
            collider.gameObject.GetComponent<PlayerMovement>().enabled = false;
            collider.gameObject.GetComponent<Animator>().SetBool("isRun",false);
        }
    }
}
