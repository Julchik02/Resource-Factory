using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 5;
    [SerializeField] Transform emptySpace;
    [SerializeField] float moveSpeed = 4f;
    List<Resourse> inventoryResources1 = new List<Resourse>();
    List<Resourse> inventoryResources2 = new List<Resourse>();
    Resourse resource;
    bool transferInProgress;
 
    float offset = 0f;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Produced") { StartCoroutine(PicUpResourse1(other.gameObject)); }
        if (other.tag == "Consumed") { StartCoroutine(PutDownResources1(other.gameObject)); }
        if (other.tag == "Produced2") { StartCoroutine(PicUpResourse2(other.gameObject)); }
        if (other.tag == "Consumed2") { StartCoroutine(PutDownResources2type2(other.gameObject)); StartCoroutine(PutDownResources2type1(other.gameObject)); }
    }

    IEnumerator PicUpResourse1(GameObject repository)
    {
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (inventoryResources1.Count < inventorySize)
        {
            var repo = repository.GetComponentInParent<FirstStorage>();
            if (repo == null) { transferInProgress = false; yield break; }
            resource = repo.DecreaseResourses();
            if (resource == null) { transferInProgress = false; yield break; }
            inventoryResources1.Add(resource);
            resource.transform.parent = transform;
            Vector3 endPosition = new Vector3(emptySpace.position.x, 
                                                        emptySpace.position.y + offset, 
                                                        emptySpace.position.z);
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            offset += 0.5f;
        }
        transferInProgress = false;
    }
    IEnumerator PutDownResources1(GameObject repository)
    {
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (inventoryResources1.Count > 0)
        {
            var repo = repository.GetComponentInParent<SecondStorage>();
            if (repo == null) { transferInProgress = false; yield break; }
            resource = inventoryResources1[inventoryResources1.Count - 1];
            if (repo.SpaceCheck() == false) { transferInProgress = false; yield break; }
            inventoryResources1.RemoveAt(inventoryResources1.Count - 1);
            if (resource == null) { transferInProgress = false; yield break; };
            Vector3 endPosition = repo.GetEmptySpace(resource);
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            offset -= 0.5f;
            repo.IncreaseResourses(resource);
        }
        transferInProgress = false;
    }

    IEnumerator PicUpResourse2(GameObject repository)
    {
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (inventoryResources2.Count < inventorySize)
        {
            var repo = repository.GetComponentInParent<SecondStorage>();
            if (repo == null) { transferInProgress = false; yield break; }
            resource = repo.DecreaseResourses();
            if (resource == null) { transferInProgress = false; yield break; }
            inventoryResources2.Add(resource);
            resource.transform.parent = transform;
            Vector3 endPosition = new Vector3(emptySpace.position.x,
                                                                   emptySpace.position.y + offset,
                                                                   emptySpace.position.z);
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            offset += 0.5f;
        }
        transferInProgress = false;

    }
    IEnumerator PutDownResources2type2(GameObject repository)
    {
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (inventoryResources2.Count > 0)
        {
            var repo = repository.GetComponentInParent<ThirdStorage>();
            if (repo == null) { transferInProgress = false; yield break; }
            resource = inventoryResources2[inventoryResources2.Count - 1];
            if (repo.SpaceCheck2() == false) { transferInProgress = false; yield break; }
            inventoryResources2.RemoveAt(inventoryResources2.Count - 1);
            if (resource == null) {transferInProgress = false; yield break;}
            Vector3 endPosition = repo.GetEmptySpace2(resource);
            offset -= 0.5f;
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            repo.IncreaseResourses2(resource);
        }
        transferInProgress = false;
    }
    IEnumerator PutDownResources2type1(GameObject repository)
    {
        yield return new WaitUntil(() => !transferInProgress);
        transferInProgress = true;
        while (inventoryResources1.Count > 0)
        {
            var repo = repository.GetComponentInParent<ThirdStorage>();
            if (repo == null) { transferInProgress = false; yield break; }
            resource = inventoryResources1[inventoryResources1.Count - 1];
            if (repo.SpaceCheck1() == false) { transferInProgress = false; yield break; }
            inventoryResources1.RemoveAt(inventoryResources1.Count - 1);
            if (resource == null) { transferInProgress = false; yield break; }
            Vector3 endPosition = repo.GetEmptySpace1(resource);
            offset -= 0.5f;
            yield return StartCoroutine(MoveResources(resource, resource.transform.position, endPosition));
            repo.IncreaseResourses1(resource);
        }
        transferInProgress = false;
    }
    private IEnumerator MoveResources(Resourse resource, Vector3 startPosition, Vector3 endPosition)
    {
        float step = 0f;
        while (step <= 1)
        {
            resource.transform.position = Vector3.Lerp(startPosition, endPosition, step);
            yield return null;
            step += Time.deltaTime * moveSpeed;
        }
        resource.transform.position = endPosition;
    }
    
}
