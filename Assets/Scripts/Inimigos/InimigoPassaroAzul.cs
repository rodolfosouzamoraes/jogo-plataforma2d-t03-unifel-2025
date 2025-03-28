using UnityEngine;

public class InimigoPassaroAzul : MonoBehaviour
{
    public GameObject passaroAzul; //GameObject do pai
    public float velocidade; //Velocidade de movimentação do passaro
    public Vector3 posicaoFinal;// Posição final para onde o passaro deve ir
    private SpriteRenderer corpoPassaroAzul; //Variável para manipular o sprite do passaro
    private Vector3 posicaoInicial; //Posicial do passaro ao iniciar o jogo
    private Vector3 posicaoAlvo; //Posição com a direção para onde o passaro deve ir
    private bool estaMorto; //Diz se o passaro morreu
    private Animator animator; //Controla a animação do passáro
    // Start is called before the first frame update
    void Start()
    {
        //Definir a posição inicial do passaro
        posicaoInicial = passaroAzul.transform.position;

        //Difinir o alvo para onde o passaro deve ir de inicio
        posicaoAlvo = posicaoFinal;

        //Configurar o animator
        animator = GetComponent<Animator>();

        //Configurar o SpriteRenderer
        corpoPassaroAzul = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentar o passaro
        MovimentarPassaro();

        //Calcular a distancia entre o passaro e o alvo
        CalcularDistanciaAlvo();
    }

    /// <summary>
    /// Método para movimentar o passáro para uma posição
    /// </summary>
    private void MovimentarPassaro(){
        //Movimentar o passáro para a posição alvo
        passaroAzul.transform.position = Vector3.MoveTowards(
            passaroAzul.transform.position,
            posicaoAlvo,
            velocidade * Time.deltaTime
        );
    }

    /// <summary>
    /// Calcular a distancia entre o passáro e o alvo para onde ele deve ir
    /// </summary>
    private void CalcularDistanciaAlvo(){
        //Verificar a distancia do passaro em relação ao alvo
        if(Vector3.Distance(passaroAzul.transform.position,posicaoAlvo) < 0.001f){
            //Verificar se o flip do sprite para saber a nova direção do passaro
            if(corpoPassaroAzul.flipX == false){
                //Alterar a posição alvo para a inicial
                posicaoAlvo = posicaoInicial;
            }
            else{
                //Alterar a posição alvo para a final
                posicaoAlvo = posicaoFinal;
            }
            //Inverter o flip para olhar na direção para onde deve ir
            corpoPassaroAzul.flipX = !corpoPassaroAzul.flipX;
        }
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        //Verificar se o player colidiu com o passaro e se ele está vivo
        if(colisao.gameObject.tag == "Player" && estaMorto == false){
            //Dizer que o passaro morreu
            estaMorto = true;

            //Arremessar o player
            colisao.GetComponent<PlayerControlador>().MovimentarPlayer.ArremessarPlayer();

            //Ativo a animação de morte
            animator.SetTrigger("Morte");

            //Ativa audio de dano
            AudioMng.Instance.PlayAudioDanoInimigo();
        }
    }

    /// <summary>
    /// Método acionado pela animação de morte para poder destruir o objeto
    /// </summary>
    public void DestruirPassaro(){
        Destroy(passaroAzul);
    }
}
