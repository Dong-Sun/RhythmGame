using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ������Ʈ�� ������ ����� Ŭ����
/// </summary>
public class RhythmStorage : MonoBehaviour
{
    public Note NotePrefab;                         // ��Ʈ
    public Bar BarPrefab;                           // ����

    public Queue<Bar> Bars = new Queue<Bar>();          // ���� ������Ʈ Ǯ
    public Queue<Note> Notes = new Queue<Note>();       // ��Ʈ ������Ʈ Ǯ

    public Queue<Bar>[] BarLoad = new Queue<Bar>[2];       // �����ִ� ����
    public Queue<Note>[] NoteLoad = new Queue<Note>[2];    // �����ִ� ��Ʈ

    private RhythmManager manager;                  // ���� �Ŵ��� ĳ��

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            BarLoad[i] = new Queue<Bar>();
            NoteLoad[i] = new Queue<Note>();
        }
    }

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
    }

    private void Update()
    {
        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        ReturnNote();
    }

    public Note DequeueNote()
    {
        Note note;

        // ������Ʈ Ǯ�� ��Ʈ�� ���� (����)
        if (Notes.Count > 0)
            note = Notes.Dequeue();

        // ������Ʈ Ǯ�� ��Ʈ�� �������� ���� (���� ����)
        else
            note = Instantiate(NotePrefab, transform);

        return note;
    }

    public Bar DequeueBar()
    {
        Bar bar;

        // ������Ʈ Ǯ�� ���� ���� (����)
        if (Bars.Count > 0)
            bar = Bars.Dequeue();

        // ������Ʈ Ǯ�� ���� �������� ���� (���� ����)
        else
            bar = Instantiate(BarPrefab, transform);

        return bar;
    }

    /// <summary>
    /// �Է¹��� ������ ��Ʈ�� Ŭ���� �ϴ� �Լ�
    /// </summary>
    public void NoteClear(int line)
    {
        // �ش� ������ ť�� ��� ��Ʈ ���
        Note n = NoteLoad[line].Peek();

        // �ش� ��Ʈ ��Ȱ��ȭ
        n.gameObject.SetActive(false);

        // ��Ʈ�� �ٽ� ������Ʈ Ǯ�� ���
        Notes.Enqueue(NoteLoad[line].Dequeue());
    }

    /// <summary>
    /// �����ִ� ������ ��Ʈ�� �����޴� �Լ�
    /// </summary>
    public void ReturnNote()
    {
        // ��� �ε� Ž��
        foreach (var load in NoteLoad)
        {
            // �ش� �ε忡 �ִ� ��Ʈ�� ������ ��Ʈ�� �����ް� Miss ī����
            if (load.Count > 0 && load.Peek().Timing < -0.12501m)
            {
                Notes.Enqueue(load.Dequeue());
                manager.Judges.Miss++;
            }
        }
    }

    /// <summary>
    /// �����ִ� ��� ��Ʈ���� Ǯ�� �������� �Լ�
    /// </summary>
    public void NoteLoadReset()
    {
        // ��� �ε� Ž��
        foreach (var load in NoteLoad)
        {
            // �ε忡 ���� ��Ʈ�� ������ �� ���� ������Ʈ Ǯ�� �����ֱ�
            while (load.Count > 0)
            {
                Note note = load.Peek();
                note.gameObject.SetActive(false);
                Notes.Enqueue(load.Dequeue());
            }
        }
    }

    /// <summary>
    /// �����ִ� ��� ������� Ǯ�� �������� �Լ�
    /// </summary>
    public void BarLoadReset()
    {
        // ��� �ε� Ž��
        foreach (var load in BarLoad)
        {
            // �ε忡 ���� ��Ʈ�� ������ �� ���� ������Ʈ Ǯ�� �����ֱ�
            while (load.Count > 0)
            {
                Bar bar = load.Peek();
                bar.gameObject.SetActive(false);
                Bars.Enqueue(load.Dequeue());
            }
        }
    }
}
