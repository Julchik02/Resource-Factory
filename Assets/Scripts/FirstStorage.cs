using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStorage : MonoBehaviour
{
    [SerializeField] Resourse _resorses1;
    [SerializeField] float _timeBetweenProduse;
    [SerializeField] int _repositorySize;
    [SerializeField] GameObject _resoursesParent;
    Stack<Resourse> availableResources = new Stack<Resourse>();
    Vector3[] resoursesPositions;

    void Start()
    {
        resoursesPositions = new Vector3[_repositorySize];
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < _repositorySize / 2; i++)
            {
                resoursesPositions[j * _repositorySize / 2 + i] = new Vector3(-9f + 1.2f * j, 0.39f, 9.5f - 0.7f * i);
            }
        }
        StartCoroutine("ResoursesProduse");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ResoursesProduse()
    {
        while (true)
        {
            if (availableResources.Count < _repositorySize)
            {
                Resourse resource = Instantiate(_resorses1, new Vector3(-9f, 0.39f, 9.5f), Quaternion.identity, _resoursesParent.transform);
                //resource.transform.parent = _resoursesParent.transform;
                availableResources.Push(resource);
            }
            yield return new WaitForSeconds(_timeBetweenProduse);
        }
    }

    public Resourse DecreaseResourses()
    {
        if (availableResources.Count != 0)
        {
            var resource = availableResources.Pop();
            return resource;
        }
        return null;
    }

}
