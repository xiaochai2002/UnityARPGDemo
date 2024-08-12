using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ܹ����� ��Ҫ�ṩ��������
/// </summary>
public class EncryptionUtil
{
    //1.��ȡ�����Կ
    public static int GetRandomKey()
    {
        return Random.Range(1, 10000) + 5;
    }

    //2.��������
    public static int LockValue(int value, int key)
    {
        //��Ҫ����������
        value = value ^ (key % 9);
        value = value ^ 0xADAD;
        value = value ^ (1 << 5);
        value += key;
        return value;
    }

    public static long LockValue(long value, int key)
    {
        //��Ҫ����������
        value = value ^ (key % 9);
        value = value ^ 0xADAD;
        value = value ^ (1 << 5);
        value += key;
        return value;
    }

    //3.��������
    public static int UnLoackValue(int value, int key)
    {
        //�п��ܻ�û�м��ܹ� û�г�ʼ���������� ֱ����Ҫ��ȡ ��ô�Ͳ��ý�����
        //����ʱ����ֵ�϶���0
        if (value == 0)
            return value;
        value -= key;
        value = value ^ (key % 9);
        value = value ^ 0xADAD;
        value = value ^ (1 << 5);
        return value;
    }

    public static long UnLoackValue(long value, int key)
    {
        //�п��ܻ�û�м��ܹ� û�г�ʼ���������� ֱ����Ҫ��ȡ ��ô�Ͳ��ý�����
        //����ʱ����ֵ�϶���0
        if (value == 0)
            return value;
        value -= key;
        value = value ^ (key % 9);
        value = value ^ 0xADAD;
        value = value ^ (1 << 5);
        return value;
    }
}
