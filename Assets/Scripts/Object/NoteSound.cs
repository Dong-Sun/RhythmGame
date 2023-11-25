using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSound : MonoBehaviour
{
    private AudioSource source;     // ���� ����� ���� ����� �ҽ� ĳ��
    private RhythmManager manager;  // �Ŵ��� ĳ��
    // Start is called before the first frame update
    void Start()
    {
        // ����� �ҽ� ĳ��
        source = GetComponent<AudioSource>();
        manager = RhythmManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = manager.KeySound;
    }
}
