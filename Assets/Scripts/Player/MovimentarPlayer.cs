using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPlayer : MonoBehaviour
{
    public FlipCorpoPlayer flipCorpo; //Variavel para poder fazer o flip da imagem do player
    public AnimacaoPlayer animacaoPlayer; //Variavel com os códigos da animação do player
    public LimitePlayer limiteDireita; //Variavel para obter a informação do limite da direita do player
    public LimitePlayer limiteEsquerda;
    public LimitePlayer limiteCabeca;
    public LimitePlayer limitePe;
    public Rigidbody2D rigidbody2d; //Variavel para acessar as propriedades fisicas do player

    public float velocidade; //Velocidade de movimentação do player

    public float forcaPuloY; //Força do pulo no eixo Y
    private float forcaPuloX; //Força do pulo no eixo X
    private bool estaPulando; //Diz se o player está em modo de pulo
    private bool puloDuplo; //Permite o player efetuar um pulo duplo
    private bool pularDaParede; //Permitir pular da parede
    private Coroutine coroutinePulo; //Contador de tempo para poder limitar o pulo do player

    // Start is called before the first frame update
    void Start()
    {
        //Habilitar o pulo da parede ao iniciar o game
        pularDaParede = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se o jogo acabou
        if(CanvasGameMng.Instance.fimDeJogo == true) return;

        Movimentar();
        Pular();
        PularDaParede();
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

        //Verificar se está no chão para poder ativar as animações de movimentação
        if(limitePe.estaNoLimite == true){

            //Verificar se o player está se movendo
            if(eixoX != 0){
                //Ativa animação de correndo do player
                animacaoPlayer.PlayCorrendo();
            }
            else{
                //Ativa animação de parado
                animacaoPlayer.PlayParado();
            }
        }
        else{
            //Ativa a animação de queda
            animacaoPlayer.PlayCaindo();
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
                //Ativa a animação do pulo
                animacaoPlayer.PlayPulando();

                //Habilitar o está pulando
                estaPulando = true;

                //Habilita o pulo duplo
                puloDuplo = true;

                //Ativar o tempo do pulo
                AtivarTempoPulo();
            }
            else{
                //Verificar se pode fazer o pulo duplo
                if(puloDuplo == true){
                    //Ativa animação do pulo duplo
                    animacaoPlayer.PlayPuloDuplo();

                    //Habilita novamente o pulo
                    estaPulando = true;

                    //Desabilita o pulo duplo
                    puloDuplo = false;

                    //Ativo um novo tempo de pulo
                    AtivarTempoPulo();
                }
            }
        }

        //Efetuar o pulo
        EfetuarPulo();
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

        //Zerar a força do pulo no eixo X
        forcaPuloX = 0;
    }

    /// <summary>
    /// Método para poder fazer o jogador simular o pulo
    /// </summary>
    private void EfetuarPulo(){

        //Verificar se o player pode subir
        if(estaPulando == true){

            //Verificar se a cabeça do player está livre para pular
            if(limiteCabeca.estaNoLimite == false){

                //Zerar as forças nos eixos do rigidbody2D
                ResetarFisicaDeMovimentacao();

                //Alterar a propriedade do rigidbody para fazer o player subir
                rigidbody2d.gravityScale = 0;

                //Direcionar o pulo do player
                Vector3 direcaoPulo = new Vector3(forcaPuloX,forcaPuloY,0);

                //Mover o player para cima, simbolizando o pulo
                transform.position += direcaoPulo * velocidade * Time.deltaTime;
            }
        }
        else{

            //Fazer o player voltar a cair
            rigidbody2d.gravityScale = 4;
        }
    }

    /// <summary>
    /// Faz o jogador pular através da parede
    /// </summary>
    private void PularDaParede(){

        //Verificar se está no chão para poder habilitar novmaente o pulo da parede
        if(limitePe.estaNoLimite == true){
            pularDaParede = true;
        }

        //Verificar se o pular da parede está permitido
        if(pularDaParede == false) {return;}

        //Verificar se o player não está no chão e a cabeça não está no limite
        //Verificar tbm se o player está em alguma das extremidades
        if(limitePe.estaNoLimite == false && limiteCabeca.estaNoLimite == false &&
        (limiteEsquerda.estaNoLimite == true || limiteDireita.estaNoLimite == true)){   

            //Ativa a animação de deslizar da parede
            animacaoPlayer.PlayDeslizarParede();

            //Ajuste na visão do player
            if(limiteEsquerda.estaNoLimite == true) flipCorpo.OlharEsquerda();
            else flipCorpo.OlharDireita();

            //Obter a entrada do usuário para poder efetuar o pulo pela parede
            if(Input.GetButtonDown("Jump")){

                //Aplicar uma força em X na direção contraria a parede que ele está encostado
                if(limiteDireita.estaNoLimite == true){
                    forcaPuloX = forcaPuloY * -1;
                }
                else if(limiteEsquerda.estaNoLimite == true){
                    forcaPuloX = forcaPuloY;
                }
                else{
                    forcaPuloX = 0;
                }

                //Ativa animação de pulo
                animacaoPlayer.PlayPulando();

                //Habilitar o pulo
                estaPulando = true;

                //Habilitar o pulo duplo
                puloDuplo = true;

                //Desabilitar o pulo da parede
                pularDaParede = false;

                //Ativar um novo tempo do pulo
                AtivarTempoPulo();
            }
        }
    }    

    /// <summary>
    /// Arremessa o player para uma direção aleatória
    /// </summary>
    public void ArremessarPlayer(){

        //Sortear um número entre 0 e 1 para poder definir qual direção a ser arremessado
        int valorSorteado = new System.Random().Next(0,2);

        //Definir a direção em X a ser arremessado
        int direcaoX = valorSorteado == 0? -1000 : 1000;

        //Aplicar a força no player
        rigidbody2d.AddForce(new Vector2(direcaoX,1000));
    }

    /// <summary>
    /// Reseta as forças do rigidbody do
    /// </summary>
    public void ResetarFisicaDeMovimentacao(){
        rigidbody2d.velocity = Vector3.zero;
    }

    /// <summary>
    /// Método para remover a gravidade
    /// </summary>
    public void RemoverGravidade(){
        //Colocar o rigidbody do player como Static
        rigidbody2d.bodyType = RigidbodyType2D.Static;
    }

    /// <summary>
    /// Método para tirar as funções de movimentação do player
    /// </summary>
    public void CongelarPlayer(){
        //Resetar a física de movimentação
        ResetarFisicaDeMovimentacao();

        //Remover a gravidade
        RemoverGravidade();

        //Ativar a animação de parado do player
        animacaoPlayer.PlayParado();
    }
}
