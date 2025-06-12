using System.Collections;
using UnityEngine;

public class ShortEffectView
{
    private Renderer[] _renderers;
    private MonoBehaviour _monoBehaviour;

    public ShortEffectView(Renderer[] renderers, MonoBehaviour monoBehaviour)
    {
        _renderers = renderers;
        _monoBehaviour = monoBehaviour;
    }

    public void PlayIncreaseEffect(string effectKey, float duration) => _monoBehaviour.StartCoroutine(EffectIncreaseProcess(effectKey, duration));

    public void PlayDecreaseEffect(string effectKey, float duration) => _monoBehaviour.StartCoroutine(EffectDecreaseProcess(effectKey, duration));

    public void PlayIncreaseDecreaseEffect(string effectKey, float duration)
    {
        _monoBehaviour.StartCoroutine(EffectIncreaseProcess(effectKey, duration / 2));
        _monoBehaviour.StartCoroutine(EffectDecreaseProcess(effectKey, duration / 2));
    }

    private IEnumerator EffectIncreaseProcess(string effectKey, float endValue)
    {
        float timer = 0;

        while (timer <= endValue)
        {
            SetEffect(effectKey, timer / endValue);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    private IEnumerator EffectDecreaseProcess(string effectKey, float startValue)
    {
        float timer = startValue;

        while (timer >= 0)
        {
            SetEffect(effectKey, timer / startValue);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    private void SetEffect(string effectKey, float value)
    {
        foreach (Renderer renderer in _renderers)
            renderer.material.SetFloat(effectKey, value);
    }
}
