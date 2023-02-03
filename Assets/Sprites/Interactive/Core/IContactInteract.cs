using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContactInteract
{
    public void OnContact(Collision collision);
    public void OnContact(Collision2D collision);

    public void OnUnContact(Collision collision);
    public void OnUnContact(Collision2D collision);
}
