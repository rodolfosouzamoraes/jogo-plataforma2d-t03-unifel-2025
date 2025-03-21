using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarChefe : MonoBehaviour
{
    private ChefeControlador chefeControlador;
    private SpriteRenderer spriteRenderer;
    public float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        chefeControlador = GetComponent<ChefeControlador>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se pode se mover
        if(chefeControlador.EstaMovendo == false) return;
        
        //Movimentar o Chefe
        Movimentar();
    }

    private void Movimentar(){
        //Fazer a movimentação do chefe para a direção onde ele está olhando
        if(spriteRenderer.flipX == false){
            //Mover o chefe para a esquerda
            transform.Translate(Vector3.left * velocidade * Time.deltaTime);
        }
        else{
            //Mover o chefe para a direita
            transform.Translate(Vector3.right * velocidade * Time.deltaTime);
        }
    }

    /// <summary>
    /// Método para inverter o flip da imagem
    /// </summary>
    public void FlipCorpo(){
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
