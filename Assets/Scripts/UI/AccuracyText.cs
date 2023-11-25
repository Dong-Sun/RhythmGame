using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� ������ִ� Ŭ����
/// </summary>
public class AccuracyText : MonoBehaviour
{
    private Text accuracy;              // ����� �ؽ�Ʈ
    private RhythmManager manager;      // ���� �Ŵ��� ĳ��

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // �ڱ� �ڽ��� Text ������Ʈ
        accuracy = GetComponent<Text>();
    }

    void Update()
    {
        // ��Ȯ���� �ؽ�Ʈ�� ��ȯ
        accuracy.text = manager.Judges.Accuracy.ToString("00.0") + "%";
    }
}
