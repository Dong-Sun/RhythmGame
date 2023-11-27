using UnityEngine;

/// <summary>
/// ���� üũ �� �� Ŭ����
/// </summary>
public class Bar : MonoBehaviour
{
    public decimal Timing { get { return timing; } }    // ���� �ð� ��ȯ

    private float speed;                                // �̵� �ӵ�
    private decimal arrive;                             // ���� �ð�
    private decimal timing;                             // ���� �ð�
    private Vector2 end = new Vector2(0f, -3f);         // ���� ��ġ
    private Transform trans;                            // �ڽ��� transform Ȱ���� ���� ĳ��
    private RhythmManager manager;                      // ���� �Ŵ��� ĳ��
    void Update()
    {
        // Ÿ�̹� = �����ð� - ����ð�
        timing = arrive - manager.CurrentTime;

        // ���� �̵�
        BarMove();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // trasform ĳ�� �ȉ��� ���� ���� �����͸�
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
    /// �� �̵�
    /// </summary>
    private void BarMove()
    {
        // �ӵ� ����ȭ
        speed = manager.Speed;

        // ��Ʈ ��ġ = ������ + (right * timing * speed * ����)
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }
}
