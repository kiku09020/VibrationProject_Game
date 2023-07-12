using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialFirstDisconnectedCondition : SerialSettingCondition {
	public override void CheckCondition()
	{
		Condition = SerialSelector.IsDisconnect_First;        // ƒ|[ƒg–¼0ŒÂ‚¾‚Á‚½‚ç–¢Ú‘±
	}
}
