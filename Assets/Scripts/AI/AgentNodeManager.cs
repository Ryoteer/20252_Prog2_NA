using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNodeManager : MonoBehaviour
{
    private Transform[] _nodes;

    private void Awake()
    {
        _nodes = GetComponentsInChildren<Transform>();
        GameManager.Instance.AgentNodes.AddRange(_nodes);
    }
}
