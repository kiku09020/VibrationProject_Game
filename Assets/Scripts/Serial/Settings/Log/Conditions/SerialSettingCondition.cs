using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シリアル設定ログの条件チェックなど
public abstract class SerialSettingCondition : MonoBehaviour
{
    /// <summary> 条件 </summary>
    public bool Condition { get; protected set; }

    //--------------------------------------------------
    /// <summary> 条件チェック </summary>
    public abstract void CheckCondition();
}






