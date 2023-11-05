using UnityEngine;

public class FadeAnimationManager : MonoBehaviour
{
    private const string ANIMATION_FADE_OUT = "Fade Out";   
    private const string ANIMATION_FADE_IN = "Fade In";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.Play(ANIMATION_FADE_IN);
    }

    public void FadeOut()
    {
        animator.Play(ANIMATION_FADE_OUT);
    }
}
