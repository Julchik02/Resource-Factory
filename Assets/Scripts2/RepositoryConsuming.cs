using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryConsuming : MonoBehaviour
{
    [SerializeField] Resources consumingRecources;
    [SerializeField] Transform positionRecources;
    Stack<Resources> consumedResources = new Stack<Resources>();
    public Resources ConsumingRecources => consumingRecources;
    float offset = 0;
    public void IncreaseResources(Resources recources)
    {
        consumedResources.Push(recources);
        Vector3 pos = positionRecources.position;
        recources.transform.position = new Vector3(pos.x, pos.y, pos.z - offset);
        recources.transform.parent = transform;
        offset += 0.5f;
    }
    public Resources DecreaseResources()
    {
        var res = consumedResources.Pop();
        offset -= 0.5f;
        return res;
    }
    public int GetResourcesCount()
    {
        return consumedResources.Count;
    }
}
