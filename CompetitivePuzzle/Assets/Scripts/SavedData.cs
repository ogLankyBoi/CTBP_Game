using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public int difficulty;
    public int timer;
    public int appColor;
    public int appMusic;
    public string playerName;

    private static SavedData savedDataInstance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (savedDataInstance == null)
        {
            savedDataInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
