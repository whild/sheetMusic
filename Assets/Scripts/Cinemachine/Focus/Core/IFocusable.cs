using Cinemachine;

public interface IFocusable
{
    /// <summary>
    /// Objectを照らす時の実装
    /// </summary>
    public void FocusEffect(CinemachineTargetGroup group = null);
}
