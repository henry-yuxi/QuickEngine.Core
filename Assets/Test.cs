using QuickEngine.Extensions;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.Log(System.DateTime.Now.ToTimeStamp());
            Debug.Log(System.DateTime.Now.ToTimeStamp(false));
            Debug.Log("1538164861".ToDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.Log(((long)1538164861).ToDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.Log(((long)1538164417208).ToDateTime(false).ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}