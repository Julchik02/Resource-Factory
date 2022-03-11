using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 10;
    [SerializeField] Transform resoursesPosition;
    List<Resources> resoursesInInventory = new List<Resources>();
    [SerializeField] float moveSpeed = 4;
    float offset = 0;
    bool transferInProgress;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Produced")) { StartCoroutine(GrabResources(other.gameObject)); }
        if (other.CompareTag("Consumed")) { StartCoroutine(PutDownResources(other.gameObject)); }
    }

    IEnumerator GrabResources(GameObject repo)
    {
       
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (resoursesInInventory.Count < inventorySize)
        {
            FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
            if (factoryManager.RepositoryProducting.GetResourcesCount() == 0)
            {
                transferInProgress = false;
                yield break;
            }
            Resources resource = factoryManager.RepositoryProducting.DecreaseResources();
            resoursesInInventory.Add(resource);
            Vector3 pos = resoursesPosition.position;
            Vector3 endPosition = new Vector3(pos.x, pos.y + offset, pos.z);
            resource.transform.parent = resoursesPosition;
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            offset += 0.5f;
        }
        transferInProgress = false;

    }

    IEnumerator PutDownResources(GameObject repo)
    {
        
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (resoursesInInventory.Count > 0)
        {
            FactoryManager factoryManager = repo.GetComponentInParent<FactoryManager>();
            foreach (var item in factoryManager.RepositoryConsumingList)
            {
                Resources.Type resoursesType = item.ConsumingRecources.ResourceType;
                Resources resourceFound = resoursesInInventory.FindLast
                (res => res.ResourceType == resoursesType);
                if (resourceFound == null)
                {
                    transferInProgress = false;
                    yield break;
                }
                resoursesInInventory.Remove(resourceFound);
                Vector3 endPosition = item.GetEndPosition(resourceFound);
                yield return StartCoroutine(MoveResources(resourceFound, resourceFound.transform.position, endPosition));
                offset -= 0.5f;
                item.IncreaseResources(resourceFound);
            }
            transferInProgress = false;
        }
    }
    IEnumerator MoveResources(Resources resources, Vector3 startPosition, Vector3 endPosition)
    {

        float step = 0f;
        while (step <= 1)
        {
            resources.transform.position = Vector3.Lerp(startPosition, endPosition, step);

            yield return null;
            step += Time.deltaTime * moveSpeed;
        }
        resources.transform.position = endPosition;
        }
}
