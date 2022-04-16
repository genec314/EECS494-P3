using System.Collections;
using UnityEngine;

public class CircleWipeController : MonoBehaviour
{
    private const float RADIUS = 1.1f;

    public Shader shader;

    private Material material;

    [Range(0, RADIUS)]
    public float _radius = 0f;

    public float _horizontal = 16;

    public float _vertical = 9;

    public float _duration = 0.25f;

    public Color _fadeColour = Color.black;

    public Texture _fadeTexture;

    public Vector2 _offset;

    void Start()
    {
        material = new Material(shader);
        _radius = RADIUS;
        UpdateShader();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void FadeInAndOut()
    {
        StartCoroutine(DoFadeInAndOut());
    }

    public void FadeOut()
    {
        StartCoroutine(DoFade(RADIUS, 0f));
    }

    public void FadeIn()
    {
        StartCoroutine(DoFade(0, RADIUS));
    }

    IEnumerator DoFadeInAndOut()
    {
        StartCoroutine(DoFade(RADIUS, 0f));
        yield return new WaitForSeconds(_duration);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(DoFade(0, RADIUS));
    }

    IEnumerator DoFade(float start, float end)
    {
        _radius = start;
        UpdateShader();

        var time = 0f;
        while (time < _duration)
        {
            _radius = Mathf.Lerp(start, end, time / _duration);
            time += Time.deltaTime;
            UpdateShader();
            yield return null;
        }

        _radius = end;
        UpdateShader();
    }

    private void UpdateShader()
    {
        var radiusSpeed = Mathf.Max(_horizontal, _vertical);
        material.SetFloat("_Horizontal", _horizontal);
        material.SetFloat("_Vertical", _vertical);
        material.SetFloat("_RadiusSpeed", radiusSpeed);
        material.SetFloat("_Radius", _radius);
        material.SetColor("_FadeColour", _fadeColour);
        material.SetTexture("_FadeTex", _fadeTexture);
        material.SetVector("_Offset", _offset);
    }
}