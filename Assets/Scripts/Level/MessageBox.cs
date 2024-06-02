using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private float _showTime;
    [SerializeField] private float _showingTime;
    [SerializeField] private float _hidingTime;

    private Coroutine _coroutine;

    public void Show(int count, int neededCount, Sprite sprite)
    {
        _image.sprite = sprite;
        _text.text = $"{count}/{neededCount}";

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = StartCoroutine(TempShowing());
    }

    private IEnumerator TempShowing()
    {
        float time = 0;

        while (time < _showingTime)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0, 1, time / _showingTime);
            yield return null;
        }

        _canvasGroup.alpha = 1;

        yield return new WaitForSeconds(_showingTime);

        time = 0;

        while (time < _hidingTime)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1, 0, time / _hidingTime);
            yield return null;
        }

        _canvasGroup.alpha = 0;

        _coroutine = null;
    }
}
