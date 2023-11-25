/// <summary>
/// ��Ȯ�� ������ ���� ������ ��� Ŭ����
/// </summary>
public class JudgeStorage
{
    public float Accuracy;      // ��Ȯ��
    public int Attractive;      // �ŷµ�
    public int Perfect;         // 100%
    public int Great;           // 70%
    public int Good;            // 50%
    public int Miss;            // 0%

    public JudgeStorage()
    {
        Init();
    }

    /// <summary>
    /// ������ �����ϴ� �ʱ�ȭ �Լ�
    /// </summary>
    public void Init()
    {
        Accuracy = 0;
        Attractive = 0;
        Perfect = 0;
        Great = 0;
        Good = 0;
        Miss = 0;
    }

    /// <summary>
    /// ��Ȯ���� �����ϰ� �׿� �´� �ŷµ��� �����ϴ� �Լ�
    /// </summary>
    public void SetAttractive()
    {
        // ��Ȯ�� = ��Ȯ�� ������ �������� �� / ��ü �������� ��
        // Perfect = 1, Great = 0.7, Good = 0.5, Miss = 0
        // ��ü ���� = Perfect + Great + Good + Miss

        // ������ ��Ʈ�� �ϳ��� ���� ��
        if (Perfect + Great + Good + Miss > 0)
            Accuracy = (float)(Perfect + Great * 0.8f + Good * 0.6f) / (Perfect + Great + Good + Miss) * 100f;
        // �ϳ��� ���� ��
        else
            Accuracy = 100;
    }
}
