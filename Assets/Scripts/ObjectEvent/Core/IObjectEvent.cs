/// <summary>
/// ��ܬ�ʫ��٫�Ȫ����뫪�֫�������
/// </summary>
public interface IObjectEvent
{
    public void Event();
    public void Event(float val);

    public int GetSyncroIndex();
}
