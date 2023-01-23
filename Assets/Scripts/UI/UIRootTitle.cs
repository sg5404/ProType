using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    private float remainTime = 0f;

    public Button startButton;    //GameScene으로 씬을 옮겨주는 함수

    private void Awake() 
    {
        InitFirst();
    }

    void InitFirst()
    {
        startButton.onClick.AddListener(() => MGScene.Instance.ChangeScene(eSceneName.Game));
    }

  
 //   void Update () 
 //   {
 //       remainTime -= Time.deltaTime;
 //       if(remainTime <= 0f)
 //       {
 //           MGScene.Instance.ChangeScene(eSceneName.Game);
 //       }

 //       if(textWaitSecond)
 //           textWaitSecond.text = ((int)remainTime).ToString(); 
	//}
}
