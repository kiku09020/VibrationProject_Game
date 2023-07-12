using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHitChecker : ObjectComponentBase<PlayerCore>
    {

		//--------------------------------------------------

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Enemy") {

			}
		}
	}
}
