using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClearKeyInput : MonoBehaviour
{
    public Text[] KeyInput;
    public Image[] Select;
    public UnityEvent Refresh;
    private RhythmManager manager;
    private KeyCode[] clearKeys;
    private bool isEdit;
    private int index;
    private Dictionary<char, bool> keyMap = new Dictionary<char, bool>();

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (!isEdit)
            return;

        if (Input.anyKey)
        {
            Next();
            Refresh.Invoke();
        }
    }

    public void Edit()
    {
        index = 0;
        isEdit = true;
        foreach (var i in Select)
        {
            i.gameObject.SetActive(false);
        }
        Select[index].gameObject.SetActive(true);
    }

    private void Next()
    {
        string str = Input.inputString;        

        // �߸��� �Է�
        if (str.Length <= 0)
            return;

        // ���� ���� Ű ����
        char newKey = str[0];
        newKey = char.ToLower(newKey);

        // ���� Ű ����
        char oldKey = (char)clearKeys[index];

        // �������� �ִ� Ű�� ���
        if (keyMap.ContainsKey(newKey))
        {
            // ���ο� Ű�� �̹� �ߺ��� ���
            if (keyMap[newKey])
            {
                // �Ҵ� ���� ��ġ ã��
                for(int i = 0; i < clearKeys.Length; i++)
                {
                    if ((char)clearKeys[i] == newKey)
                    {
                        // �߰� �� ���� Ű�� ����
                        clearKeys[i] = (KeyCode)oldKey;
                        manager.ClearKeys[i] = clearKeys[i];
                        KeyInput[i].text = char.ToUpper(oldKey).ToString();
                        break;
                    }
                }
            }

            // �ߺ����� ���� ���
            else
            {
                keyMap[oldKey] = false;
                keyMap[newKey] = true;
            }
        }
        // ���ο� Ű �Է� ��
        else
        {
            keyMap[oldKey] = false;
            keyMap.Add(newKey, true);
        }

        clearKeys[index] = (KeyCode)newKey;
        manager.ClearKeys[index] = clearKeys[index];
        KeyInput[index].text = char.ToUpper(newKey).ToString();
        Select[index].gameObject.SetActive(false);

        // ���� �ڸ��� �̵�
        index++;
        if(index >= clearKeys.Length)
        {
            isEdit = false;
            return;
        }

        Select[index].gameObject.SetActive(true);
    }

    private void Init()
    {
        if(manager == null)
            manager = RhythmManager.Instance;
        clearKeys = new KeyCode[4];
        index = 0;
        isEdit = false;
        keyMap.Clear();
        for (int i = 0; i < manager.ClearKeys.Length; i++)
        {
            clearKeys[i] = manager.ClearKeys[i];
            char key = (char)clearKeys[i];
            keyMap.Add(key, true);
            key = char.ToUpper(key);
            KeyInput[i].text = key.ToString();
        }
        foreach(var i in Select)
        {
            i.gameObject.SetActive(false);
        }
    }
}