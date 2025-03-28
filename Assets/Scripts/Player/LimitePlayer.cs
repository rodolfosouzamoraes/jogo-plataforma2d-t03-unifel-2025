using UnityEngine;

public class LimitePlayer : MonoBehaviour
{
    public bool estaNoLimite; //Diz se o player chegou no limite

    void OnTriggerStay2D(Collider2D colisao)
    {
        //Verificar se o limite chegou nos objetos cujo o Layer é o Limite
        if(colisao.gameObject.layer == 6){
            estaNoLimite = true;
        }
    }

    void OnTriggerExit2D(Collider2D colisao)
    {
        if(colisao.gameObject.layer == 6){
            estaNoLimite = false;
        }
    }
}
