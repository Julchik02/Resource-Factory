using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] Resources productingRecources;
    [SerializeField] int _repositorySize = 5;
    public RepositoryProducting RepositoryProducting { get; private set; }
    [SerializeField] List<RepositoryConsuming> repositoryConsumingList = new List<RepositoryConsuming>();
    bool recoursesEnabled = false;
    public List<RepositoryConsuming> RepositoryConsumingList
    {
        get { return repositoryConsumingList; }
    }

    private void Start()
    {
        RepositoryProducting = GetComponentInChildren<RepositoryProducting>();
        StartCoroutine(MakeResources());
    }

    IEnumerator MakeResources()
    {
        while (true)
        {
            recoursesEnabled = true;
            foreach (var item in repositoryConsumingList)
            {
                if (item.GetResourcesCount() == 0) recoursesEnabled = false;
            }
            if (RepositoryProducting.GetResourcesCount() < _repositorySize && recoursesEnabled)
            {
                Resources resources = Instantiate(productingRecources);
                RepositoryProducting.IncreaseResources(resources);
                foreach (var item in repositoryConsumingList)
                {
                    Resources res = item.DecreaseResources();
                    Destroy(res.gameObject);
                }
            }
            else if(RepositoryProducting.GetResourcesCount() == _repositorySize)
            {
                UIManager.Instance.ShowNotification($"{gameObject.name} production Storage is full");
            }
            yield return new WaitForSeconds(1f);
        }


     }

    
    


}
