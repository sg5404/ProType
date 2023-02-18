using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DrawManager : MonoSingleton<DrawManager>
{
    [SerializeField] List<ElementModule> Elements;
    //private Dictionary<string, int> ElementQuantity = new Dictionary<string, int>();
    private Dictionary<string, int> DebugQuantity = new Dictionary<string, int>(); //디버그용 Dictionary


    [SerializeField] private List<ElementModule> NormalList;
    [SerializeField] private List<ElementModule> RareList;
    [SerializeField] private List<ElementModule> EpicList;

    [SerializeField] private Button OneDraw;
    [SerializeField] private Button TenDraw;
    [SerializeField] private Button ResetButton;

    //총 합이 100이 되게 만들어줘야함
    [SerializeField] List<int> DrawPercent;
    // Start is called before the first frame update

    private void Awake()
    {
        Set();
    }

    /// <summary>
    /// 처움 시작할때 필요한 함수들을 모아놓은 함수
    /// </summary>
    void Set()
    {
        //LoadData();
        ClassifyRarity();
        //ClearElements(); //나중에 주석 풀어주기

        ButtonEventSet();
    }

    void ButtonEventSet()
    {
        ButtonEventPlus(OneDraw, DrawOneButtonClick);
        ButtonEventPlus(TenDraw, DrawTenButtonClick);
        ButtonEventPlus(ResetButton, QuantityReset); //나중에 없애기
    }

    /// <summary>
    /// 데이터를 불러오는 함수
    /// </summary>
    void LoadData()
    {
        int num = 0;
        int quantity = 0;
        string name = "";

        while (num < Elements.Count)
        {
            name = Elements[num].Name;
            quantity = Elements[num].Quantity;
            //ElementQuantity.Add(name, quantity);
            ++num;
        }
    }

    /// <summary>
    /// 데이터를 분류해서 각각의 리스트에 넣어주는 함수
    /// </summary>
    void ClassifyRarity()
    {
        int num = 0;
        Rarity rarity;
        while (num < Elements.Count)
        {
            rarity = Elements[num].Grade;
            InputElemets(rarity, num);
            ++num;
        }
    }

    /// <summary>
    /// 각각의 희귀도 배열에 맞는 희귀도를 가진 원소를 넣는 함수
    /// </summary>
    /// <param name="rarity"></param>
    /// <param name="num"></param>
    void InputElemets(Rarity rarity, int num)
    {
        var RarityList = rarity switch
        {
            Rarity.Normal => NormalList,
            Rarity.Rare => RareList,
            Rarity.Epic => EpicList,
        };

        RarityList.Add(Elements[num]);
    }

    /// <summary>
    /// Elements[] 배열을 클리어하는 함수
    /// </summary>
    void ClearElements()
    {
        Elements.Clear();
    }

    /// <summary>
    /// 1뽑 눌렀을때 실행 시키는 함수
    /// </summary>
    void DrawOneButtonClick()
    {
        RarityDecide();
        DebugPrint();
    }

    /// <summary>
    /// 10뽑 눌렀을때 실행시키는 함수
    /// </summary>
    void DrawTenButtonClick()
    {
        for (int i = 0; i < 10; i++) { RarityDecide(); }
        DebugPrint();
    }

    /// <summary>
    /// 디버그 로그 찍어주는 함수
    /// </summary>
    void DebugPrint()
    {
        foreach (var name in DebugQuantity) { Debug.Log($"{name.Key} : {name.Value}"); }
        DebugQuantity.Clear();
    }

    /// <summary>
    /// 뽑았을때 희귀도를 결정해주는 함수
    /// </summary>
    void RarityDecide()
    {
        var rarity = NormalList;
        int R_num = Random.Range(0, 101);

        if (R_num <= DrawPercent[(int)Rarity.Normal]) { rarity = NormalList; }
        else if (R_num <= DrawPercent[(int)Rarity.Normal] + DrawPercent[(int)Rarity.Rare]) { rarity = RareList; }
        else { rarity = EpicList; }

        RarityDraw(rarity);
    }

    /// <summary>
    /// 정해진 희귀도의 원소를 뽑아주는 함수
    /// </summary>
    /// <param name="rarity"></param>
    void RarityDraw(List<ElementModule> rarity)
    {
        int R_num = Random.Range(0, 101);
        R_num %= rarity.Count;

        rarity[R_num].Quantity++;

        //디버그용
        if (!DebugQuantity.ContainsKey(rarity[R_num].name)) { DebugQuantity.Add(rarity[R_num].name, 0); }
        DebugQuantity[rarity[R_num].name]++;
    }

    /// <summary>
    /// 디버그용 함수로 버튼을 누르면 수량 초기화
    /// </summary>
    void QuantityReset()
    {
        foreach (var element in Elements)
        {
            element.Quantity = 0;
        }
    }

    /// <summary>
    /// 버튼에 이벤트를 추가시켜주는 함수
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    void ButtonEventPlus(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
