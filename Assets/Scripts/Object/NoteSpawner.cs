using UnityEngine;

/// <summary>
/// ��Ʈ �� �ٸ� ������Ű�� Ŭ����
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Sync;                      // �� ��ũ (���� ���� ���� �ʿ�)
    public static decimal BitSlice;         // 1 ��Ʈ�� 8 ���
    public float BarInterval = 1f;          // �� ����
    private decimal oneBar;                 // 4 ��Ʈ�� 1 ����
    private decimal nextBar;                // ���� ����
    public GameObject[] Lines;              // ������� ������Ʈ���� �ٴ� ����

    public RhythmStorage storage;           // ������� ������Ʈ�� ��� Ŭ����
    private RhythmManager manager;          // ���� �Ŵ��� ĳ��

    private Vector2 end = new Vector2(0f, -3f);     // ��Ʈ �� ���� ���� ��ǥ

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // �ʱ�ȭ �Լ� ȣ��
        Init();
    }

    private void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;
    }

    /// <summary>
    /// �����ʸ� �ʱ�ȭ �ϴ� �Լ�
    /// </summary>
    public void Init()
    {
        // ���� �ʱ�ȭ
        manager.Init();

        // ������ �� ����
        DataCalculator();

        // ��Ʈ ����
        CreateNote();

        // �� ����
        CreateBar();
    }

    /// <summary>
    /// ��Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void CreateNote()
    {
        // ����
        storage.NoteLoadReset();

        // ��� ���� �ݺ�
        for (int i = 0; i < Lines.Length; i++)
        {
            // �ε��� �����Ϳ� �ش���ο� ��� ��Ʈ Ž��
            foreach (var v in manager.Data.NoteLines[i])
            {
                Note note;

                // ���� ����ҿ��� ���´�
                note = storage.DequeueNote();

                // �����Ϳ� ����ִ� Ÿ������ �ʱ�ȭ
                note.Type = v.Value;

                // ��Ʈ Ÿ�Կ� �´� �̹��� �Ҵ�
                if (note.Type == NoteType.Normal)
                    note.GetComponent<SpriteRenderer>().color = Color.white;

                else if (note.Type == NoteType.Hold)
                    note.GetComponent<SpriteRenderer>().color = Color.yellow;

                // �ش� ��Ʈ�� �ִ� ������ y��ǥ�� �ʱ�ȭ
                end.y = Lines[i].transform.position.x;

                // ��Ʈ �ʱ�ȭ
                note.Init(BitSlice * v.Key, end);

                // ��� ������ �ʱ�ȭ �� �� Ȱ��ȭ
                note.gameObject.SetActive(true);

                // ��Ʈ�� NoteLoad(�����ִ� ��Ʈ ����)�� �߰�
                storage.NoteLoad[i].Enqueue(note);
            }
        }
    }

    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    private void CreateBar()
    {
        // ����
        storage.BarLoadReset();

        // ����
        // 2000���� ���� ����(���Ŀ� �� ���̿� ���� ����� ����)
        for (int i = 0; i < 2000; i++)
        {
            // ��� ���� �ݺ�
            for (int j = 0; j < storage.BarLoad.Length; j++)    
            {
                Bar bar;
                // ���� ����ҿ��� ���´�
                bar = storage.DequeueBar();

                // �ش� ���� �ִ� ������ y��ǥ�� �ʱ�ȭ
                end.y = Lines[j].transform.position.y;

                // ���� �ʱ�ȭ
                bar.Init(nextBar, end);

                // ��� ������ �ʱ�ȭ �� �� Ȱ��ȭ
                bar.gameObject.SetActive(true);

                // ���� BarLoad(�����ִ� ���� ����)�� �߰�
                storage.BarLoad[j].Enqueue(bar);           
            }

            // ���� ���� ���� ��ŭ ���ؼ� ���� ���� ��ġ ǥ��
            nextBar += (oneBar / (decimal)BarInterval);
        }
    }

    /// <summary>
    /// �����͸� ������� ���� �� �����ϴ� �Լ�
    /// </summary>
    private void DataCalculator()
    {
        // 60m / (decimal)data.BPM = 1 ��Ʈ
        // 1 ���� = 4 ��Ʈ
        oneBar = 60m / (decimal)manager.Data.BPM * 4m;
        Sync = manager.Data.Sync;
        nextBar = 0;

        // BitSlice = ��Ʈ / 8 = ���� / 32
        BitSlice = oneBar / 32m;
    }
}
