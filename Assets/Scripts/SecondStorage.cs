using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStorage : MonoBehaviour
{
    [SerializeField] Resourse _resources2;
    [SerializeField] Transform emptySpaseConsumed;
    [SerializeField] Transform emptySpaceProduced;
    [SerializeField] int _repositorySize;
    Stack<Resourse> availableResources = new Stack<Resourse>();
    Stack<Resourse> madeResources = new Stack<Resourse>();
    float offsetConsumed = 0f;
    float offsetProduced = 0f;
    private void Start()
    {
        StartCoroutine(MakeResources());
    }
    public Resourse DecreaseResourses()
    {
        if (madeResources.Count != 0)
        {
            var resource = madeResources.Pop();
            offsetProduced -= 0.5f;
            return resource;
        }
        return null;
    }
    public void IncreaseResourses(Resourse resourse)
    {
        if(availableResources.Count < _repositorySize)
        availableResources.Push(resourse);
        resourse.transform.parent = emptySpaseConsumed;
        resourse.transform.position = new Vector3 (emptySpaseConsumed.position.x, emptySpaseConsumed.position.y, emptySpaseConsumed.position.z - offsetConsumed);
        offsetConsumed += 0.5f;
    }
    public bool SpaceCheck()
    {
        return availableResources.Count < _repositorySize;
    }
    private IEnumerator MakeResources()
    {
        while (true)
        {
            if (availableResources.Count > 0 && madeResources.Count < _repositorySize)
            {
                Resourse resourse = Instantiate(_resources2, new Vector3(emptySpaceProduced.position.x, emptySpaceProduced.position.y, emptySpaceProduced.position.z - offsetProduced), Quaternion.identity);
                madeResources.Push(resourse);
                var resource1 = availableResources.Pop();
                Destroy(resource1.gameObject);
                offsetProduced += 0.5f;
                offsetConsumed -= 0.5f;
                resourse.transform.parent = emptySpaceProduced;
            }
            yield return new WaitForSeconds(1f);
        }
    
    }
}
