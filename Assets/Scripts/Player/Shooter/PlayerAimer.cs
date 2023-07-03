using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	/// <summary>
	/// プレイヤーの照準関係のクラス
	/// </summary>
    public class PlayerAimer : PlayerBaseComponent {

		[Header("Controll")]
		[SerializeField, Tooltip("照準上移動閾値"), Range(0, 1)]
		float aimThresholdUp = .6f;
		[SerializeField, Tooltip("照準下移動閾値"), Range(-.5f, .5f)]
		float aimThresholdDown = .6f;

		[Header("CheckX")]
		[SerializeField, Tooltip("左右移動の閾値チェックするか。" )]
		bool checkAimThresholdX = true;

		[SerializeField, Tooltip("移動X閾値。この値以下のときのみ、上下照準移動する"), Range(0, 1)]
		float movingThresholdX = .5f;

		[Header("Aiming")]
		[SerializeField, Tooltip("照準の角度"), Range(0, 90)]
		float aimAngle = 30;
		[SerializeField, Tooltip("照準移動時間"), Range(0, 1)]
		float aimDuration = .5f;

		[SerializeField, Tooltip("照準イージング")]
		Ease aimEasing;

		[Header("Direction")]
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
			if (CheckAimMovable(Direction.up)) {
				core.transform.DORotate(new Vector3(-aimAngle, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.up;
			}

			else if (CheckAimMovable(Direction.down)) {
				core.transform.DORotate(new Vector3(0, 0, 0), aimDuration)
					.SetEase(aimEasing);

				currentDirection = Direction.down;
			}
		}

		// 照準移動可能かを確認する
		bool CheckAimMovable(Direction checkDirection)
		{
			switch(checkDirection) {
				case Direction.up:
					if (core.Controller.ActiveController.IsUp) {
						break;
					}
					return false;

					case Direction.down:
					if (core.Controller.ActiveController.IsDown) {
						break;
					}

					return false;
			}

			// 傾きのXが移動閾値以下か かつ 方向が現在の方向ではないとき
			// ※左右に傾けながら上下に照準移動しないようにするための判定です
			if ((Mathf.Abs(core.DataReceiver.Gyro.x) <= movingThresholdX) || !checkAimThresholdX) {
				if (currentDirection != checkDirection) {
					return true;
				}
			}
			
			return false;
		}
	}
}