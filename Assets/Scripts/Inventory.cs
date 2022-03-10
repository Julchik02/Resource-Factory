using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Resourse> inventoryResources1 = new List<Resourse>();
    List<Resourse> inventoryResources2 = new List<Resourse>();
    Resourse resource;
    [SerializeField] int inventorySize = 5;
    [SerializeField] Transform emptySpace;
    float offset = 0f;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Produced") { PicUpResourse1(other.gameObject); }
        if (other.tag == "Consumed") { PutDownResources1(other.gameObject); }
        if (other.tag == "Produced2") { PicUpResourse2(other.gameObject); }
        if (other.tag == "Consumed2") { PutDownResources2type2(other.gameObject); PutDownResources2type1(other.gameObject); }
    }

    void PicUpResourse1(GameObject repository)
    {
        if (inventoryResources1.Count < inventorySize)
        {
            var repo = repository.GetComponentInParent<FirstStorage>();
            if (repo == null) return;
            resource = repo.DecreaseResourses();
            if (resource == null) return;
            inventoryResources1.Add(resource);
            resource.transform.parent = transform;
            resource.transform.position = new Vector3(emptySpace.position.x, 
                                                        emptySpace.position.y + offset, 
                                                        emptySpace.position.z);
            offset += 0.5f;
        }

    }
    void PutDownResources1(GameObject repository)
    {
        if (inventoryResources1.Count > 0)
        {
            var repo = repository.GetComponentInParent<SecondStorage>();
            if (repo == null) return;
            resource = inventoryResources1[inventoryResources1.Count - 1];
            if (repo.SpaceCheck() == false) return;
            inventoryResources1.RemoveAt(inventoryResources1.Count - 1);
            if (resource == null) return;
            repo.IncreaseResourses(resource);
            offset -= 0.5f;
        }
    }

    void PicUpResourse2(GameObject repository)
    {
        if (inventoryResources2.Count < inventorySize)
        {
            var repo = repository.GetComponentInParent<SecondStorage>();
            if (repo == null) return;
            resource = repo.DecreaseResourses();
            if (resource == null) return;
            inventoryResources2.Add(resource);
            resource.transform.parent = transform;
            resource.transform.position = new Vector3(emptySpace.position.x,
                                                        emptySpace.position.y + offset,
                                                        emptySpace.position.z);
            offset += 0.5f;
        }

    }
    void PutDownResources2type2(GameObject repository)
    {
        if (inventoryResources2.Count > 0)
        {
            var repo = repository.GetComponentInParent<ThirdStorage>();
            if (repo == null) return;
            resource = inventoryResources2[inventoryResources2.Count - 1];
            if (repo.SpaceCheck2() == false) return;
            inventoryResources2.RemoveAt(inventoryResources2.Count - 1);
            if (resource == null) return;
            repo.IncreaseResourses2(resource);
            offset -= 0.5f;
        }

    }
    void PutDownResources2type1(GameObject repository)
    {
        if (inventoryResources1.Count > 0)
        {
            var repo = repository.GetComponentInParent<ThirdStorage>();
            if (repo == null) return;
            resource = inventoryResources1[inventoryResources1.Count - 1];
            if (repo.SpaceCheck1() == false) return;
            inventoryResources1.RemoveAt(inventoryResources1.Count - 1);
            if (resource == null) return;
            repo.IncreaseResourses1(resource);
            offset -= 0.5f;
        }
    }
    
}
