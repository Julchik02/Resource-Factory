using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Stack<Resourse> inventoryResources = new Stack<Resourse>();
    Resourse resourse;
    [SerializeField] int inventorySize = 5;
    [SerializeField] Transform emptySpace;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Produced") {PicUpResourse(other.gameObject); }
        if (other.tag == "Consumed") { PutDownResources(other.gameObject); }
    }

    void PicUpResourse(GameObject repository)
    {
        if (inventoryResources.Count < inventorySize)
        {
            var repo = repository.GetComponentInParent<FirstStorage>();
            if (repo == null) return;
            resourse = repo.DecreaseResourses();
            if (resourse == null) return;
            inventoryResources.Push(resourse);
            resourse.transform.parent = transform;
            resourse.transform.position = emptySpace.position;
        }
       
    }
    void PutDownResources(GameObject repository)
    {
        if (inventoryResources.Count > 0)
        {
            var repo = repository.GetComponentInParent<SecondStorage>();
            if (repo == null) return;
            resourse = inventoryResources.Pop();
            if (resourse == null) return;
            repo.IncreaseResourses(resourse);
            }
    }


}
