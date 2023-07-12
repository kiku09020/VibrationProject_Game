using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialDisconnectedCondition : SerialSettingCondition {
	public override void CheckCondition()
	{
		Condition = SerialSelector.IsDisconnected;
	}
}
