using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // 노트 자동 클리어
    public bool FirstAuto;                     // 노트 자동 클리어
    public bool SecondAuto;                     // 노트 자동 클리어
    public AudioSource NoteSound;           // 키음 불러올 오디오 소스
    public RhythmStorage storage;           // 노트를 클리어하며 돌려보낼 저장소 캐싱
    public AudioSource BgSound;             // 배경음 불러올 오디오 소스
    [SerializeField] GameObject menu;
    private RhythmManager manager;          // 리듬 매니저 캐싱
    private Judge judge;                    // 판정 정보
    private KeyCode[] clearKeys;

    private void Start()
    {
        // 리듬 매니저 캐싱
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
    /// 받은 판정을 카운트 해주는 함수
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
                Debug.LogError("잘못된 정보 (Judge)");
                return;
        }
    }

    private bool KeyDownInput(int index)
    {
        // 1 라인 : [0] [1]
        if (index == 0)
        {
            return Input.GetKeyDown(clearKeys[0]) || Input.GetKeyDown(clearKeys[1]);
        }

        // 2 라인 : [2] [3]
        else if (index == 1)
        {
            return Input.GetKeyDown(clearKeys[2]) || Input.GetKeyDown(clearKeys[3]);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        // 1 라인 : [0] [1]
        if (index == 0)
        {
            return Input.GetKey(clearKeys[0]) || Input.GetKey(clearKeys[1]);
        }

        // 2 라인 : [2] [3]
        else if (index == 1)
        {
            return Input.GetKey(clearKeys[2]) || Input.GetKey(clearKeys[3]);
        }
        else
            return false;
    }

    private void Clear(int index)
    {
        // 노트 클리어
        JudgeCount(judge);

        // 노트 복귀
        storage.NoteClear(index);

        // 키음 출력
        NoteSound.PlayOneShot(NoteSound.clip);
    }
}
