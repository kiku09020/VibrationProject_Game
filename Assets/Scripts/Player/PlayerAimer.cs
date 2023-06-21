using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary>
	/// プレイヤーの照準関係のクラス
	/// </summary>
    public class PlayerAimer : PlayerBaseComponent {

		[SerializeField, Tooltip("照準移動閾値"), Range(0, 1)]
		float aimThreshold = .6f;
		[SerializeField, Tooltip("照準移動閾値(戻り)"), Range(-1, 1)]
		float aimReturnThreshold = .6f;

		[SerializeField, Tooltip("照準の角度"), Range(0, 90)]
		float aimAngle = 30;

		[SerializeField, Tooltip("照準移動時間"), Range(0, 1)]
		float aimDuration = .5f;


		[SerializeField, Tooltip("照準イージング")]
		Ease aimEasing;

		[SerializeField, Tooltip("現在の方向")]
		Direction currentDirection = Direction.down;

		// 方向
		enum Direction
		{
			up,
			down,
		}

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			Aiming();
		}

		void Aiming()
		{
			if (core.DataReceiver.Gyro.y >= aimThreshold && currentDirection == Direction.down) {
				core.transform.DORotate(new Vector3(-aimAngle, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.up;
			}

			else if (core.DataReceiver.Gyro.y <= aimReturnThreshold && currentDirection == Direction.up){
				core.transform.DORotate(new Vector3(0, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.down;
			}
		}
	}
}