using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdStorage : MonoBehaviour
{

    [SerializeField] Resourse _recources3;
    [SerializeField] int _repositorySize;
    [SerializeField] Transform emptySpaceResourses1;
    [SerializeField] Transform emptySpaceResourses2;
    [SerializeField] Transform emptySpaceResourses3;
    [SerializeField] float _timeBetweenResourceMade;
    Stack<Resourse> avaibleResources1 = new Stack<Resourse>();
    Stack<Resourse> avaibleResources2 = new Stack<Resourse>();
    Stack<Resourse> madeResources = new Stack<Resourse>();
    float offsetResourses1 = -0.5f;
    float offsetResourses2 = -0.5f;
    float offsetResourses3 = 0;
    void Start()
    {
        StartCoroutine(ResourceMade());
    }


    private IEnumerator ResourceMade()
    {
        while (true)
        {
            if (avaibleResources1.Count > 0 && avaibleResources2.Count > 0 && madeResources.Count < _repositorySize)
            {
                Resourse resourse = Instantiate(_recources3);
                madeResources.Push(resourse);
                var resource1 = avaibleResources1.Pop();
                Destroy(resource1.gameObject);
                var resourse2 = avaibleResources2.Pop();
                Destroy(resourse2.gameObject);
                resourse.transform.parent = emptySpaceResourses3;
                resourse.transform.position = new Vector3(emptySpaceResourses3.position.x,
                                                           emptySpaceResourses3.position.y,
                                                           emptySpaceResourses3.position.z - offsetResourses3);
                offsetResourses3 += 0.5f;
                offsetResourses2 -= 0.5f;
                offsetResourses1 -= 0.5f;
            }
            yield return new WaitForSeconds(_timeBetweenResourceMade);
        }

    }
    public bool SpaceCheck1()
    {
        return avaibleResources1.Count < _repositorySize;
    }
    public bool SpaceCheck2()
    {
        return avaibleResources2.Count < _repositorySize;
    }
    public void IncreaseResourses1(Resourse resourse)
    {
        avaibleResources1.Push(resourse);
    }
    public Vector3 GetEmptySpace1(Resourse resourse)
    {
        resourse.transform.parent = emptySpaceResourses1;
        offsetResourses1 += .5f;
        return new Vector3(emptySpaceResourses1.position.x,
                                                  emptySpaceResourses1.position.y,
                                                  emptySpaceResourses1.position.z - offsetResourses1);
    }
    public void IncreaseResourses2(Resourse resourse)
    {
        avaibleResources2.Push(resourse);
    }
    public Vector3 GetEmptySpace2(Resourse resourse)
    {
        resourse.transform.parent = emptySpaceResourses2;
        offsetResourses2 += .5f;

        return new Vector3(emptySpaceResourses2.position.x,
                                                   emptySpaceResourses2.position.y,
                                                   emptySpaceResourses2.position.z - offsetResourses2);
    }
}
