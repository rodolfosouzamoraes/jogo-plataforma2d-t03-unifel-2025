using UnityEngine;

public class ContatoPlataformaFlutuante : MonoBehaviour
{
    public Animator animator;
    public GameObject plataformaFlutuante; //Objeto pai da plataforma
    private bool saiuDaPlataforma; //Dizer se o player saiu ou n�o da plataforma

    private void OnTriggerExit2D(Collider2D colisao)
    {
        //Verificar se o player que saiu do contato com a plataforma e se ele n�o saiu antes
        if(colisao.gameObject.tag == "Player" && saiuDaPlataforma == false)
        {
            //Dizer que o player saiu da plataforma
            saiuDaPlataforma = true;

            //Adicionar uma f�sica para fazer a plataforma cair
            Rigidbody2D rigidbody2d = plataformaFlutuante.AddComponent<Rigidbody2D>();

            //Congelar os eixos para que a f�sica n�o o rotacione o mova sem querer
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

            //Ativar a anima��o de queda da plataforma
            animator.SetTrigger("Caindo");

            //Destruir o objeto
            Destroy(plataformaFlutuante, 0.25f);
        }
    }
}
