using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    //Declarar as váriaveis que vão armazenar os códigos do player
    private MovimentarPlayer movimentarPlayer;
    private AnimacaoPlayer animacaoPlayer;
    private DanoPlayer danoPlayer;

    //Declarar as propriedades de acesso as variáveis do player
    public MovimentarPlayer MovimentarPlayer{
        get{ return movimentarPlayer; }
    }
    public AnimacaoPlayer AnimacaoPlayer{
        get{ return animacaoPlayer; }
    }
    public DanoPlayer DanoPlayer{
        get { return danoPlayer; }
    }

    void Awake()
    {
        //Obter a referencia do movimentar player
        movimentarPlayer = GetComponent<MovimentarPlayer>();

        //Obter a referencia da animação do player
        animacaoPlayer = GetComponentInChildren<AnimacaoPlayer>();

        //Obter a referencia do dano ao player
        danoPlayer = GetComponent<DanoPlayer>();
    }
}
