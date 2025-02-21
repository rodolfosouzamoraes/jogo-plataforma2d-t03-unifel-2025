using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPlayer : MonoBehaviour
{
    public MovimentarPlayer movimentarPlayer; //Variavel para acessar os métodos e atributos de movimentação do player

    /// <summary>
    /// Efetua um dano ao player
    /// </summary>
    public void EfetuarDano(){
        //Resetar a fisicao do jogador
        movimentarPlayer.ResetarFisicaDeMovimentacao();

        //Arremessar o jogador
        movimentarPlayer.ArremessarPlayer();
    }
}
