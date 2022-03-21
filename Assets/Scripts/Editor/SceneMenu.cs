using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;


public class SceneMenu : MonoBehaviour
{

    [MenuItem("Scenes/Lobby")]
    static void OpenLobby()
    {
        OpenScene(SceneUtils.Names.Lobby);
    }

    [MenuItem("Scenes/Maze")]
    static void OpenMaze()
    {
        OpenScene(SceneUtils.Names.Maze);
    }

    [MenuItem("Scenes/ComplexInteractions")]
    static void OpenComplexInteractions()
    {
        OpenScene(SceneUtils.Names.ComplexInteractions);
    }

    [MenuItem("Scenes/SecretTree")]
    static void OpenSecretTree()
    {
        OpenScene(SceneUtils.Names.SecretTree);
    }

    static void OpenScene(string name)
    {
        Scene persistentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + SceneUtils.Names.PersistentScene + ".unity", OpenSceneMode.Single);
        Scene currentScene = EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity", OpenSceneMode.Additive);
        SceneUtils.AlignXRRig(persistentScene, currentScene);

       
    }

}
