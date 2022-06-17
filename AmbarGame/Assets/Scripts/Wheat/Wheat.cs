using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    /// <summary>
    /// Время, за которое вырастет новая пшеница.
    /// </summary>
    [SerializeField] private float GrowthTime = 10;

    /// <summary>
    /// Целостная пшеница, такая как и должна быть.
    /// </summary>
    [SerializeField] private GameObject SolidWheat;

    /// <summary>
    /// То что остается после сбора пшеницы.
    /// </summary>
    [SerializeField] private GameObject NotSolidWheat; // То что остается после сбора пшеницы

    /// <summary>
    /// Точка, куда помещается стак пшеницы, когда ее срезают.
    /// </summary>
    [SerializeField] private Transform PackWheatPos; 

    private Pool pool;

    private PersonController personController;

    /// <summary>
    /// Скошена ли пшеница.
    /// </summary>
    private bool isMoved = false; 

    /// <summary>
    /// Текущее время, которое прошло с момента срезания пшеницы.
    /// </summary>
    private float timer = 0f;

    void Start()
    {
        ShowWheat();

        pool = ServiceLocator.instance.Pool;
        personController = ServiceLocator.instance.PersonController;
    }

    private void Update()
    {
        if(isMoved)
            if (Timer())
            {
                //Debug.Log("Пшеница снова выросла");
                isMoved = false;
                ShowWheat();
            }
    }

    /// <summary>
    /// Скрыть пшеницу и показать то что осталось.
    /// </summary>
    private void HideWheat()
    {
        SolidWheat.SetActive(false);
        NotSolidWheat.SetActive(true);
    }

    /// <summary>
    /// Вернуть пшеницу в изначальное состояние (до срезания).
    /// </summary>
    private void ShowWheat()
    {
        SolidWheat.SetActive(true);
        NotSolidWheat.SetActive(false);
    }

    /// <summary>
    /// Скинуть стак пшеницы
    /// </summary>
    private void SendPackWheat()
    {
        var packWheat = pool.GetPackWheat();
        packWheat.transform.position = PackWheatPos.position;
        packWheat.FlyToBag();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("До травы дотронулись: " + other.tag);
        if(other.tag == "WorkTool" && !isMoved)
        {
            //Debug.Log("До травы дотронулись каким-то рабочим инструментом");
            isMoved = true;
            HideWheat();
            if (personController.CurrentFillBag < personController.SizeBag)
            {
                Debug.Log("В рюкзаке еще есть место ");
                SendPackWheat();
                //personController.CurrentFillBag += 1;
            }
            else
            {
                Debug.Log("Рюкзак забит до отказа");
            }
        }
    }

    /// <summary>
    /// Отсчитывает время, которое прошло с момента срезания пшеницы.
    /// </summary>
    /// <returns>Возращает true, если необходимое время для роста пшеницы прошло.</returns>
    private bool Timer()
    {
        timer += Time.deltaTime;
        if(timer > GrowthTime)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
