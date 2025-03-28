using TMPro;
using UnityEngine;
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

    public Slider sldVFX;
    public Slider sldMusica;
    private Volume volume;

    // Start is called before the first frame update
    void Start()
    {
        //Configurar para exibir o painel menu ao iniciar o jogo
        ExibirPainel(0);

        //Configura o painel do nível
        ConfigurarPainelNiveis();

        //Configurar o painel de configurações
        ConfigurarPainelConfiguracoes();

        //Tocar o audio do menu
        AudioMng.Instance.PlayAudioMenu();

        //Ocultar o painel de loading
        CanvasLoadingMng.Instance.OcultarPainelLoading();
    }

    private void ConfigurarPainelConfiguracoes(){
        //obter os volumes salvos na memória
        volume = DBMng.ObterVolume();

        //Atualizar os valores nos sliders
        sldVFX.value = volume.vfx;
        sldMusica.value = volume.musica;

        //Atualizar os audios nos audios sources
        AudioMng.Instance.MudarVolume(volume);
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

        AudioMng.Instance.PlayAudioClick();

        //Exibir a tela de carregando
        CanvasLoadingMng.Instance.ExibirPainelLoading();
    }

    /// <summary>
    /// Método para iniciar os demais leveis
    /// </summary>
    public void IniciarLevel(int idLevel){
        //Verificar se o cadeado está desabilitado
        if(cadeadosDosLevels[idLevel].activeSelf == false){
            //Iniciar o level
            SceneManager.LoadScene(idLevel);

            AudioMng.Instance.PlayAudioClick();

            //Exibir a tela de carregando
            CanvasLoadingMng.Instance.ExibirPainelLoading();
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

    /// <summary>
    /// Método para atualizar os volumes
    /// </summary>
    public void AtualizarVolumes(){
        volume = DBMng.ObterVolume();
        AudioMng.Instance.MudarVolume(volume);
    }

    /// <summary>
    /// Método utilizado no slider do volume vfx
    /// </summary>
    public void MudarVolumeVFX(){
        //Salvar o novo volume
        DBMng.SalvarVolume(sldVFX.value, volume.musica);
        //Atualizar os volumes no jogo
        AtualizarVolumes();
    }

    /// <summary>
    /// Método utilizado no slider do volume musica
    /// </summary>
    public void MudarVolumeMusica(){
        //Salvar o novo volume
        DBMng.SalvarVolume(volume.vfx, sldMusica.value);
        //Atualizar os volumes no jogo
        AtualizarVolumes();
    }

    public void PlayAudioClick(){
        AudioMng.Instance.PlayAudioClick();
    }
}
