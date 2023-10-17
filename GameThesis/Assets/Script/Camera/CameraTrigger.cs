using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraTrigger : MonoBehaviour
{
    public GameObject blurFX;

    [Header("Post Processing Profile")]
    [SerializeField] private Volume postProcessing_Volume;
    [SerializeField] private VolumeProfile VFXprofile;

    [Header("Post Processing Parameter")]
    [Header("Vignette")]
    private Vignette vignette;
    public float maxVignette = 0.8f;
    public float minVignette = 0.3f;
    public float stepVignette = 0.1f;

    [Header("Depht of Field")]
    private DepthOfField depthOfField;
    public float maxDOF = 0;
    public float minDOF = 0;
    public float stepDOF = 0;

    public float duration;
    public float magnitude;

    private void Start()
    {
        postProcessing_Volume.profile.TryGet(out vignette);
        postProcessing_Volume.profile.TryGet(out depthOfField);

        Vignette_StepDown();
        DOF_StepDown();
    }

    public void Vignette_StepUp()
    {
        vignette.intensity.value += stepVignette;
        if (vignette.intensity.value >= maxVignette)
        {
            vignette.intensity.value = maxVignette;
        }
    }
    public void Vignette_StepDown()
    {
        vignette.intensity.value = minVignette;
    }


    public void DOF_StepUp()
    {
        depthOfField.aperture.value -= stepDOF;
    }

    public void DOF_StepDown()
    {
        depthOfField.aperture.value = maxDOF;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
