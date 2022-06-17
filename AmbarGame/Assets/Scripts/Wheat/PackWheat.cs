using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PackWheat : MonoBehaviour
{
    /// <summary>
    /// Куда летят стаки пшеницы  (рюкзак игрока)
    /// </summary>
    [SerializeField] private Transform BagPersonTarget;

    /// <summary>
    /// Куда летят стаки пшеницы (амбар)
    /// </summary>
    [SerializeField] private Transform AmbarTarget; 

    private Vector3 P0;
    [SerializeField] private Transform P1;
    [SerializeField] private Transform P2;
    private Vector3 P3;

    private PersonController personController;

    /// <summary>
    /// Летит ли стак сена в Амбар, если нет, значит в рюкзак к персонажу/
    /// </summary>
    private bool isFlyToAmbar = false; 

    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        personController = ServiceLocator.instance.PersonController;
    }


    /// <summary>
    /// Отправить стак пшеницы в рюкзак игрока.
    /// </summary>
    public void FlyToBag()
    {
        isFlyToAmbar = false;
        P0 = transform.position;
        P3 = BagPersonTarget.position;
        DOTween.To(FlyFunction,0, 1, 1);
    }

    /// <summary>
    /// Отправить стак пшеницы в амбар.
    /// </summary>
    public void FlyToAmbar()
    {
        isFlyToAmbar = true;
        P0 = transform.position;
        P3 = AmbarTarget.position;
        DOTween.To(FlyFunction, 0, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Storage")
        {
            if (personController.CurrentFillBag < personController.SizeBag)
            {
                // Временное решение. Когда игрок продает пшеницу, стаки пшеницы берутся из пула и помещаются в портфель персонажа, но без этого условия
                if ((isFlyToAmbar && other.name == "AmbarStorage"))
                {
                    Debug.Log("Стак сена добрался до пункта хранилища");
                    gameObject.SetActive(false);
                    personController.Coins += 15;
                }

                else if (!isFlyToAmbar)
                {
                    gameObject.SetActive(false);
                    personController.CurrentFillBag += 1;
                }
            }
        }
    }

    /// <summary>
    /// Кривая безье. 
    /// </summary>
    /// <param name="t"></param>
    private void FlyFunction(float t)
    {
        var p01 = Vector3.Lerp(P0, P1.position, t);
        var p12 = Vector3.Lerp(P1.position, P2.position, t);
        var p23 = Vector3.Lerp(P2.position, P3, t);

        var p012 = Vector3.Lerp(p01, p12, t);
        var p123 = Vector3.Lerp(p12, p23, t);

        var p0123 = Vector3.Lerp(p012, p123, t);

        transform.position = p0123;
    }
}
