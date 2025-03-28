using UnityEngine;

public class ColetarChave : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private bool coletouChave = false;

    public bool ColetouChave {
        get{return coletouChave;}
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if(colisao.gameObject.tag == "Player" && coletouChave == false){
            //Dizer que coletou a chave
            coletouChave = true;

            AudioMng.Instance.PlayAudioChave();

            //Ocultar a textura da chave
            spriteRenderer.enabled = false;
        }
    }
}
