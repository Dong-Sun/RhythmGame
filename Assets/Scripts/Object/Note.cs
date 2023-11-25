using UnityEngine;

/// <summary>
/// ����
/// </summary>
public enum Judge { NONE = 0, PERFECT, GREAT, GOOD, MISS }

/// <summary>
/// ��Ʈ
/// </summary>
public class Note : MonoBehaviour
{
    public decimal Timing { set { timing = value; } get { return timing; } }    // ���� �ð� ��ȯ
    public NoteType Type;
    private float speed;                                    // �̵� �ӵ�
    private decimal arrive;                                 // ���� �ð�
    private decimal timing;                                 // ���� �ð�
    private Vector2 end;                                    // ���� ��ġ
    private Transform trans;                                // �ڽ��� transform ������ Ȱ���ϱ� ���� ĳ��
    private RhythmManager manager;                          // ���� �Ŵ��� ĳ��

    private void Update()
    {
        // ���� �Ŵ��� ���� ���� �ð��� ���� ��Ʈ Ÿ�̹� �ʱ�ȭ
        // Ÿ�̹� = �����ð� - ����ð�
        timing = arrive - manager.CurrentTime;

        // ��Ʈ ������
        NoteMove();

        // ������ ��Ʈ ���
        NoteDrop();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // transform ĳ�� �ȉ��� �� ����ó��
        if (trans == null)
            trans = GetComponent<Transform>();

        // �����ð� ����
        arrive = arriveTime;

        // ��Ʈ ���� �� timing�� ���� ���� �ذ��� ���� �ʱ�ȭ(timing�� ������ ���ÿ� ����Ǵ� ����)
        timing = 300m;

        // ������ ����
        end = _end;
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    /// <returns>����</returns>
    public Judge SendJudge()
    {
        if (Mathf.Abs((float)timing) > 0.12501f)
            return Judge.NONE;
        else if (Mathf.Abs((float)timing) <= 0.065f)
            return Judge.PERFECT;
        else if (Mathf.Abs((float)timing) <= 0.105f)
            return Judge.GREAT;
        else
            return Judge.GOOD;
    }

    /// <summary>
    /// ��Ʈ �̵�
    /// </summary>
    private void NoteMove()
    {
        // �ӵ� ����ȭ
        speed = manager.Speed;

        // ��Ʈ ��ġ = ������ + (right * timing * speed * ����)
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }

    /// <summary>
    /// ������ ��Ʈ ���
    /// </summary>
    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            gameObject.SetActive(false);
        }
    }
}
