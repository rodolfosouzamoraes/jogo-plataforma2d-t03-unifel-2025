using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenuMng : MonoBehaviour
{
    public TextMeshProUGUI[] txtItensColetadosPorLevels;
    public GameObject[] cadeadosDosLevels;
    public GameObject[] qtdsItensDoLevel; //GameObject que possui o texto e o icone da maçã
    public GameObject[] medalhasDosLevels;
    public Sprite[] sptsMedalhasDosLevels;
    public GameObject[] paineis;

    // Start is called before the first frame update
    void Start()
    {
        //Configurar para exibir o painel menu ao iniciar o jogo
        ExibirPainel(0);

        //Configura o painel do nível
        ConfigurarPainelNiveis();
    }

    private void ConfigurarPainelNiveis(){
        //Exibir a quantidade de itens de cada level
        for(int i = 1; i < txtItensColetadosPorLevels.Length; i++){
            //Buscar os dados dos itens coletados de cada nivel
            txtItensColetadosPorLevels[i].text = "x" + DBMng.BuscarQtdItensColetaveisLevel(i).ToString();
        }

        //Habilitar ou desabilitar os levels
        for(int i = 2; i < cadeadosDosLevels.Length; i++){
            //Verificar se o level atual está habilitado
            bool estaHabilitado = DBMng.BuscarLevelHabilitado(i) == 1 ? true : false;

            //Exibir ou não o cadeado
            cadeadosDosLevels[i].SetActive(!estaHabilitado);

            //Exibir ou não os itens do level
            qtdsItensDoLevel[i].SetActive(estaHabilitado);
        }

        //Definir as medalhas de cada level
        for(int i = 1; i < medalhasDosLevels.Length ; i++){
            //Verificar o id da medalha do level
            int medalhaDoLevel = DBMng.BuscarMedalhaLevel(i);

            //Verificar  há alguma medalha salva no level
            if(medalhaDoLevel == 0){
                medalhasDosLevels[i].SetActive(false);
            }
            else{
                //Altera a imagem da medalha para a medalha correspondente do level
                medalhasDosLevels[i].GetComponent<Image>().sprite = sptsMedalhasDosLevels[medalhaDoLevel];
            }
        }
    }

    /// <summary>
    /// Método para carregar o level 1
    /// </summary>
    public void IniciarLevel1(){
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Método para iniciar os demais leveis
    /// </summary>
    public void IniciarLevel(int idLevel){
        //Verificar se o cadeado está desabilitado
        if(cadeadosDosLevels[idLevel].activeSelf == false){
            //Iniciar o level
            SceneManager.LoadScene(idLevel);
        }
    }

    /// <summary>
    /// Método para exibir o painel solicitado
    /// </summary>
    public void ExibirPainel(int id){
        //Ocultar todos os paineis por padrão
        foreach(var painel in paineis){
            painel.SetActive(false);
        }

        //Exibir o painel solicitado
        paineis[id].SetActive(true);
    }

    /// <summary>
    /// Método para fechar o jogo
    /// </summary>
    public void FecharJogo(){
        Application.Quit();
    }
}
