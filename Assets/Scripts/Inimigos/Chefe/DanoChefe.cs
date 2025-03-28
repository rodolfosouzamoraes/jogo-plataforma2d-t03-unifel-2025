using UnityEngine;

public class DanoChefe : MonoBehaviour
{
    private ChefeControlador chefeControlador;
    private bool houveColisao = false;
    // Start is called before the first frame update
    void Start()
    {
        chefeControlador = GetComponentInParent<ChefeControlador>();
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.tag == "Player" && houveColisao == false){
            //Dizer que houve a colisao
            houveColisao = true;

            //Arremessar o player
            colisao.GetComponent<PlayerControlador>().MovimentarPlayer.ArremessarPlayer();

            //Decrementar a vida do chefe
            chefeControlador.DecrementarVidaChefe();

            //Rehabilitar a colisão com o chefe
            Invoke("PermitirColisao",0.3f);
        }
    }

    /// <summary>
    /// Rehabilita a colisão com o player
    /// </summary>
    private void PermitirColisao(){
        houveColisao = false;
    }
}
