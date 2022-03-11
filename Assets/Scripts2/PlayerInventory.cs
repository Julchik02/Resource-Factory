using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 10;
    [SerializeField] Transform resoursesPosition;
    List<Resources> resoursesInInventory = new List<Resources>();


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Produced") { GrabResources(other.gameObject); }
        if (other.tag == "Consumed") { PutDownResources(other.gameObject); }
    }

    void GrabResources(GameObject repo)
    {
        if (resoursesInInventory.Count >= inventorySize) return;
        FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
        if (factoryManager.RepositoryProducting.GetResourcesCount() == 0) return;
        Resources resource = factoryManager.RepositoryProducting.DecreaseResources();
        resoursesInInventory.Add(resource);
        resource.transform.position = resoursesPosition.position;
        resource.transform.parent = resoursesPosition;
    }

    void PutDownResources(GameObject repo)
    {
        if (resoursesInInventory.Count == 0) return;
        FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
        foreach (var item in factoryManager.RepositoryConsumingList)
        {
            Resources.Type resoursesType = item.ConsumingRecources.ResourceType;
            Resources resourceFound = resoursesInInventory.FindLast
            (res => res.ResourceType == resoursesType);
            if (resourceFound == null) return;
            resoursesInInventory.Remove(resourceFound);
            item.IncreaseResources(resourceFound);
        }

    }
}
