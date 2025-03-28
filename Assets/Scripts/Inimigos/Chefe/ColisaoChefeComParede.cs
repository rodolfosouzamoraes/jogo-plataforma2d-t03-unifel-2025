using UnityEngine;

public class ColisaoChefeComParede : MonoBehaviour
{
    private MovimentarChefe movimentarChefe; //Acessar o movimentar para poder alterar o flip
    private bool houveColisao = false; //Definir se houve a colisão com a parede
    // Start is called before the first frame update
    void Start()
    {
        movimentarChefe = GetComponentInParent<MovimentarChefe>();
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.layer == 6 && houveColisao == false){
            //Definir que houve a colisao
            houveColisao = true;

            //Mudar o flip do chefe
            movimentarChefe.FlipCorpo();
        }
    }

    private void OnTriggerExit2D(Collider2D colisao)
    {
        if(colisao.gameObject.layer == 6){
            houveColisao = false;
        }
    }
}
