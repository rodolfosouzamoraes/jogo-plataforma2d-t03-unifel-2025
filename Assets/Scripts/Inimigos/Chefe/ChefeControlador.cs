using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChefeControlador : MonoBehaviour
{
    private Animator animator;
    private List<BoxCollider2D> colisores; //Armazenar todos os colliders do chefe
    private int vidaChefe = 4;
    public GameObject itemFinal; //Para habilitar o item quando o chefe morrer
    private bool estaMovendo = false; //Diz se o chefe vai se mover ou não

    public bool EstaMovendo{
        get{return estaMovendo;}
    }
    // Start is called before the first frame update
    void Start()
    {
        //Ocultar o item final
        itemFinal.SetActive(false);

        //Referenciar o animator
        animator = GetComponent<Animator>();

        //Referenciar os colisores internos
        colisores = GetComponentsInChildren<BoxCollider2D>().ToList();

        //Adicionar o colisor do objeto pai
        colisores.Add(GetComponent<BoxCollider2D>());
    }

    public void DecrementarVidaChefe(){
        //Decrementar a vida do chefe
        vidaChefe--;

        //Verificar se a vida do chefe acabou
        if(vidaChefe == 0){
            //Parar de se mover
            estaMovendo = false;

            //Destruir os colisores
            foreach(var colisor in colisores){
                Destroy(colisor);
            }

            //Ativa a animação de morte do chefe
            animator.SetTrigger("Morte");
        }
        else{
            //Ativa animação de dano
            animator.SetTrigger("Dano");
        }
    }

    /// <summary>
    /// Ativa o item final após o fim da animação de morte
    /// </summary>
    public void AtivarItemFinal(){
        //Habilita o item final
        itemFinal.SetActive(true);

        //Destruo o chefe
        Destroy(gameObject);
    }

    /// <summary>
    /// Habilita a movimentação quando o player entrar numa area
    /// </summary>
    public void HabilitaMovimentacao(){
        //Diz que pode se mover
        estaMovendo = true;

        //Ativa a animação de corrida
        animator.SetBool("Correndo", true);
    }
}
