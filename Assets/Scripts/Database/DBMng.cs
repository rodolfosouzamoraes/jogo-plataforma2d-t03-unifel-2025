using UnityEngine;

public static class DBMng
{
    private const string LEVEL_DATA = "level-data-"; //variável com o endereço do armazenamento no registro
    private const string HABILITA_LEVEL = "habilita_level-"; //Endereço com a info do level habilitado para jogar
    private const string MEDALHA_LEVEL = "medalha-level-"; //Endereço com a info da medalha do level
    private const string VOLUME = "volume"; //Endereço com a info do volume dos audios do jogo

    /// <summary>
    /// Buscar na memória a quantidade de itens coletados no level informado
    /// </summary>
    public static int BuscarQtdItensColetaveisLevel(int idLevel){
        return PlayerPrefs.GetInt(LEVEL_DATA+idLevel);
    }

    /// <summary>
    /// Método para salvar os dados no registro do computador
    /// </summary>
    public static void SalvarDadosLevel(int idLevel, int totalItensColetados, int idMedalha){
        //buscar a quantidade de itens coletados no level
        int itensSalvosDoLevel = BuscarQtdItensColetaveisLevel(idLevel);
        
        //Verificar se o total atual é maior que o que já foi salvo
        if(totalItensColetados > itensSalvosDoLevel){
            //Salvar a quantidade de itens coletados
            PlayerPrefs.SetInt(LEVEL_DATA+idLevel,totalItensColetados);

            //Salvar o identificador da medalha
            PlayerPrefs.SetInt(MEDALHA_LEVEL+idLevel,idMedalha);
        }
    	
        //Habilitar o proximo level
        PlayerPrefs.SetInt(HABILITA_LEVEL + (idLevel +1),1);
    }

    /// <summary>
    /// Método para buscar a info se o level pode ser jogado ou não
    /// </summary>
    public static int BuscarLevelHabilitado(int idLevel){
        return PlayerPrefs.GetInt(HABILITA_LEVEL + idLevel);
    }

    /// <summary>
    /// Método para buscar a info da medalha do level
    /// </summary>
    public static int BuscarMedalhaLevel(int idLevel){
        return PlayerPrefs.GetInt(MEDALHA_LEVEL + idLevel);
    }

    //Salvar Volume

    //Obter Volume
}
