using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���൵�� ���� ������ UI ǥ�� Ŭ����
/// </summary>
public class RhythmProgress : MonoBehaviour
{
    public Image Front;                 // ���� �����
    public Image Back;                  // ���� �ĸ��
    private RhythmManager manager;      // ���� �Ŵ��� ĳ��

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        // ���� �ð��� ������ ��� ����� ���� ������
        if((float)manager.CurrentTime <= 0f)
        {
            Front.fillAmount = (2 + (float)manager.CurrentTime) / 2;
        }

        // ���� �ð��� ����� ��� �ĸ�� ���� ������
        else
        {
            if(Front.fillAmount < 1f)
                Front.fillAmount = 1f;
            Back.fillAmount = (float)manager.CurrentTime / manager.Data.Length;
        }
    }
}
