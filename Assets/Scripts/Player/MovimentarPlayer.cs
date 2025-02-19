using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    public FlipCorpoPlayer flipCorpo; //Variavel para poder fazer o flip da imagem do player
    public LimitePlayer limiteDireita; //Variavel para obter a informação do limite da direita do player
    public LimitePlayer limiteEsquerda;
    public LimitePlayer limiteCabeca;
    public LimitePlayer limitePe;

    public float velocidade; //Velocidade de movimentação do player

    public float forcaPuloY; //Força do pulo no eixo Y
    public bool estaPulando; //Diz se o player está em modo de pulo
    private Coroutine coroutinePulo; //Contador de tempo para poder limitar o pulo do player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimentar();
    }

    /// <summary>
    /// Lógica de movimentação do personagem
    /// </summary>
    private void Movimentar()
    {
        //Obter uma entrada do usuário para fazer a movimentação
        float eixoX = Input.GetAxis("Horizontal");

        //Verificar se chegou nos limites da esquerda ou direita
        if(eixoX > 0 && limiteDireita.estaNoLimite == true) {eixoX = 0;}
        else if(eixoX < 0 && limiteEsquerda.estaNoLimite == true) {eixoX = 0;}

        //Olhar para o lado onde o jogador está movendo
        if(eixoX > 0){
            flipCorpo.OlharDireita();
        }
        else if(eixoX < 0){
            flipCorpo.OlharEsquerda();
        }

        //Definir a direção da movimentação
        Vector3 direcaoMovimento = new Vector3(eixoX,0,0);

        //Movimentar o personagem no sentido da direção
        transform.position += direcaoMovimento * velocidade * Time.deltaTime;
    }

    /// <summary>
    /// Efetua o pulo do player
    /// </summary>
    private void Pular()
    {
        //Obter a entrada do usuário para poder efetuar o pulo
        if(Input.GetButtonDown("Jump")){
            //Verificar se o pulo está habitado e se o player está no chão
            if(limitePe.estaNoLimite == true && estaPulando == false){
                //Habilitar o está pulando

                //Ativar o tempo do pulo
            }
        }
    }


    /// <summary>
    /// Vai ativar o contador de tempo do pulo
    /// </summary>
    private void AtivarTempoPulo(){
        //Verificar se já existe um contador de tempo para o pulo
        if(coroutinePulo != null){
            StopCoroutine(coroutinePulo); //Vai parar o contador de tempo do pulo anterior
        }
        //Iniciar um novo contador de tempo do pulo
        coroutinePulo = StartCoroutine(TempoPulo());
    }

    /// <summary>
    /// Vai contar o tempo que o jogador ficará "subindo"
    /// </summary>
    /// <returns></returns>
    private IEnumerator TempoPulo(){
        //Permitir 0.3 segundo o player subindo
        yield return new WaitForSeconds(0.3f);

        //Desabilitar a variavel que permite o player subir
        estaPulando = false;
    }
    
}
