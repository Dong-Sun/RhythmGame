using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ʈ ���� ���� Ŭ����
/// </summary>
public class NoteEditor : MonoBehaviour
{
    public Button FrontButton;      // �ǰ��� ��ư
    public Button BackButton;       // ���� ��ư
    public AudioSource BgSound;     // ��� ����
    public Transform Line1;         // ��Ʈ�� �ٴ� ���� 1
    public Transform Line2;         // ��Ʈ�� �ٴ� ���� 2

    private RhythmManager manager;  // ��1��Ŵ��� ĳ��
    private decimal calculator;     // ��Ʈ �迭 �ε��� ����

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        // ������ ������� �ƴϸ� ������ �۵�x
        if (manager.CurrentTime < 0m)
            return;

        // ��Ʈ �߰�
        AddNote();

        // ��Ʈ ����
        RemoveNote();

        // ����Ű�� ���� ����
        Wind();

        // ���� ���
        SetPitch();

        // �����̽��ٸ� ���� ���� ��� �� �Ͻ�����
        Playing();

        // ���콺 �Է��� ���� ��Ʈ ���� �� ����
        MouseInput();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="second">���� �ð�</param>
    public void Front(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time - second, 0f, BgSound.clip.length);
    }

    /// <summary>
    /// �ڷ� ����
    /// </summary>
    /// <param name="second">���� �ð�</param>
    public void Back(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time + second, 0f, (int)BgSound.clip.length);
    }

    /// <summary>
    /// ���� ������¿� ���� �÷��� �Լ�
    /// </summary>
    public void Play()
    {
        // ���� �������� ������ �÷��� ��
        if (!BgSound.isPlaying)
            BgSound.Play();
    }

    /// <summary>
    /// ���� �Ͻ�����
    /// </summary>
    public void Pause()
    {
        BgSound.Pause();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Stop()
    {
        BgSound.time = 0;
        BgSound.Stop();
    }

    /// <summary>
    /// ��Ʈ �߰�
    /// </summary>
    private void AddNote()
    {
        // 1 ����
        // Q = �Ϲ� ��Ʈ ����, W = Ȧ�� ��Ʈ ����
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.W))
        {
            // �ε��� = ����ð� / �ּҴ��� ��Ʈ
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // �ݿø��� ���� ���� ������ �ε����� �ʱ�ȭ
            calculator = decimal.Round(calculator);

            // �ش� Ű�� ��Ī�� �����Ͱ� ������ ���� ����
            if (!manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                // �Է¹��� Ű�� ���� ��Ʈ Ÿ�� ���� �� �߰�
                manager.Data.NoteLines[0].Add((int)calculator, (Input.GetKeyDown(KeyCode.Q)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }

        // 2 ����
        // O = �Ϲ� ��Ʈ ����, P = Ȧ�� ��Ʈ ����
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKey(KeyCode.P))
        {
            // �ε��� = ����ð� / �ּҴ��� ��Ʈ
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // �ݿø��� ���� ���� ������ �ε����� �ʱ�ȭ
            calculator = decimal.Round(calculator);

            // �ش� Ű�� ��Ī�� �����Ͱ� ������ ���� ����
            if (!manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                // �Է¹��� Ű�� ���� ��Ʈ Ÿ�� ���� �� �߰�
                manager.Data.NoteLines[1].Add((int)calculator, (Input.GetKeyDown(KeyCode.O)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }
    }

    /// <summary>
    /// ��Ʈ ����
    /// </summary>
    private void RemoveNote()
    {
        // 1 ����
        // LeftAlt = ����
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // �ε��� = ����ð� / �ּҴ��� ��Ʈ
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // �ݿø��� ���� ���� ������ �ε����� �ʱ�ȭ
            calculator = decimal.Round(calculator);

            // �ش� Ű�� ��Ī�� �����Ͱ� ������ ������ ����
            if (manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[0].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }

        // 2 ����
        // M = ����(�ӽ� ����)
        if (Input.GetKey(KeyCode.M))
        {
            // �ε��� = ����ð� / �ּҴ��� ��Ʈ
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;

            // �ݿø��� ���� ���� ������ �ε����� �ʱ�ȭ
            calculator = decimal.Round(calculator);

            // �ش� Ű�� ��Ī�� �����Ͱ� ������ ������ ����
            if (manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[1].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
    }

    /// <summary>
    /// ����Ű �Է��� ���� ���� ����
    /// </summary>
    private void Wind()
    {
        // LeftArrow = ������ ����
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontButton.onClick.Invoke();
        }

        // RightArrow = �ڷ� ����
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BackButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// ���� ���
    /// </summary>
    private void SetPitch()
    {
        // UpArrow = ��� ����
        if (Input.GetKey(KeyCode.UpArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch + 0.01f, 0f, 2f);
        }

        // DownArrow = ��� ����
        if (Input.GetKey(KeyCode.DownArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch - 0.01f, 0f, 2f);
        }

        // Backspace = ��� �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BgSound.pitch = 1;
        }
    }

    /// <summary>
    /// �����̽��ٸ� ���� ���� ��� �� �Ͻ������� �ϴ� �Լ�
    /// </summary>
    private void Playing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ��� ���¿� ���� ��� �Ͻ����� ���� ����
            if (BgSound.isPlaying)
                Pause();
            else
                Play();
        }
    }

    /// <summary>
    /// ���콺 �Է��� ���� ��Ʈ ������ ����� ������ �Լ�
    /// </summary>
    private void MouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            // ȭ����� ���콺 ��ǥ �Է�
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int line;
            // ���콺�� y��ǥ�� ���� line ����(���� �Ұ� �� �Լ� ����)
            if (Mathf.Abs(Line1.position.y - vector.y) < 0.4f)
                line = 0;
            else if (Mathf.Abs(Line2.position.y - vector.y) < 0.4f)
                line = 1;
            else
                return;

            // Bar�� ��ġ�� ��� ���� ����
            // ��Ʈ ��ġ = ������ + (right * timing * speed * ����)
            // => timing = (��Ʈ ��ġ - ������) / (right * speed * ����)
            // right = 1, ���� = 5
            //��Ʈ ��ġ = ���콺 ��ġ�� ��ȯ�� ���콺 ��ǥ�� ���� Ÿ�̹� ��ȯ
            // timing = (���콺 ��ġ - ������) / (Speed * 5)
            float timing = (vector.x + 8) / (manager.Speed * 5f);

            // timing / BitSlice = ���� ��Ʈ �� ���� ���콺 ��ġ�� �ε���
            // CurrentTime / BitSlice = ������ ��Ʈ���� ������ �ε���
            // timing / BitSlice + CurrentTime / BitSlice = ���콺�� ��ġ�� ��ǥ�� ���� �ε���
            // ������������ ���� calculator = (timing + CurretTime) / BitSlice
            calculator = ((decimal)timing + manager.CurrentTime) / NoteSpawner.BitSlice;

            // �ݿø��� ���� ���� ������ �ε����� �ʱ�ȭ
            calculator = decimal.Round(calculator);

            // ��Ī�� Ű�� ������ ��Ʈ �߰�
            if (!manager.Data.NoteLines[line].ContainsKey((int)calculator))
            {
                // ��Ŭ�� = �Ϲ� ��Ʈ, ��Ŭ�� = Ȧ�� ��Ʈ
                manager.Data.NoteLines[line].Add((int)calculator, (Input.GetKeyDown(KeyCode.Mouse0)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }

            // ��Ī�� Ű�� ������ ��Ʈ ����
            else
            {
                manager.Data.NoteLines[line].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
    }
}