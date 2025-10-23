using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveLoadTrigger : MonoBehaviour
{
    [Header("Scene Management")]
    [SerializeField] private string _actualScene = "TerrainScene";
    [SerializeField] private string _sceneToLoad = "DungeonScene";
    [SerializeField] private Animation _doorAnimation;

    private bool _isLoaded = false;
    public bool IsLoaded { get { return _isLoaded; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBehaviour>() && !_isLoaded)
        {
            StartCoroutine(LoadSceneAdditive(_sceneToLoad));
        }
    }

    private void CloseDoor()
    {
        _doorAnimation.clip = _doorAnimation.GetClip("DungeonDoorClose");
        _doorAnimation.Play();
    }

    private void OpenDoor(AsyncOperation asyncOp)
    {
        _doorAnimation.clip = _doorAnimation.GetClip("DungeonDoorOpen");
        _doorAnimation.Play();
    }

    private IEnumerator LoadSceneAdditive(string name)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        asyncOp.completed += OpenDoor;

        while (!asyncOp.isDone)
        {
            yield return null;
        }

        _isLoaded = true;
    }

    private IEnumerator UnoadSceneAdditive(string name)
    {
        CloseDoor();

        yield return new WaitForSeconds(_doorAnimation.clip.length);

        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(_actualScene);

        _isLoaded = false;
    }
}
