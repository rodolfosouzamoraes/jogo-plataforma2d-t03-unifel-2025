using UnityEngine;

public class FlipCorpoPlayer : MonoBehaviour
{
    public SpriteRenderer spriteCorpo; //Variável para acessar os atributos e métodos públicos do Sprite Renderer

    /// <summary>
    /// Fazer o corpo virar para a direita
    /// </summary>
    public void OlharDireita()
    {
        spriteCorpo.flipX = false;
    }

    /// <summary>
    /// Fazer o corpo virar para esquerda
    /// </summary>
    public void OlharEsquerda()
    {
        spriteCorpo.flipX = true;
    }
}
