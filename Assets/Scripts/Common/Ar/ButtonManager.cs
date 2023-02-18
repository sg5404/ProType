using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private int[] doMovePos = { 1440, 0, -1440 };
    bool isBackgroundMove = false;
    public STATE state = STATE.MAIN;


    [Header("Buttons")]
    [SerializeField] Button TitleButton;
    [SerializeField] Button InventoryButton;
    [SerializeField] Button MainButton;
    [SerializeField] Button ShopButton;
    [SerializeField] Button StartButton;


    public enum STATE
    {
        INVENTORY,
        MAIN,
        SHOP
    }
    private void Awake()
    {
        ButtonEventPlus(InventoryButton, InventoryButtonClick);
        ButtonEventPlus(ShopButton, ShopButtonClick);
        ButtonEventPlus(MainButton, MainButtonClick);
        ButtonEventPlus(StartButton, StartButtonClick);
    }
    public void ChangeState(STATE _state) => state = _state; 
    public bool CompareState(STATE _state) => state == _state;
    public void TitleButtonClick() => SceneManager.LoadScene("MainScene");
    public void InventoryButtonClick()
    {
        if (CompareState(STATE.INVENTORY) || isBackgroundMove) return;
        ChangeState(STATE.INVENTORY);
        MoveBackGround();
    }
    public void MainButtonClick()
    {
        if (CompareState(STATE.MAIN) || isBackgroundMove) return;
        ChangeState(STATE.MAIN);
        MoveBackGround();
    }
    public void ShopButtonClick()
    {
        if (CompareState(STATE.SHOP) || isBackgroundMove) return;
        ChangeState(STATE.SHOP);
        MoveBackGround();
    }
    public void StartButtonClick() => SceneManager.LoadScene("GameScene");
    private void MoveBackGround()
    {
        isBackgroundMove = true;
        background.transform.DOLocalMoveX(doMovePos[(int)state], 0.2f);
        isBackgroundMove = false;
    }
    
    void ButtonEventPlus(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
