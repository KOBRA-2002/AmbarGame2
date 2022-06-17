using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public delegate void Callback();
    private event Callback ClickMovButton;

    private PersonController personController;

    [SerializeField] private TextMeshProUGUI TextNumberOfCoins;
    [SerializeField] private TextMeshProUGUI TextNumberOfWheat;

    [SerializeField] private Animation CoinIconAnimation;
    [SerializeField] private Animation WheatIconAnimation;
    [SerializeField] private Animation SickleIconAnimation;

    /// <summary>
    /// Кнопка продажи пшеницы в амбар.
    /// </summary>
    [SerializeField] private GameObject SellButton;

    /// <summary>
    /// Количество монеток игрока, до того как добаввилис новые
    /// </summary>
    private int oldCoinsValue = 0; 


    void Start()
    {
        DeactiveSellButton();
        personController = ServiceLocator.instance.PersonController;
    }

    /// <summary>
    /// Событие нажатия клавиши "Косить"
    /// </summary>
    public void OnClickMowButton()
    {
        PlaySickleClickAnimation();
        ClickMovButton.Invoke();
    }

    /// <summary>
    /// Подписаться на событие нажатия клавиши "Косить"
    /// </summary>
    /// <param name="callback"></param>
    public void StartListenMowButton(Callback callback)
    {
        ClickMovButton = callback;
    }

    /// <summary>
    /// Увеличить счетчик монет на.
    /// </summary>
    /// <param name="numberOfCoins"></param>
    public void AddCoins(int numberOfCoins)
    {
        StartCoroutine(CoinAdditionAnimation(numberOfCoins));

    }

    /// <summary>
    /// Отобразить количество собранной пшеницы на канвасе.
    /// </summary>
    public void ShowNumberOfWheat(int number)
    {
        TextNumberOfWheat.text = Convert.ToString(number) + "/5";
        WheatCountChangeAnimation();
    }

    /// <summary>
    /// Отобразить количество собранных монет на канвасе.
    /// </summary>
    private void ShowNumberOfCoins(int number)
    {
        TextNumberOfCoins.text = Convert.ToString(number);
    }

    /// <summary>
    /// Запустить анимацию увелечения счетчика монет.
    /// </summary>
    private IEnumerator CoinAdditionAnimation(int number)
    {
        var delta = personController.Coins - oldCoinsValue;
        for (int i = 1; i <= delta; i++)
        {
            ShowNumberOfCoins(oldCoinsValue + i);
            yield return new WaitForSeconds(0.1f);
            CoinIconAnimation.Play();
        }
        oldCoinsValue = personController.Coins;
    }

    /// <summary>
    /// Анимация изменения счетчика пшеницы.
    /// </summary>
    private void WheatCountChangeAnimation()
    {
        WheatIconAnimation.Play();
    }

    /// <summary>
    /// Проиграть анимацию нажатия кнопки скоса пшеницы.
    /// </summary>
    private void PlaySickleClickAnimation()
    {
        SickleIconAnimation.Play();
    }

    /// <summary>
    /// Активировать кнопку продажи пшеницы.
    /// </summary>
    public void ActiveSellButton()
    {
        SellButton.SetActive(true);
    }

    /// <summary>
    /// Отключить кнопку продажи пшеницы.
    /// </summary>
    public void DeactiveSellButton()
    {
        SellButton.SetActive(false);
    }

    /// <summary>
    /// Событие нажатия кнопки продажи пшеницы.
    /// </summary>
    public void ClickSellButton()
    {
        DeactiveSellButton();
        personController.PickUpWheat();
    }

    /// <summary>
    /// Событие нажатия клавиши выхода из игры.
    /// </summary>
    public void ClickExitButton()
    {
        Application.Quit();
    }
}
