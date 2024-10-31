using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Recorder;

public class CameraBehaviour : MonoBehaviour
{
    private Material skyboxMat;

    private string[] textures;

    private int currentIndex = 0;

    [SerializeField]
    private float cameraSpeed = 30;

    private float totalTime;

    private float elapseTime;

    private static readonly string texDir = "Assets/Textures/";

    // Start is called before the first frame update
    void Start()
    {
        skyboxMat = GetComponent<Skybox>().material;
        string[] texturesUid = AssetDatabase.FindAssets("", new string[] { texDir });
        textures = new string[texturesUid.Length];
        for (int i = 0; i < texturesUid.Length; i++)
        {
            textures[i] = AssetDatabase.GUIDToAssetPath(texturesUid[i]);
        }

        totalTime = 360f / cameraSpeed;

        NextOne();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (textures == null || textures.Length == 0)
        {
            return;
        }

        transform.rotation *= Quaternion.AngleAxis(cameraSpeed * Time.deltaTime, Vector3.up);

        elapseTime += Time.deltaTime;
        if (elapseTime >= totalTime)
        {
            if (currentIndex == textures.Length)
            {
                skyboxMat.mainTexture = null;
                EditorApplication.isPlaying = false;
                return;
            }
            NextOne();
        }
    }

    void NextOne()
    {
        if (textures == null || textures.Length == 0)
        {
            return;
        }
        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(textures[currentIndex]);
        skyboxMat.mainTexture = tex;
        elapseTime = 0;
        currentIndex++;
    }
}
