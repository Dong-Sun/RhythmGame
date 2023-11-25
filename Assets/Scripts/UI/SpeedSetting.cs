using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ӿ� ���Ǵ� ������ �������ִ� UI Ŭ����
/// </summary>
public class SpeedSetting : MonoBehaviour
{
    public Slider SpeedSlider;

    private RhythmManager manager;          // ���� �Ŵ��� ĳ��

    private float CurrentSpeed;
    private void Start()
    {
        SpeedSlider.onValueChanged.AddListener(SetSpeed);
    }

    private void OnEnable()
    {
        if (manager == null)
            manager = RhythmManager.Instance;
        SpeedSlider.value = manager.Speed / 5f;
        CurrentSpeed = manager.Speed;
    }

    private void Update()
    {
        if (CurrentSpeed == manager.Speed)
            return;

        SpeedSlider.value = manager.Speed / 5f;
        CurrentSpeed = manager.Speed;
    }

    /// <summary>
    /// Slider�� �ӵ��� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�����̴� ��</param>
    public void SetSpeed(float volume)
    {
        float v = Mathf.Round(volume * 50);
        manager.Speed = v / 10;
    }
}
