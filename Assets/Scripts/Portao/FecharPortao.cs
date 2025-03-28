using UnityEngine;

public class FecharPortao : MonoBehaviour
{
    public float velocidade; //Velocidade de rotacao
    public GameObject fechadura;
    public GameObject grade;
    private Quaternion rotacaoAlvo; //Definir a rotação para qual o portão deve ir para "abrir"
    private bool fechouPortao = false;
    // Start is called before the first frame update
    void Start()
    {
        //Definir a rotação alvo
        rotacaoAlvo = Quaternion.Euler(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        //verificar se deve abrir o portao
        if(fechouPortao == true){
            grade.transform.rotation = Quaternion.RotateTowards(
                grade.transform.rotation,
                rotacaoAlvo,
                velocidade * Time.deltaTime
            );
        }
    }

    private void AtivarFechadura(){
        fechadura.SetActive(true);

        AudioMng.Instance.PlayAudioPortao();
    }

    /// <summary>
    /// Mudar o layer o objeto para o layer do limite para o jogador identificar como parede
    /// </summary>
    private void MudarLayer(){
        transform.gameObject.layer = 6;
    }

    void OnTriggerExit2D(Collider2D colisao)
    {
        if(colisao.gameObject.tag == "Player" && fechouPortao == false){
            //diz que fechou o portao
            fechouPortao = true;

            //Mudar o boxCollider para corpo rigido
            GetComponent<BoxCollider2D>().isTrigger = false;
            
            //Ativar a fechadura depois de um tempo
            Invoke("AtivarFechadura",1f);
            
            //Mudar layer depois de 1f
            Invoke("MudarLayer",1f);
        }        
    }
}
