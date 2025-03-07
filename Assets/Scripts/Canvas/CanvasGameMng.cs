using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGameMng : MonoBehaviour
{
    #region Singleton
    //Criar uma variável para instanciar o objeto
    public static CanvasGameMng Instance;

    void Awake()
    {
        //Criar a instancia estática do objeto
        //Verificar se já existe uma instancia na cena
        if(Instance == null){
            Instance = this;
            return;
        }
        //Destruir o gameobject caso já exista uma instancia dessa classe na cena
        Destroy(gameObject);
    }
    #endregion
    public bool fimDeJogo; //Diz se o jogo acabou  
    private PlayerControlador playerControlador; //Código que controlam os demais códigos do player   

    [Header("Vida do Player")]
    public Image imgVida; //Imagem da vida
    public Sprite[] sptsVida; //Os sprites que vão ser trocados na imagem da vida
    private int totalVidas; //Quantidade de vidas que o player tem
    
    [Header("Controle do Painel Topo")]
    public TextMeshProUGUI txtTotalItensColetados; //Texto que exibe o total de itens coletados
    private int totalItensColetados; //Armazenar o total de itens coletados no level
    public TextMeshProUGUI txtTempoDeJogo; //Texto que exibe o tempo do jogo
    public float tempoDeJogo; //Variável que vai armazenar o tempo do jogo
    public GameObject pnlTopo; //Gameobject do painel do topo da tela

    [Header("Controle do Painel Lv Completado")]
    public GameObject pnlLevelCompletado; //Gameobject do painel do level completado
    public TextMeshProUGUI txtTotalItensColetadosFinal; //Texto com o total coletado no final do level
    public Image imgIconeMedalha; //Imagem da medalha
    public Sprite[] sptsMedalhas; //Sprites com as medalhas, 1 - Bronze, 2 - Prata, 3 - Ouro
    private float qtdDeItensColetaveisNoLevel; //Total de itens coletaveis que existe no level ao iniciar o jogo
    private int idMedalha; //Identificador da medalha que o jogador conseguiu no level
    // Start is called before the first frame update
    void Start()
    {
        //Adicionar o total de vidas que o player tem ao iniciar o jogo
        totalVidas = sptsVida.Length -1;

        //Pegar a referencia do controle do player
        playerControlador = FindFirstObjectByType<PlayerControlador>();

        //Zerar o total de itens coletados
        totalItensColetados = 0;

        //Atualizar o texto com o total de itens coletados
        txtTotalItensColetados.text = $"x{totalItensColetados}";

        //Atualizar o tempo no texto
        txtTempoDeJogo.text = $"{tempoDeJogo}";

        //Atualizar o texto com o total de itens coletados no level
        txtTotalItensColetadosFinal.text = $"x{totalItensColetados}";

        //Exibir o painel de topo e ocultar o painel de level completado
        pnlTopo.SetActive(true);
        pnlLevelCompletado.SetActive(false);

        //Obter a quantidade de itens coletáveis que existe no level
        qtdDeItensColetaveisNoLevel = FindObjectsByType<ItemColetavel>(FindObjectsSortMode.None).Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Contar o tempo do jogo
        ContarTempo();
    }

    /// <summary>
    /// Método para decrementar a vida do jogador na cena
    /// </summary>
    public void DecrementarVidaJogador(){
        //Decrementar a vida do jogador
        totalVidas--;

        //Verificar se o jogador tem vidas para continuar o jogo
        if(totalVidas < 1)
        {
            //Finaliza o jogo
            FimDeJogo();
        }
        else
        {
            //Atualizar a imagem da vida para o sprite correspondente
            imgVida.sprite = sptsVida[totalVidas];
        }
    }

    //Método para finalizar o jogo
    public void FimDeJogo(){
        //Dizer que acabou o jogo
        fimDeJogo = true;

        //Zerar as vidas do jogador
        totalVidas = 0;

        //Atualizar o sprite da imagem da vida
        imgVida.sprite = sptsVida[totalVidas];

        //Desabilitar as funções do jogador
        playerControlador.DanoPlayer.MatarJogador();

        //Contar o tempo para reiniciar a cena
        StartCoroutine(ReiniciarLevel());
    }

    /// <summary>
    /// Tempo para poder reiniciar o level
    /// </summary>
    /// <returns></returns>
    IEnumerator ReiniciarLevel(){
        //Aguardar 3 segundos para reiniciar o level
        yield return new WaitForSeconds(3f);

        //Reiniciar o level
        ReiniciarLevelAtual();
    }

    /// <summary>
    /// Reiniciar a cena atual do jogo
    /// </summary>
    public void ReiniciarLevelAtual(){
        //Reinicia a cena do jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Método para incrementar um item ao total de itens coletados
    /// </summary>
    public void IncrementarItemColetavel(){
        //Incrementar um item na variavel
        totalItensColetados++;

        //Atualizar o texto com o total de itens coletados
        txtTotalItensColetados.text = $"x{totalItensColetados}";
    }

    /// <summary>
    /// Método para contar o tempo do jogo
    /// </summary>
    private void ContarTempo(){
        //Verificar se o jogo acabou para poder não contar o tempo mais
        if(fimDeJogo == true) return;

        //Decrementar o tempo na variável
        tempoDeJogo -= Time.deltaTime;

        //Verificar se o tempo acabou
        if(tempoDeJogo <= 0){
            //Finaliza Jogo
            FimDeJogo();
        }
        else{
            //Atualizar o texto do tempo de jogo
            txtTempoDeJogo.text = $"{(int)tempoDeJogo}";
        }

    }

    /// <summary>
    /// Método para finalizar o level
    /// </summary>
    public void CompletouLevel(){
        //Dizer que o jogo acabou
        fimDeJogo = true;

        //Congelar o player
        playerControlador.MovimentarPlayer.CongelarPlayer();

        //Exibir a tela final do level
        StartCoroutine(ExibirTelaDoLevelCompletado());
    }

    /// <summary>
    /// Contar o tempo para poder exibir o painel do level completado e fazer a contagem das frutas
    /// </summary>
    /// <returns></returns>
    IEnumerator ExibirTelaDoLevelCompletado(){
        //Aguardar 3 segundos
        yield return new WaitForSeconds(3f);

        //Calcular a medalha do level
        CalcularMedalhaLevel();

        //Exibir a tela do level completado e ocultar a tela do topo
        pnlLevelCompletado.SetActive(true);
        pnlTopo.SetActive(false);

        //Fazer uma contagem dos itens coletados
        int contagem = 0;
        while(contagem < totalItensColetados){
            //Incrementar a contagem
            contagem++;

            //Exibir essa contagem no texto
            txtTotalItensColetadosFinal.text = $"x{contagem}";

            //Aguardar 0.1 segundo para reiniciar o loop e contar novamente
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Método para calcular a medalha obtida no level
    /// </summary>
    private void CalcularMedalhaLevel(){
        //Definir uma porcentagem da coleta dos itens coletaveis
        float porcentagem = (totalItensColetados/qtdDeItensColetaveisNoLevel) *100;

        //Definir qual medalha foi conquistada com base na porcentagem
        idMedalha = porcentagem < 50 ? 1 : porcentagem < 100 ? 2 : 3;

        //Atribuo a medalha na imagem do icone
        imgIconeMedalha.sprite = sptsMedalhas[idMedalha];
    }
}
