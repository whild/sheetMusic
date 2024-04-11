using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asset\NPCで作ってください。
/// </summary>
[CreateAssetMenu(fileName = "INPCDialog", menuName = "SheetMusic/Create NewI NPC Dialog", order = 10002)]
public class NPCDialogBase : ScriptableObject
{
    /// <summary>
    /// Dialogに使うイメージ
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

}
