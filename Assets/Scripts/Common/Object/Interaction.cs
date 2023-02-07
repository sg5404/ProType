using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Reflection;

public enum ePrintStates
{
    None,
    Printing,

}

public class Interaction : MonoBehaviour
{
    [SerializeField] private List<string> scriptStr;
    [SerializeField] private Button textPanel;
    [SerializeField] private TextMeshProUGUI scriptText;

    private StringBuilder _sb;

    public ePrintStates printState;

    public float printTime = 0.5f;

    public int num = 0;

    public bool isPriting = false;

    public bool isFrirst = true;

    public float timer = 0f;

    public LayerMask searchLayer;

    private void Awake()
    {
        _sb = new StringBuilder();


        textPanel.onClick.RemoveAllListeners();
        textPanel.onClick.AddListener(PanelClick);
    }

    private void Start()
    {
        printState = ePrintStates.None;

        isFrirst = true;
    }

    private void Update()
    {
        searchPlayer();
    }

    void searchPlayer()
    {
        Collider2D col;
        col = Physics2D.OverlapCircle(transform.position, 50f, searchLayer);

        if (col != null)
        {
            Debug.Log("�÷��̾� ����");
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isFrirst)
                {
                    NextState();
                    isFrirst = false;
                }
            }
        }
    }

    IEnumerator NoneState()
    {
        printState = ePrintStates.Printing;

        while(printState == ePrintStates.None)
        {
            yield return null;
        }
        NextState();
    }

    IEnumerator PrintingState()
    {
        timer = printTime;
        textPanel.gameObject.SetActive(true);

        //�ؽ�Ʈ �ϳ��� ����ϰ� �ϱ�
        isPriting = true;

        for (int j = 0; j < scriptStr[num].Length; j++)
        {
            yield return new WaitForSeconds(timer);
            scriptText.text = scriptStr[num].Substring(0, j + 1);
        }

        isPriting = false;
    }

    void NextState()
    {
        _sb.Remove(0, _sb.Length);
        _sb.Append(printState.ToString());
        _sb.Append("State");

        MethodInfo info = GetType().GetMethod(_sb.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    void PanelClick()
    {
        Debug.Log("Ŭ��");
        //������϶� �����ٸ�
        if(isPriting)
        {
            timer = 0f;
            return;
        }

        //������� �ƴҶ� �����ٸ�
        if(num + 1 < scriptStr.Count)
        {
            num++;
            NextState();
            return;
        }

        textPanel.gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 50f);
    }

    //triger�� stay�� ����.

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(CompareTag("Player"))
    //    {
    //        //��ȣ�ۿ� Ű ������� �߰��ϱ�
    //        Debug.Log("����");
    //        if(Input.GetKeyDown(KeyCode.F))
    //        {
    //            if(isFrirst)
    //            {
    //                NextState();
    //                isFrirst = false;
    //            }
    //        }
    //    }
    //}
}
