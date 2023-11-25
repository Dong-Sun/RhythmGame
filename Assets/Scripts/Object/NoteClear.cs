using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // ��Ʈ �ڵ� Ŭ����
    public bool FirstAuto;                     // ��Ʈ �ڵ� Ŭ����
    public bool SecondAuto;                     // ��Ʈ �ڵ� Ŭ����
    public AudioSource NoteSound;           // Ű�� �ҷ��� ����� �ҽ�
    public RhythmStorage storage;           // ��Ʈ�� Ŭ�����ϸ� �������� ����� ĳ��
    public AudioSource BgSound;             // ����� �ҷ��� ����� �ҽ�
    [SerializeField] GameObject menu;
    private RhythmManager manager;          // ���� �Ŵ��� ĳ��
    private Judge judge;                    // ���� ����
    private KeyCode[] clearKeys;

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
        KeyMapping();
    }

    public void KeyMapping()
    {
        clearKeys = new KeyCode[4];
        if (manager.ClearKeys.Length > 0)
        {
            for (int i = 0; i < manager.ClearKeys.Length; i++)
            {
                clearKeys[i] = manager.ClearKeys[i];
            }
        }
    }

    /// <summary>
    /// ���� ������ ī��Ʈ ���ִ� �Լ�
    /// </summary>
    private void JudgeCount(Judge judge)
    {
        switch (judge)
        {
            case Judge.PERFECT:
                manager.Judges.Perfect++;
                break;
            case Judge.GREAT:
                manager.Judges.Great++;
                break;
            case Judge.GOOD:
                manager.Judges.Good++;
                break;
            case Judge.MISS:
                manager.Judges.Miss++;
                break;
            default:
                Debug.LogError("�߸��� ���� (Judge)");
                return;
        }
    }

    private bool KeyDownInput(int index)
    {
        // 1 ���� : [0] [1]
        if (index == 0)
        {
            return Input.GetKeyDown(clearKeys[0]) || Input.GetKeyDown(clearKeys[1]);
        }

        // 2 ���� : [2] [3]
        else if (index == 1)
        {
            return Input.GetKeyDown(clearKeys[2]) || Input.GetKeyDown(clearKeys[3]);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        // 1 ���� : [0] [1]
        if (index == 0)
        {
            return Input.GetKey(clearKeys[0]) || Input.GetKey(clearKeys[1]);
        }

        // 2 ���� : [2] [3]
        else if (index == 1)
        {
            return Input.GetKey(clearKeys[2]) || Input.GetKey(clearKeys[3]);
        }
        else
            return false;
    }

    private void Clear(int index)
    {
        // ��Ʈ Ŭ����
        JudgeCount(judge);

        // ��Ʈ ����
        storage.NoteClear(index);

        // Ű�� ���
        NoteSound.PlayOneShot(NoteSound.clip);
    }
}
