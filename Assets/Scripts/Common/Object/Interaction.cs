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

    public float delayTime = 0.5f;

    private int num = 0;

    public bool isPriting = false;

    private bool isFrirst = true;

    private float timer = 0f;

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
        timer = delayTime;
        textPanel.gameObject.SetActive(true);

        //�ؽ�Ʈ �ϳ��� ����ϰ� �ϱ�
        isPriting = true;

        for (int j = 0; j < scriptStr[num].Length; j++)
        {
            if (!isPriting) break;
            yield return new WaitForSeconds(timer);
            scriptText.text = scriptStr[num].Substring(0, j + 1);
        }

        //�������� Ȥ�� ���� �ʱ�ȭ �ѹ�
        scriptText.text = scriptStr[num];
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
        //������϶� �����ٸ�
        if(isPriting)
        {
            isPriting = false;
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
