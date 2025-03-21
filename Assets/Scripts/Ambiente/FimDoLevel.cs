using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimDoLevel : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se foi o player que colidiu
        if(colisao.gameObject.tag == "Player"){
            //Ativar a animação do fim do level
            animator.SetBool("FimDoLevel", true);

            //Finalizar o level
            CanvasGameMng.Instance.CompletouLevel();
        }
    }
}
