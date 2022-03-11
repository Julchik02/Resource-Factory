using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryConsuming : MonoBehaviour
{
    [SerializeField] Resources consumingRecources;
    [SerializeField] Transform positionRecources;
    Stack<Resources> consumedResources = new Stack<Resources>();
    public Resources ConsumingRecources => consumingRecources;

    public void IncreaseResources(Resources recources)
    {
        consumedResources.Push(recources);
        recources.transform.position = positionRecources.position;
        recources.transform.parent = transform;
    }
    public Resources DecreaseResources()
    {
        var res = consumedResources.Pop();
        return res;
    }
    public int GetResourcesCount()
    {
        return consumedResources.Count;
    }
}
