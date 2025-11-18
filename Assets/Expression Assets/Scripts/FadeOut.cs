using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Image fadePanel;

    private void Start()
    {
        fadePanel.canvasRenderer.SetAlpha(0f);
        fadePanel.CrossFadeAlpha(1f, fadeDuration, false);
    }
}
