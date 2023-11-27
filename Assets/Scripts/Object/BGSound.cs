using UnityEngine;

/// <summary>
/// ��������� ��� �뷡 ��°� ���õ� Ŭ����
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BGSound : MonoBehaviour
{
    public float Timer = 2f;        // ���� ������
    public bool IsReWind = false;
    private float timer;            // Ÿ�̸� ������ ���� ����
    private AudioSource source;     // ���� ����� ���� ����� �ҽ� ĳ��
    private RhythmManager manager;  // �Ŵ��� ĳ��
    private float myDelay = 0;

    private void Awake()
    {
        // ����� �ҽ� ĳ��
        source = GetComponent<AudioSource>();

        // Ÿ�̸� �ʱ�ȭ
        timer = Timer;
    }
    private void Start()
    {
        // �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // �뷡�� �Ŵ����� �ִ� Ŭ������ ����
        source.clip = manager.AudioClip;
    }

    private void Update()
    {
        // ������ �Ŵ��� ������ ����ȭ
        source.volume = manager.MusicSound;

        // ���� �����̰� ���� ������(���� x)
        if (timer >= 0f)
        {
            if (timer < myDelay)
            {
                timer += Time.deltaTime;
                if (timer >= myDelay)
                    myDelay = -999f;
            }
            else
            {
                if (!manager.IsRhythmGuide || timer > 1f)
                    // Ÿ�̸Ӹ� �ð���ŭ ���ֱ�
                    timer -= Time.deltaTime;
            }

            if (!source.isPlaying && timer < 0f)
            {
                source.Play();
                manager.CurrentTime = (decimal)source.time;
                IsReWind = false;
            }
            else
            {
                // �Ŵ����� ����ð��� Ÿ�̸� ������ �����
                manager.CurrentTime = (decimal)source.time - (decimal)timer;
            }
        }
        else
        {
            // �뷡 ��� �ð� ����ȭ
            manager.CurrentTime = (decimal)source.time;
        }
    }
    public void RePlay(float delay)
    {
        timer = 0;
        myDelay = delay;
        IsReWind = true;
    }
}
