using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour
{
    /// <summary>
    /// Количество стаков, которое может унести персонаж.
    /// </summary>
    [SerializeField] private int _SizeBag = 5;

    /// <summary>
    /// Скорость персонажа.
    /// </summary>
    [SerializeField] private float Speed;
    /// <summary>
    /// Рабочий инструмент персонажа (Серп).
    /// </summary>
    [SerializeField] private WorkTool WorkTool;

    /// <summary>
    /// Рюкзак персонажа.
    /// </summary>
    [SerializeField] private Transform Bag;

    private Animator animator;
    private CharacterController characterController;

    /// <summary>
    /// Ссылка на пул объектов.
    /// </summary>
    private Pool pool;

    /// <summary>
    /// Ссылка на портфель(в котором находится собранная пшеница) персонажа.
    /// </summary>
    [SerializeField] private BagController bag;

    /// <summary>
    /// Машина состояний персонажа.
    /// </summary>
    private SMPerson smPerson;

    /// <summary>
    /// Ссылка на канвас.
    /// </summary>
    private CanvasController canvasController;

    /// <summary>
    /// Статус окончания анимации сбора пшеницы.
    /// </summary>
    public bool IsEndStatusForMowClip { set; get; } = false;

    /// <summary>
    /// Количество стаков, которое может унести персонаж.
    /// </summary>
    public int SizeBag { get { return _SizeBag; } }

    private int _CurrentFillBag = 0;
    /// <summary>
    /// Текущая заполненность багажа рюкзака игрока.
    /// </summary>
    public int CurrentFillBag 
    {
        get
        {
            return _CurrentFillBag;
        }
        set
        {
            _CurrentFillBag = value;
            bag.DisplayRequiredNumberOfElements(value);
            canvasController.ShowNumberOfWheat(_CurrentFillBag);
        }
    }

    private int _Coins = 0;

    /// <summary>
    /// Монеты игрока.
    /// </summary>
    public int Coins
    {
        get
        {
            return _Coins;
        }
        set
        {
            _Coins = value;
            canvasController.AddCoins(value);
        }
    }

    /// <summary>
    /// Момент срезания.
    /// </summary>
    public bool IsMomentWorm { set; get; } = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        pool = ServiceLocator.instance.Pool;
        canvasController = ServiceLocator.instance.CanvasController;

        smPerson = new SMPerson(this);
        smPerson.SetIdleState();
    }

    void Update()
    {
        smPerson.UpdateState();
        smPerson.UpdateLogicState();
    }


    /// <summary>
    /// Запустить анимацию покоя.
    /// </summary>
    public void PlayIdleClip()
    {
        animator.SetBool("isWalk", false);
    }

    /// <summary>
    /// Запустить анимацию хотьбы.
    /// </summary>
    public void PlayWalkClip()
    {
        animator.SetBool("isWalk", true);
    }

    /// <summary>
    /// Запустить анимацию сбора пшеницы.
    /// </summary>
    public void PlayMowClip()
    {
        animator.SetTrigger("Mow");
    }

    /// <summary>
    /// Перемещение персонажа.
    /// </summary>
    /// <param name="Dir">Вектор по направлению которого персонаж должен двигаться.</param>
    public void MoveTo(Vector3 Dir)
    {
        characterController.SimpleMove(Dir * Speed);
    }

    /// <summary>
    /// Включить инструмент, которым персонаж собирает пшеницу.
    /// </summary>
    public void ActivateWorkTool()
    {
        WorkTool.Activate();
    }

    /// <summary>
    /// Отключить инструмент, которым персонаж собирает пшеницу.
    /// </summary>
    public void DeactivateWorkTool()
    {
        WorkTool.Deactivate();
    }

    /// <summary>
    /// Повернуть персонажа лицом в сторону движения. 
    /// </summary>
    /// <param name="Dir">Вектор по направлению которого персонаж должен двигаться.</param>
    public void LookAtForward(Vector3 Dir)
    {
        if (Dir != Vector3.zero)
        {
            var playerPos = transform.position;
            var target = playerPos + Dir;
            target.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(target - playerPos);
            transform.rotation = rot;  
        }
    }
    /// <summary>
    /// Вызывается, когда анимация сбора пшеницы завершена.
    /// </summary>
    public void SetEndStatusForMowClip()
    {
        //Debug.Log("Анимация сбора пшеницы завершена");
        IsEndStatusForMowClip = true;
    }

    /// <summary>
    /// Вызывается, когда анимация сбора пшеницы началась.
    /// </summary>
    public void SetStartStatusForMowClip()
    {
        //Debug.Log("Анимация сбора пшеницы началась");
        IsEndStatusForMowClip = false;
    }

    /// <summary>
    /// Вызывается, когда анимация сбора пшеницы находится в моменте срезания пшеницы.
    /// </summary>
    public void SetMomentMowForMowClip()
    {
        IsMomentWorm = true;
    }

    /// <summary>
    /// Забрать пшеницу у персонажа из портфеля.
    /// </summary>
    /// <param name="number">Количество пшеницы, которое необходимо забрать из портфеля.</param>
    public void PickUpWheat()
    {
        StartCoroutine(TakeTurnsHandingOverWheat());
    }

    /// <summary>
    /// Поочереди отправить стаки пшеницы из портфеля персонажа.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeTurnsHandingOverWheat()
    {
        var number = CurrentFillBag;
        for (int i = 0; i< number; i++)
        {
            var packWheat = pool.GetPackWheat();
            packWheat.transform.position = Bag.position;
            packWheat.FlyToAmbar();
            CurrentFillBag -= 1;
            yield return new WaitForSeconds(2f);
        }
    }
}
