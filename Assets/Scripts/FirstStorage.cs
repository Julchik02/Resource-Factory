using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStorage : MonoBehaviour
{
    [SerializeField] Resourse _resorses1;
    [SerializeField] float _timeBetweenProduse;
    [SerializeField] int _repositorySize;
    [SerializeField] Transform _resoursesParent;
    Stack<Resourse> availableResources = new Stack<Resourse>();
    float offset = 0;

    void Start()
    {
        StartCoroutine("ResoursesProduse");
    }

    IEnumerator ResoursesProduse()
    {
        while (true)
        {
            if (availableResources.Count < _repositorySize)
            {
                Resourse resource = Instantiate(_resorses1, new Vector3(_resoursesParent.position.x, _resoursesParent.position.y, _resoursesParent.position.z - offset), Quaternion.identity);
                resource.transform.parent = _resoursesParent.transform;
                availableResources.Push(resource);
                offset += 0.5f;
            }
            else 
            {
                UIManager.Instance.ShowNotification("First production Storage is full");
            }
            yield return new WaitForSeconds(_timeBetweenProduse);
        }
    }

    public Resourse DecreaseResourses()
    {
        if (availableResources.Count != 0)
        {
            var resource = availableResources.Pop();
            offset -= 0.5f;
            return resource;
        }
        return null;
    }

}
