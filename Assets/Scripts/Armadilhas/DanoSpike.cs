using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoSpike : MonoBehaviour
{
    //private bool houveColisao; //Verificar se ouve uma colisão com o spike

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se colidiu com o player
        if(colisao.gameObject.tag == "Player"){
            //Efetuar um dano ao jogador
            colisao.gameObject.GetComponent<DanoPlayer>().EfetuarDano();
        }
    }
}
