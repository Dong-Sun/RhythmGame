using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ���õ� �����͸� �����ϴ� �̱��� Ŭ����
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance             // �̱��� �ν��Ͻ�
    {
        get { return instance; }
    }
    public string Title;                            // ���� �� �� ����
    public AudioClip AudioClip;                     // ����� �� Ŭ��
    public decimal CurrentTime;                     // ���� �ð�
    public AudioData Data;                          // �� ������
    public float Speed;                             // �ӵ�
    public float MusicSound;                        // �����
    public float KeySound;                          // Ű��
    public bool SceneChange;                        // �� ��ȯ ����
    public JudgeStorage Judges;                     // ���� �����
    public KeyCode[] ClearKeys;

    public bool IsSelectGuide = true;
    public bool IsRhythmGuide = true;

    private static RhythmManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Judges = new JudgeStorage();
        Data = new AudioData();
        MusicSound = 1f;
        KeySound = 1f;
        ClearKeys[0] = KeyCode.A;
        ClearKeys[1] = KeyCode.S;
        ClearKeys[2] = KeyCode.Semicolon;
        ClearKeys[3] = KeyCode.Quote;
    }

    private void Update()
    {
        Judges.SetAttractive();

        // ������ ������ ���
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
    }

    /// <summary>
    /// �� �����͸� Json ���Ϸ� ����
    /// </summary>
    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    /// <summary>
    /// Json ������ �� ������ �ҷ�����
    /// </summary>
    public void LoadData()
    {
        Data = new AudioData(Title);
    }

    /// <summary>
    /// ������� ���� �� ������ �ʱ�ȭ�� ���� �Լ�
    /// </summary>
    public void Init()
    {
        // ���� ���� �ִ� Title�� ���� Json�����͸� �ҷ���
        LoadData();

        // ���� �ð� �ʱ�ȭ
        CurrentTime = 0;

        // ���� �ʱ�ȭ
        Judges.Init();

        // �� ��ȯ ���� �ʱ�ȭ
        SceneChange = false;
    }

    /// <summary>
    /// ���� ��ȯ�ϸ� �ŷµ��� �����ϴ� �Լ�
    /// </summary>
    public void EndScene()
    {
        // �� ��ȯ
        SceneChange = true;
    }
}
