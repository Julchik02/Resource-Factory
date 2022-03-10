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
        Debug.Log(other);
        PicUpResourse(other.gameObject);
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


}
