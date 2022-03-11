using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryProducting : MonoBehaviour
{
    [SerializeField] Resources productingRecources;
    [SerializeField] Transform positionRecources;
    Stack<Resources> productedResources = new Stack<Resources>();
    public Resources ProductingRecources => productingRecources;
    float offset = 0;

    public void IncreaseResources(Resources recource)
    {
        productedResources.Push(recource);
        Vector3 pos = positionRecources.position;
        recource.transform.position = new Vector3 (pos.x, pos.y, pos.z - offset);
        recource.transform.parent = transform;
        offset += 0.5f;
    }

    public Resources DecreaseResources()
    {
        var res = productedResources.Pop();
        offset -= 0.5f;
        return res;
    }
    public int GetResourcesCount()
    {
        return productedResources.Count;
    }
}
