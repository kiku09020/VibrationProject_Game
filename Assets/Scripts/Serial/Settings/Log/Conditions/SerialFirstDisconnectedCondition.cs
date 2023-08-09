using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialFirstDisconnectedCondition : SerialSettingCondition {
	public override void CheckCondition()
	{
		Condition = SerialSelector.IsDisconnect_First;        // ポート名0個だったら未接続
	}
}
