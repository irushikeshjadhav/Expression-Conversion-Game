using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadePanel;

    private void Start()
    {
        fadePanel.CrossFadeAlpha(0f, fadeDuration, false);
    }
}
