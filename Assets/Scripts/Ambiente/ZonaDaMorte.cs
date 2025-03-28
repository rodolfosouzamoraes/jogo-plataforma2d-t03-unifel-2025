using UnityEngine;

public class ZonaDaMorte : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            //Finaliza jogo
            CanvasGameMng.Instance.FimDeJogo();
        }
    }
}
