using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryProducting : MonoBehaviour
{
    [SerializeField] Resources productingRecources;
    [SerializeField] Transform positionRecources;
    Stack<Resources> productedResources = new Stack<Resources>();
    public Resources ProductingRecources => productingRecources;

    public void IncreaseResources(Resources recource)
    {
        productedResources.Push(recource);
        recource.transform.position = positionRecources.position;
        recource.transform.parent = transform;
    }

    public Resources DecreaseResources()
    {
        var res = productedResources.Pop();
        return res;
    }
    public int GetResourcesCount()
    {
        return productedResources.Count;
    }
}
