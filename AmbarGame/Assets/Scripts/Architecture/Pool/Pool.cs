using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int SizePool = 3;
    [SerializeField] private PackWheat WheatPackPrefab;

    private PoolMono<PackWheat> poolOfPackWheat;
    void Start()
    {
        poolOfPackWheat = new PoolMono<PackWheat>(WheatPackPrefab, SizePool, transform);
    }
    /// <summary>
    /// Получить стак пшеницы из пула.
    /// </summary>
    /// <returns></returns>
    public PackWheat GetPackWheat()
    {
        return poolOfPackWheat.GetFreeElement();
    }
}
