using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 10;
    [SerializeField] Transform resoursesPosition;
    List<Resources> resoursesInInventory = new List<Resources>();
    float offset = 0;
    bool transferInProgress;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Produced")) { StartCoroutine(GrabResources(other.gameObject)); }
        if (other.CompareTag("Consumed")) { PutDownResources(other.gameObject); }
    }

    IEnumerator GrabResources(GameObject repo)
    {
        
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (resoursesInInventory.Count < inventorySize)
        {
            FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
            if (factoryManager.RepositoryProducting.GetResourcesCount() == 0) yield break;
            Resources resource = factoryManager.RepositoryProducting.DecreaseResources();
            resoursesInInventory.Add(resource);
            Vector3 pos = resoursesPosition.position;
            Vector3 endPosition = new Vector3(pos.x, pos.y + offset, pos.z);
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            resource.transform.parent = resoursesPosition;
            offset += 0.5f;
        }
        transferInProgress = false;

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
    IEnumerator MoveResources(Resources resources, Vector3 startPosition, Vector3 endPosition)
    {

        float step = 0.25f;
        while (step <= 1)
        {
            resources.transform.position = Vector3.Lerp(startPosition, endPosition, step);
            
            yield return new WaitForSeconds(0.1f);
            step += 0.25f;
        }
        resources.transform.position = endPosition;
         }
}
