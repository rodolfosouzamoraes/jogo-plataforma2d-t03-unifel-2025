using System.Collections;
using System.Collections.Generic;
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

    public Image imgVida; //Imagem da vida
    public Sprite[] sptsVida; //Os sprites que vão ser trocados na imagem da vida
    private int totalVidas; //Quantidade de vidas que o player tem

    public bool fimDeJogo; //Diz se o jogo acabou

    private PlayerControlador playerControlador; //Código que controlam os demais códigos do player


    // Start is called before the first frame update
    void Start()
    {
        //Adicionar o total de vidas que o player tem ao iniciar o jogo
        totalVidas = sptsVida.Length -1;

        //Pegar a referencia do controle do player
        playerControlador = FindFirstObjectByType<PlayerControlador>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
