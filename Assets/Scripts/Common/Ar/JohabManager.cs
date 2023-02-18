using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class JohabManager : MonoBehaviour
{
    [SerializeField] private GameObject JohabPanel;

    [SerializeField] private List<Button> ElementButtons; //������Ʈ ��ư �������� �������� ������ �ؾ���
    [SerializeField] private List<Button> PanelButtons; //�гξȿ� �ִ� ������Ʈ ��ư �������� ���� ������ �ؾ���
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
    /// ó�� �����Ҷ� �������ִ� �Լ�
    /// </summary>
    void Set()
    {
        ButtonEventSet();   
    }

    /// <summary>
    /// ��ư�� �������� �߰��Ǵ� �̺�Ʈ���� ��Ƴ��� �Լ�
    /// </summary>
    void ButtonEventSet()
    {
        ButtonEventPlus(CloseButton, JohabPanelOff);
        ButtonEventPlus(JohabButton, JohabButtonClick);
        PanelSetEvents();
    }

    /// <summary>
    /// ��ư�鿡 �г��� ������ ������ �������ִ� �Լ��� �־��ִ� �Լ�
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
    /// ���� ��ư�� �������� ������ ���ͼ� �г��� �������ִ� �Լ�
    /// </summary>
    void ElementPanelSet(int num)
    {
        JohabPanelOn();
        //���� �־��ֱ�
        //��ư�� ������ �����, ������ so�� �Ĺڰ� ���� ������
        SetInformation(num);
    }


    /// <summary>
    /// �гο� ������ �־��ִ� �Լ���
    /// </summary>
    /// <param name="num"></param>
    void SetInformation(int num)
    {
        buttonNum = num;
        bringModule(num);

        //����� �Ǵ� ���Ҹ� ����
        resultText.text = module.name;
        resultAmount.text = ($"Amount : { module.Quantity}");

        //������ �Ʒ��� ����
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
    /// ���� ��ư�� �������� �������� ����������
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
    /// ������ �� ���� ��ŭ�� �ڿ��� �ִ��� Ȯ���ϴ� �Լ�
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
            Debug.LogWarning("bringModule �Լ��� module�� �������");
        }
    }

    #region �гε��� ���� �ִ� ���ִ� �Լ�
    /// <summary>
    /// ���� �г��� ���ִ� �Լ�
    /// </summary>
    void JohabPanelOn()
    {
        JohabPanel.SetActive(true);
    }

    /// <summary>
    /// ���� �г��� ���ִ� �Լ�
    /// </summary>
    void JohabPanelOff()
    {
        JohabPanel.SetActive(false);
    }
    #endregion

    #region �̺�Ʈ�� �߰����ִ� �Լ���
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

    /// <summary>
    /// ElementPanelSet �Լ��� ��ư�� �߰������ִ� �Լ�
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
