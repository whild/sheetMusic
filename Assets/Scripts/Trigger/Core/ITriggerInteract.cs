using UnityEngine;

public interface ITriggerInteract
{
    public void OnTrigger(Collider collision);
    public void OnTrigger(Collider2D collision);

    public void OnUnTrigger(Collider collision);
    public void OnUnTrigger(Collider2D collision);
}
