using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Threading.Tasks;

public class CameraFade : MonoBehaviour
{
    private System.Action<float> updateFade;
    public static CameraFade Instance { get; private set; }

    [SerializeField] Image fadeImage = default;
    [SerializeField] float fadeTime = 2f;

    [Space]
    [SerializeField] bool fadeOutOnStart = true;
    [SerializeField] Gradient fadeGradient = default;

    private FadeType fadeType = FadeType.None;
    private float currentFadeTime = 0;

    public FadeType CurrentFadeType
    {
        get => fadeType;
        set
        {
            switch (value)
            {
                case FadeType.None:
                    updateFade = null;
                    break;
                case FadeType.FadeIn:

                    if (fadeType.Equals(FadeType.None))
                        currentFadeTime = fadeTime;
                    updateFade = FadeIn;
                    break;
                case FadeType.FadeOut:

                    if (fadeType.Equals(FadeType.None))
                        currentFadeTime = 0;
                    updateFade = FadeOut;
                    break;
            }

            fadeType = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private async void Start()
    {
        if (fadeOutOnStart)
        {
            await Task.Delay(1000);
            CurrentFadeType = FadeType.FadeOut;
        }
    }


    private void Update()
    {
        if (CurrentFadeType.Equals(FadeType.None) || updateFade == null) return;
        updateFade.Invoke(Time.deltaTime);

    }

    /// <summary>
    ///     /// Przejúcie ze 0% alfy na 100% alfy (do przeüroczystoúci)
    /// </summary>
    private void FadeOut(float deltaTime)
    {
        currentFadeTime += deltaTime;

        if (currentFadeTime < fadeTime)
        {
            fadeImage.color = fadeGradient.Evaluate(currentFadeTime / fadeTime);
        }
        else
        {
            fadeImage.color = fadeGradient.Evaluate(1);
            CurrentFadeType = FadeType.None;
        }
    }

    /// <summary>
    /// Przejúcie ze 100% alfy na 0% alfy (do czerni)
    /// </summary>
    private void FadeIn(float deltaTime)
    {
        currentFadeTime -= deltaTime;

        if (currentFadeTime > 0)
        {
            fadeImage.color = fadeGradient.Evaluate(currentFadeTime / fadeTime);
        }
        else
        {
            fadeImage.color = fadeGradient.Evaluate(0);
            CurrentFadeType = FadeType.None;
        }
    }

    public enum FadeType
    {
        None, FadeIn, FadeOut
    }
}
