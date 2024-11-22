using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class UISnipeCanvas : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1f;
    [SerializeField] AnimationCurve curve;

    [SerializeField] Vector3 originalSize;
    [SerializeField] CanvasGroup canvasGroup;

    [Header("Cross hairs")]
    [SerializeField] GameObject normalCrossHair;


    private void Awake() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start() 
    {
        CharacterManager.Instance.Player.Shooting.OnAim += Set;

        gameObject.SetActive(false);
    }

    void Set(bool isOn)
    {
        if (isOn) 
        {
            gameObject.SetActive(true);
            normalCrossHair.SetActive(false);
        }
        else
            normalCrossHair.SetActive(true);


        if(displayRoutine != null)
        {
            StopCoroutine(displayRoutine);
        }

        displayRoutine = StartCoroutine(Display(isOn));
    }


    Coroutine displayRoutine;
    IEnumerator Display(bool isOn)
    {
        float progress = 0f;
        float elapsedTime = 0f;
        
        float currentAlpha = canvasGroup.alpha;
        float targetAlpha = isOn ? 1f : 0f;
        
        Vector3 currentSize = transform.localScale;
        Vector3 targetSize = isOn ? Vector3.one : originalSize;

        while(progress <= 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, progress);
            transform.localScale = Vector3.Lerp(currentSize,targetSize, progress);

            yield return null;
            elapsedTime += transitionSpeed * Time.deltaTime;
            progress = curve.Evaluate(elapsedTime);
        }

        displayRoutine = null;

        if (!isOn) 
            gameObject.SetActive(false);
    }

}
