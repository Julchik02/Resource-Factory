using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 10;
    [SerializeField] Transform resoursesPosition;
    List<Resources> resoursesInInventory = new List<Resources>();
    float offset = 0;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Produced")) { GrabResources(other.gameObject); }
        if (other.CompareTag("Consumed")) { PutDownResources(other.gameObject); }
    }

    void GrabResources(GameObject repo)
    {
        if (resoursesInInventory.Count >= inventorySize) return;
        FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
        if (factoryManager.RepositoryProducting.GetResourcesCount() == 0) return;
        Resources resource = factoryManager.RepositoryProducting.DecreaseResources();
        resoursesInInventory.Add(resource);
        Vector3 pos = resoursesPosition.position;
        resource.transform.position = new Vector3(pos.x, pos.y + offset, pos.z);
        resource.transform.parent = resoursesPosition;
        offset += 0.5f;
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
            if (resourceFound == null) continue;
            resoursesInInventory.Remove(resourceFound);
            item.IncreaseResources(resourceFound);
            offset -= 0.5f;
        }

    }
}
