using UnityEngine;

public sealed class GameManager : MonoBehaviour //design pattern singleton
{
    private static GameManager instance;
    public static GameManager GetInstance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

    public int HP = 1000; // Gán giá trị ban đầu cho biến HP ở đây
    private static int count = 0;
    private GameManager()
    {
        count += 1;
        Debug.Log("Game Manager is created " + count);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
