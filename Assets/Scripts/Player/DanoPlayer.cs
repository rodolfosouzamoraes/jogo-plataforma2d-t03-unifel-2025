using UnityEngine;

public class DanoPlayer : MonoBehaviour
{
    public MovimentarPlayer movimentarPlayer; //Variavel para acessar os métodos e atributos de movimentação do player

    public AnimacaoPlayer animacaoPlayer; //Variável para acessar os métodos e atributos da animação do player
    /// <summary>
    /// Efetua um dano ao player
    /// </summary>
    public void EfetuarDano(){
        //Verificar se acabou o jogo
        if(CanvasGameMng.Instance.fimDeJogo == true) return;

        //Ativar a animação de dano
        animacaoPlayer.PlayDano();

        //Ativar audio Dano
        AudioMng.Instance.PlayAudioDanos();

        //Resetar a fisicao do jogador
        movimentarPlayer.ResetarFisicaDeMovimentacao();

        //Arremessar o jogador
        movimentarPlayer.ArremessarPlayer();

        //Decrementar a vida do jogador
        CanvasGameMng.Instance.DecrementarVidaJogador();
    }

    /// <summary>
    /// Desabilita as fiscas do player e ativa a animação de morte
    /// </summary>
    public void MatarJogador(){
        //Zerar a fisica do player
        movimentarPlayer.ResetarFisicaDeMovimentacao();

        //Remover os efeitos de fisica do player
        movimentarPlayer.RemoverGravidade();

        //Ativar animação de morte
        animacaoPlayer.PlayMorte();

        //Ativar audio de morte
        AudioMng.Instance.PlayAudioMortePlayer();
    }
}
