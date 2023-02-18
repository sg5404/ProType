using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DrawManager : MonoSingleton<DrawManager>
{
    [SerializeField] List<ElementModule> Elements;
    //private Dictionary<string, int> ElementQuantity = new Dictionary<string, int>();
    private Dictionary<string, int> DebugQuantity = new Dictionary<string, int>(); //����׿� Dictionary


    [SerializeField] private List<ElementModule> NormalList;
    [SerializeField] private List<ElementModule> RareList;
    [SerializeField] private List<ElementModule> EpicList;

    [SerializeField] private Button OneDraw;
    [SerializeField] private Button TenDraw;
    [SerializeField] private Button ResetButton;

    //�� ���� 100�� �ǰ� ����������
    [SerializeField] List<int> DrawPercent;
    // Start is called before the first frame update

    private void Awake()
    {
        Set();
    }

    /// <summary>
    /// ó�� �����Ҷ� �ʿ��� �Լ����� ��Ƴ��� �Լ�
    /// </summary>
    void Set()
    {
        //LoadData();
        ClassifyRarity();
        //ClearElements(); //���߿� �ּ� Ǯ���ֱ�

        ButtonEventSet();
    }

    void ButtonEventSet()
    {
        ButtonEventPlus(OneDraw, DrawOneButtonClick);
        ButtonEventPlus(TenDraw, DrawTenButtonClick);
        ButtonEventPlus(ResetButton, QuantityReset); //���߿� ���ֱ�
    }

    /// <summary>
    /// �����͸� �ҷ����� �Լ�
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
    /// �����͸� �з��ؼ� ������ ����Ʈ�� �־��ִ� �Լ�
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
    /// ������ ��͵� �迭�� �´� ��͵��� ���� ���Ҹ� �ִ� �Լ�
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
    /// Elements[] �迭�� Ŭ�����ϴ� �Լ�
    /// </summary>
    void ClearElements()
    {
        Elements.Clear();
    }

    /// <summary>
    /// 1�� �������� ���� ��Ű�� �Լ�
    /// </summary>
    void DrawOneButtonClick()
    {
        RarityDecide();
        DebugPrint();
    }

    /// <summary>
    /// 10�� �������� �����Ű�� �Լ�
    /// </summary>
    void DrawTenButtonClick()
    {
        for (int i = 0; i < 10; i++) { RarityDecide(); }
        DebugPrint();
    }

    /// <summary>
    /// ����� �α� ����ִ� �Լ�
    /// </summary>
    void DebugPrint()
    {
        foreach (var name in DebugQuantity) { Debug.Log($"{name.Key} : {name.Value}"); }
        DebugQuantity.Clear();
    }

    /// <summary>
    /// �̾����� ��͵��� �������ִ� �Լ�
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
    /// ������ ��͵��� ���Ҹ� �̾��ִ� �Լ�
    /// </summary>
    /// <param name="rarity"></param>
    void RarityDraw(List<ElementModule> rarity)
    {
        int R_num = Random.Range(0, 101);
        R_num %= rarity.Count;

        rarity[R_num].Quantity++;

        //����׿�
        if (!DebugQuantity.ContainsKey(rarity[R_num].name)) { DebugQuantity.Add(rarity[R_num].name, 0); }
        DebugQuantity[rarity[R_num].name]++;
    }

    /// <summary>
    /// ����׿� �Լ��� ��ư�� ������ ���� �ʱ�ȭ
    /// </summary>
    void QuantityReset()
    {
        foreach (var element in Elements)
        {
            element.Quantity = 0;
        }
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ�� �߰������ִ� �Լ�
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    void ButtonEventPlus(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
