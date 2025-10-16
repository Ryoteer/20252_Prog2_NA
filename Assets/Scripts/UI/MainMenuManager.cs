using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneLoadManager.Instance.LoadScene(name);
    }
}
