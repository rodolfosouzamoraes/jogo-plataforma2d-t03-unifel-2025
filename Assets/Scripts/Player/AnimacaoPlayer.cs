using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    public Animator animator;

    /// <summary>
    /// Ativa a animação de Parado do player
    /// </summary>
    public void PlayParado(){
        animator.SetBool("Parado", true);
        animator.SetBool("Correndo", false);
        animator.SetBool("Pulando", false); 
        animator.SetBool("Caindo", false);  
        animator.SetBool("DeslizarParede", false); 
    }

    /// <summary>
    /// Ativa a animação de Parado do Player
    /// </summary>
    public void PlayCorrendo(){
        animator.SetBool("Correndo", true);
        animator.SetBool("Parado", false);  
        animator.SetBool("Pulando", false);    
        animator.SetBool("Caindo", false);  
        animator.SetBool("DeslizarParede", false);  
    }

    /// <summary>
    /// Ativa a animação de Pulando do player
    /// </summary>
    public void PlayPulando(){
        animator.SetBool("Pulando", true);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false); 
        animator.SetBool("Caindo", false); 
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativo a animação de caindo
    /// </summary>
    public void PlayCaindo(){
        animator.SetBool("Caindo", true);
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);  
        animator.SetBool("DeslizarParede", false);
    }

    /// <summary>
    /// Ativa a animação de deslizar na parede
    /// </summary>
    public void PlayDeslizarParede(){
        animator.SetBool("DeslizarParede", true);
        animator.SetBool("Caindo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);  
    }

    /// <summary>
    /// Ativa a animação de pulo duplo
    /// </summary>
    public void PlayPuloDuplo(){
        animator.SetTrigger("PuloDuplo");
        animator.SetBool("DeslizarParede", false);
        animator.SetBool("Caindo", false);
        animator.SetBool("Pulando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Parado", false);  
    }

    /// <summary>
    /// Ativa a animação de dano do personagem
    /// </summary>
    public void PlayDano(){
        animator.SetTrigger("Dano");
    }

    /// <summary>
    /// Ativa a animação de morte do jogador
    /// </summary>
    public void PlayMorte(){
        animator.SetBool("Fim", true);
        animator.SetTrigger("Morte");
    }

    public void PlayAudioMovimentacao(){
        AudioMng.Instance.PlayAudioCorrer();
    }
}
