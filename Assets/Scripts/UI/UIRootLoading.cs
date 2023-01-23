using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum eLoadingStatus
{
    None,
    Unload,
    NextScene,
    Done,
}

public class UIRootLoading : MonoBehaviour
{
    private StringBuilder _sb;

    private eLoadingStatus loadingState;
    private AsyncOperation unLoadDone, loadLevelDone;
    private float loadingLimitTime;

    [SerializeField] private Slider loadingSlider;

    const float MAX_LOADING_TIME = 4.0f;    // 로딩시간은 최소 1초 이상

    void Awake()
    {
        _sb = new StringBuilder();
    }

    void Start()
    {
        loadingState = eLoadingStatus.None;
        NextState();
    }

    IEnumerator NoneState()
    {
        loadingState = eLoadingStatus.Unload;

        while (loadingState == eLoadingStatus.None)
        {

            yield return null;
        }

        NextState();
    }

    IEnumerator UnloadState()
    {
        unLoadDone = Resources.UnloadUnusedAssets();
        System.GC.Collect();

        while (loadingState == eLoadingStatus.Unload)
        {
            if (unLoadDone.isDone)
            {
                loadingState = eLoadingStatus.NextScene;
            }

            yield return null;
        }

        NextState();
    }

    IEnumerator NextSceneState()
    {
        loadLevelDone = SceneManager.LoadSceneAsync("MainScene");

        loadingLimitTime = MAX_LOADING_TIME;
        while (loadingState == eLoadingStatus.NextScene)
        {
            loadingSlider.value = 1 - (loadingLimitTime - 1) / (MAX_LOADING_TIME - 1);
            loadingLimitTime -= Time.deltaTime;
            if (loadLevelDone.isDone && loadingLimitTime <= 0)
            {
                loadingState = eLoadingStatus.Done;
            }

            yield return null;
        }

        NextState();
    }

    IEnumerator DoneState()
    {
        // 모든 로딩이 완료 되었다면 LoadingDone을 호출해준다
        MGScene.Instance.LoadingDone();

        while (loadingState == eLoadingStatus.Done)
        {
            yield return null;
        }

        //NextState();
    }

    void NextState()
    {
        _sb.Remove(0, _sb.Length);
        _sb.Append(loadingState.ToString());
        _sb.Append("State");

        MethodInfo info = GetType().GetMethod(_sb.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    void Update()
    {
        //Debug.Log(loadingState);
    }
}
