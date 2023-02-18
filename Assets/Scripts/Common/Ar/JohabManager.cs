using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class JohabManager : MonoBehaviour
{
    [SerializeField] private GameObject JohabPanel;

    [SerializeField] private List<Button> ElementButtons; //엘리먼트 버튼 눌렀을때 조합정보 나오게 해야함
    [SerializeField] private List<Button> PanelButtons; //패널안에 있는 엘리먼트 버튼 눌렀을때 정보 나오게 해야함
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button JohabButton;
    [SerializeField] private List<GameObject> PanelElement;
    [SerializeField] private List<TextMeshProUGUI> PanelNameText;
    [SerializeField] private List<TextMeshProUGUI> PanelAmountText;
    [SerializeField] private List<TextMeshProUGUI> PanelNecessaryText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI resultAmount;

    private int buttonNum;
    public ElementModule module;

    private void Awake()
    {
        JohabPanelOff();
        Set();
    }

    /// <summary>
    /// 처음 시작할때 셋팅해주는 함수
    /// </summary>
    void Set()
    {
        ButtonEventSet();   
    }

    /// <summary>
    /// 버튼을 눌렀을때 추가되는 이벤트들을 모아놓은 함수
    /// </summary>
    void ButtonEventSet()
    {
        ButtonEventPlus(CloseButton, JohabPanelOff);
        ButtonEventPlus(JohabButton, JohabButtonClick);
        PanelSetEvents();
    }

    /// <summary>
    /// 버튼들에 패널을 각자의 정보로 셋팅해주는 함수를 넣어주는 함수
    /// </summary>
    void PanelSetEvents()
    {
        int i = 0;
        foreach (Button btn in ElementButtons)
        {
            PanelSetEventPlus(ElementButtons[i], i);
            ++i;
        }
    }

    /// <summary>
    /// 원소 버튼을 눌렀을때 정보를 들고와서 패널을 셋팅해주는 함수
    /// </summary>
    void ElementPanelSet(int num)
    {
        JohabPanelOn();
        //정보 넣어주기
        //버튼에 인포를 만들고, 인포에 so를 쳐박고 정보 들고오기
        SetInformation(num);
    }


    /// <summary>
    /// 패널에 정보를 넣어주는 함수들
    /// </summary>
    /// <param name="num"></param>
    void SetInformation(int num)
    {
        buttonNum = num;
        bringModule(num);

        //결과가 되는 원소를 셋팅
        resultText.text = module.name;
        resultAmount.text = ($"Amount : { module.Quantity}");

        //나머지 아래를 셋팅
        int i = 0;
        foreach(var tree in module.JohabTree.ElementTrees)
        {
            PanelElement[i].SetActive(true);
            PanelNameText[i].text = tree.Element.name;
            PanelNecessaryText[i].text = ($"Need : {tree.Amount}");
            PanelAmountText[i].text = ($"Amount : {tree.Element.Quantity}");
            ++i;
        }
    }

    /// <summary>
    /// 조합 버튼을 눌렀을때 합쳐질지 안합쳐질지
    /// </summary>
    void JohabButtonClick()
    {
        if(CanJohab())
        {
            bringModule(buttonNum);

            foreach (var tree in module.JohabTree.ElementTrees)
            {
                tree.Element.Quantity -= tree.Amount;
            }

            module.Quantity++;
            SetInformation(buttonNum);
        }
    }

    /// <summary>
    /// 조합할 수 있을 만큼의 자원이 있는지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    bool CanJohab()
    {
        bringModule(buttonNum);

        foreach (var tree in module.JohabTree.ElementTrees)
        {
            if (tree.Element.Quantity < tree.Amount) { return false; }
        }
        return true;
    }

    void bringModule(int num)
    {
        module = ElementButtons[num].GetComponent<ElementInfo>().elementModule;

        if (!module)
        {
            Debug.LogWarning("bringModule 함수의 module가 비어있음");
        }
    }

    #region 패널들을 껏다 켯다 해주는 함수
    /// <summary>
    /// 조합 패널을 켜주는 함수
    /// </summary>
    void JohabPanelOn()
    {
        JohabPanel.SetActive(true);
    }

    /// <summary>
    /// 조합 패널을 꺼주는 함수
    /// </summary>
    void JohabPanelOff()
    {
        JohabPanel.SetActive(false);
    }
    #endregion

    #region 이벤트를 추가해주는 함수들
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

    /// <summary>
    /// ElementPanelSet 함수를 버튼에 추가시켜주는 함수
    /// </summary>
    /// <param name="button"></param>
    /// <param name="num"></param>
    void PanelSetEventPlus(Button button, int num)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ElementPanelSet(num));
    }
    #endregion
}
