using UnityEngine;

public class AudioMng : MonoBehaviour
{
    public static AudioMng Instance;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); //Não vai destruir o objeto após a transição de cena
            return;
        }
        Destroy(gameObject);
    }

    public AudioSource audioVFX; //Variavel para emitir os audios de VFX
    public AudioSource audioMusica; //Variavel para emitir os audios das Musicas
    public AudioClip clipGame; //Musica dos leveis
    public AudioClip clipMenu;
    public AudioClip clipClick;
    public AudioClip clipFruta;
    public AudioClip[] clipsDanos; //Audios de dano do player
    public AudioClip clipPular;
    public AudioClip clipCorrer;
    public AudioClip clipChave;
    public AudioClip clipChefe;
    public AudioClip clipSurgir;
    public AudioClip clipProjetil;
    public AudioClip clipDanoInimigo;
    public AudioClip clipItemFinal;
    public AudioClip clipMortePlayer;
    public AudioClip clipPortao;
    public AudioClip clipMorteChefe;

    /// <summary>
    /// Método para alterar o volume dos audios
    /// </summary>
    public void MudarVolume(Volume volume){
        audioVFX.volume = volume.vfx;
        audioMusica.volume = volume.musica;
    }

    /// <summary>
    /// Tocar a musica do menu
    /// </summary>
    public void PlayAudioMenu(){
        //Verificar se a musica atual é diferente da do menu
        if(audioMusica.clip != clipMenu){
            //Parar o audio
            audioMusica.Stop();

            //Trocar a "fita" 
            audioMusica.clip = clipMenu;

            //Tocar o audio
            audioMusica.Play();
        }
    }

    /// <summary>
    /// Tocar a musica do level
    /// </summary>
    public void PlayAudioLevel(){
        //Verificar se a musica atual é diferente da do level
        if(audioMusica.clip != clipGame){
            //Parar o audio
            audioMusica.Stop();

            //Trocar a "fita" 
            audioMusica.clip = clipGame;

            //Tocar o audio
            audioMusica.Play();
        }
    }

    public void PlayAudioClick(){
        audioVFX.PlayOneShot(clipClick);//Instancia um audio na cena
    }

    public void PlayAudioFruta(){
        audioVFX.PlayOneShot(clipFruta);
    }

    public void PlayAudioDanos(){
        //Sortear o audio de dano
        var audioSorteado = new System.Random().Next(0,clipsDanos.Length);

        //Emitir o audio sorteado
        audioVFX.PlayOneShot(clipsDanos[audioSorteado]);
    }

    public void PlayAudioPular(){
        audioVFX.PlayOneShot(clipPular);
    }

    public void PlayAudioCorrer(){
        audioVFX.PlayOneShot(clipCorrer);
    }

    public void PlayAudioChave(){
        audioVFX.PlayOneShot(clipChave);
    }

    public void PlayAudioChefe(){
        audioVFX.PlayOneShot(clipChefe);
    }

    public void PlayAudioSurgir(){
        audioVFX.PlayOneShot(clipSurgir);
    }

    public void PlayAudioProjetil(){
        audioVFX.PlayOneShot(clipProjetil);
    }

    public void PlayAudioDanoInimigo(){
        audioVFX.PlayOneShot(clipDanoInimigo);
    }

    public void PlayAudioItemFinal(){
        audioVFX.PlayOneShot(clipItemFinal);
    }

    public void PlayAudioMortePlayer(){
        audioVFX.PlayOneShot(clipMortePlayer);
    }

    public void PlayAudioPortao(){
        audioVFX.PlayOneShot(clipPortao);
    }

    public void PlayAudioMorteChefe(){
        audioVFX.PlayOneShot(clipMorteChefe);
    }
}
