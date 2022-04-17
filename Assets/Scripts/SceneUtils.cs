using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneUtils 
{

    public enum SceneId
    {
        Lobby,
        TVLevel
    }

    public static readonly string[] scene = { Names.Lobby, Names.TVLevel };
   public static class Names
    {
        public static readonly string PersistentScene = "PersistentScene";
        public static readonly string TVLevel = "TVLevel";
        public static readonly string Lobby = "Lobby";
    }

    public static void AlignXRRig(Scene persistentScene, Scene currentScene)
    {
        GameObject[] currentObjects = currentScene.GetRootGameObjects();
        GameObject[] persistentObjects = persistentScene.GetRootGameObjects();

        foreach ( var origin in currentObjects)
        {
            if(origin.CompareTag("XRRigOrigin"))
            {
                foreach(var rig in persistentObjects)
                {
                    if(rig.CompareTag("XRRig"))
                    {
                        rig.transform.position = origin.transform.position;
                        rig.transform.rotation = origin.transform.rotation;
                        return;   
                    }
                }
            }
        }
    }
}
