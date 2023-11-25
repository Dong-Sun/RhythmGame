using UnityEngine;

public class VolumeSync : MonoBehaviour
{
    private AudioSource source;     // ���� ����� ���� ����� �ҽ� ĳ��
    private RhythmManager manager;  // �Ŵ��� ĳ��

    // Start is called before the first frame update
    private void Start()
    {
        // ����� �ҽ� ĳ��
        source = GetComponent<AudioSource>();

        // �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
    }

    private void Update()
    {
        // ������ �Ŵ��� ������ ����ȭ
        source.volume = manager.MusicSound;
    }
}
