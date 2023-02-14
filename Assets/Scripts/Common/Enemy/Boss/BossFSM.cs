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
    /// 해당 패턴의 실행이 끝났을때 다음 패턴으로 넘겨주는 함수
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
    /// 랜덤한 패턴을 실행 시켜주는 해주는 함수
    /// </summary>
    /// <returns></returns>
    BossPattern RandomState() //전과 같은 패턴이 나오지 못하게 만듬
    {
        while (true)
        {
            int rNum = Random.Range(1, System.Enum.GetValues(typeof(BossPattern)).Length); //첫번째 NONE State 는 배재함

            _sb.Remove(0, _sb.Length);
            _sb.Append(System.Enum.GetName(typeof(BossPattern), rNum));

            BossPattern pState = (BossPattern)System.Enum.Parse(typeof(BossPattern), _sb.ToString());

            if (patternState == pState) continue;

            return pState;
        }
    }
}
