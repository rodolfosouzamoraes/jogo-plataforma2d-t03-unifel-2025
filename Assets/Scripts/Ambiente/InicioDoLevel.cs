using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioDoLevel : MonoBehaviour
{
    private GameObject player; //Gameobject do player
    public GameObject posicaoInicialPlayer; //Gameobject da posição inicial do player
    // Start is called before the first frame update
    void Start()
    {
        //Achar o player e amarzenar na variável
        player = FindFirstObjectByType<PlayerControlador>().gameObject;

        //Posicionar o player na posição inicial
        player.transform.position = posicaoInicialPlayer.transform.position;
    }
}
