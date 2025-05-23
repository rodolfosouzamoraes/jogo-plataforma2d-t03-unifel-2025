using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public Animator animator;

    private bool coletouItem; //Variável para saber se o item foi coletado

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se foi o player que colidiu e se já houve uma colisão com ele anteriormente
        if(coletouItem == false && colisao.gameObject.tag == "Player"){
            //Diz que coletou o item
            coletouItem = true;

            //Ativar a animação de coleta do item
            animator.SetTrigger("Coletar");

            //Ativa audio de coleta da fruta
            AudioMng.Instance.PlayAudioFruta();

            //Incrementar a coleta do item
            CanvasGameMng.Instance.IncrementarItemColetavel();
        }
    }

    /// <summary>
    /// Método para poder destruir o item após o fim da animação de coleta
    /// </summary>
    public void DestruirItem(){
        Destroy(gameObject);
    }
}
