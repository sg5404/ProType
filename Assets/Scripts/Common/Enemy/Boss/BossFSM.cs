using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Reflection;

public enum BossPattern
{
    NONE,
    SKILL_1,
    SKILL_2,
    SKILL_3,
}

public class BossFSM : MonoBehaviour
{
    public BossPattern patternState;

    public float delayTime = 5.0f;

    private StringBuilder _sb;

    private void Awake()
    {
        _sb = new StringBuilder();
    }

    private void Start()
    {
        patternState = BossPattern.NONE;

        NextState();
    }

    IEnumerator NONEState()
    {
        patternState = RandomState();

        while(patternState == BossPattern.NONE)
        {
            yield return null;
        }

        NextState();
    }

    IEnumerator SKILL_1State()
    {
        yield return null;
    }

    IEnumerator SKILL_2State()
    {
        yield return null;
    }

    IEnumerator SKILL_3State()
    {
        yield return null;
    }

    IEnumerator DELAYState()
    {
        float timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;

            if(timer >= delayTime)
            {
                patternState = RandomState();
                NextState();
                break;
            }
        }

        yield return null;
    }

    void NextState_Delay()
    {
        _sb.Remove(0, _sb.Length);
        _sb.Append("DELAYState");

        MethodInfo info = GetType().GetMethod(_sb.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    /// <summary>
    /// �ش� ������ ������ �������� ���� �������� �Ѱ��ִ� �Լ�
    /// </summary>
    void NextState()
    {
        _sb.Remove(0, _sb.Length);
        _sb.Append(patternState.ToString());
        _sb.Append("State");

        MethodInfo info = GetType().GetMethod(_sb.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    /// <summary>
    /// ������ ������ ���� �����ִ� ���ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    BossPattern RandomState() //���� ���� ������ ������ ���ϰ� ����
    {
        while (true)
        {
            int rNum = Random.Range(1, System.Enum.GetValues(typeof(BossPattern)).Length); //ù��° NONE State �� ������

            _sb.Remove(0, _sb.Length);
            _sb.Append(System.Enum.GetName(typeof(BossPattern), rNum));

            BossPattern pState = (BossPattern)System.Enum.Parse(typeof(BossPattern), _sb.ToString());

            if (patternState == pState) continue;

            return pState;
        }
    }
}
