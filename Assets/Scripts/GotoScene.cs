using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoScene : MonoBehaviour
{

  public SceneUtils.SceneId nextScene = SceneUtils.SceneId.Lobby;

    public void Go()
    {
        SceneLoader.Instance.LoadScene(SceneUtils.scene[(int)nextScene]);
    }

}
