using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmadilhaLamina : MonoBehaviour
{
    public Vector3[] destinos; //todos os destinos que a lamina deve ir
    public float velocidade;
    public float tempoProximoDestino; //Tempo para aguardar até ser liberado para ir ao próximo destino
    private int idProximoDestino; //Define para qual Vector3 deve ir a lamina
    private bool chegouAoDestino; //Verifica se chegou ao destino para poder mandar a lamina para o proximo destino
    private float tempoEsperaProximoDestino = 0; //tempo de espera para o próximo destino
    // Start is called before the first frame update
    void Start()
    {
        //Posicionar o objeto no destino inicial
        transform.position = destinos[0];
        
        //Dizer o proximo destino
        idProximoDestino = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarLamina();
    }

    /// <summary>
    /// Método para mover a lamina entre os destinos
    /// </summary>
    private void MovimentarLamina(){
        //Verificar se chegou ao destino
        if(chegouAoDestino == true){
            //Verificar se a lamina pode ir para o próximo destino
            if(Time.time > tempoEsperaProximoDestino){
                //Mandar a lamina para o proximo destino
                idProximoDestino++;

                //Verificar se chegou no ultimo destino
                if(idProximoDestino == destinos.Length){
                    //Mandar a lamina para o destino inicial
                    idProximoDestino = 0;
                }

                //Dizer que não chegou ao destino
                chegouAoDestino = false;
            }
        }
        //Movimentar a lamina para o destino
        else{
            //Calcular a velocidade de movimentação
            float velocidadeMovimento = velocidade * Time.deltaTime;

            //Mover a lamina até o destino atual
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinos[idProximoDestino],
                velocidadeMovimento
            );

            //Verificar se chegou ao destino atual
            if(Vector3.Distance(transform.position,destinos[idProximoDestino]) < 0.001f){
                //Alterar o tempo de espera
                tempoEsperaProximoDestino = Time.time + tempoProximoDestino;

                //Dizer que chegou ao destino
                chegouAoDestino = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colisor)
    {
        if(colisor.gameObject.tag == "Player"){
            //Matar o jogador instantaneamente
            CanvasGameMng.Instance.FimDeJogo();
        }      
    }
}
