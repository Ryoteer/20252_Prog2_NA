using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    #region Singleton
    public static SceneLoadManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("<color=orange>UI</color>")]
    [SerializeField] private Image[] _images;
    [SerializeField] private Image _loadBarImage;
    [SerializeField] private float _fadeSpeed = 1.0f;

    private bool _isLoading = false;
    public bool IsLoading { get { return _isLoading; } }

    private void Start()
    {        
        foreach (var image in _images)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            image.enabled = false;
        }
    }

    public void LoadScene(string name)
    {
        if (!_isLoading)
        {
            StartCoroutine(LoadSceneAsync(name));
        }
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        _isLoading = true;

        foreach (var image in _images)
        {
            image.enabled = true;
        }

        _loadBarImage.fillAmount = 0.0f;

        float t = 0.0f;

        while(t < 1.0f)
        {
            t += Time.deltaTime / _fadeSpeed;

            foreach (var image in _images)
            {              
                image.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, t));                
            }

            yield return null;
        }

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(name);

        asyncOp.allowSceneActivation = false;

        while(asyncOp.progress < 0.9f)
        {
            _loadBarImage.fillAmount = asyncOp.progress / 0.9f;

            yield return null;
        }

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        asyncOp.allowSceneActivation = true;

        t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / _fadeSpeed;

            foreach (var image in _images)
            {
                image.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1.0f, 0.0f, t));
            }

            yield return null;
        }

        foreach (var image in _images)
        {
            image.enabled = false;
        }

        _isLoading = false;
    }
}
