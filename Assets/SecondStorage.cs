using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStorage : MonoBehaviour
{
    [SerializeField] Resourse _resources2;
    [SerializeField] Transform emptySpaseConsumed;
    [SerializeField] Transform emptySpaceProduced;
    Stack<Resourse> availableResources = new Stack<Resourse>();
    Stack<Resourse> madeResources = new Stack<Resourse>();
    private void Start()
    {
        StartCoroutine(MakeResources());
    }
    public Resourse DecreaseResourses()
    {
        if (madeResources.Count != 0)
        {
            var resource = madeResources.Pop();
            return resource;
        }
        return null;
    }
    public void IncreaseResourses(Resourse resourse)
    {
        availableResources.Push(resourse);
        resourse.transform.parent = emptySpaseConsumed;
        resourse.transform.position = emptySpaseConsumed.position;
    }
    private IEnumerator MakeResources()
    {
        while (true)
        {
            if (availableResources.Count != 0)
            {
                Resourse resourse = Instantiate(_resources2, emptySpaceProduced.position, Quaternion.identity);
                madeResources.Push(resourse);
                resourse.transform.parent = emptySpaceProduced;
                resourse.transform.position = emptySpaceProduced.position;
            }
            yield return new WaitForSeconds(1f);
        }
    
    }
}
