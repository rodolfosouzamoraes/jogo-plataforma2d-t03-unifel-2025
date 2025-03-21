using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortao : MonoBehaviour
{
    public ColetarChave chave; //Chave que abre o portao
    public float velocidade; //Velocidade de rotacao
    public GameObject fechadura;
    private Quaternion rotacaoAlvo; //Definir a rotação para qual o portão deve ir para "abrir"
    private bool abriuPortao = false;

    // Start is called before the first frame update
    void Start()
    {
        //Definir a rotação alvo
        rotacaoAlvo = Quaternion.Euler(new Vector3(0,90,0));
    }

    // Update is called once per frame
    void Update()
    {
        //Abrir o portao caso seja permitido
        if(abriuPortao == true){
            //Rotaciona o objeto para o angulo alvo
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                rotacaoAlvo, 
                velocidade * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        //Verificar se o player colidiu e se tem a chave
        if(colisao.gameObject.tag == "Player" && chave.ColetouChave == true && abriuPortao == false){
            //Definir que o portao deve ser aberto
            abriuPortao = true;

            //Desativar a fechadura
            fechadura.SetActive(false);

            //Desativar a colisão do portao
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            Destroy(boxCollider2D);
        }
    }
}
