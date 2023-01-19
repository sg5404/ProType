using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    private float remainTime = 5f;

    public Text textWaitSecond;    

    private void Awake() 
    {
        if(textWaitSecond)
            textWaitSecond.text =  remainTime.ToString();
    }
  
    void Update () 
    {
        remainTime -= Time.deltaTime;
        if(remainTime <= 0f)
        {
            MGScene.Instance.ChangeScene(eSceneName.Game);
        }

        if(textWaitSecond)
            textWaitSecond.text = ((int)remainTime).ToString(); 
	}
}
