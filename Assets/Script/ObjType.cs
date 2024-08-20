using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Object/ObjType", fileName = "Create new object Type")]
public class ObjType : ScriptableObject
{
    public string name;
    public bool isNeedtoReorder;
    public bool isParent;
}
