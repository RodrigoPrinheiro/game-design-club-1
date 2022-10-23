using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Poem")]
public class PoemGenerator : ScriptableObject
{
    [SerializeField]
    public Poem poem;
    public string FirstLine => poem.content.Split("\n")[0];
    public Poem SetNewPoem()
    {
        poem = GetPoems().poems[0];
        return poem;
    }

    public JsonPoemData GetPoems()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://www.poemist.com/api/v1/randompoems");
        var op = request.SendWebRequest();

        op.completed += (x) => Debug.Log(request.downloadHandler.text);
        while (!op.isDone)
        {

        }

        return JsonUtility.FromJson<JsonPoemData>("{\"poems\":" + request.downloadHandler.text + "}");
    }
}

[System.Serializable]
public class JsonPoemData
{
    public Poem[] poems;
}

[System.Serializable]
public class Poem
{
    public string title;
    [TextArea] public string content;
    public string url;
    public Poet poet;
}

[System.Serializable]
public struct Poet
{
    public string name;
    public string url;
}
