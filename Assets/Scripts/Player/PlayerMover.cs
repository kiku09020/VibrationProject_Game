using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Player {
    public class PlayerMover : PlayerBaseComponent {

		/* Fields */
		[Header("Controll")]
		[SerializeField, Tooltip("移動閾値"), Range(0, 1)]
		float movingThreshold = .6f;

		[SerializeField, Tooltip("移動クールタイム"), Range(0, 1)]
		float movingCoolTimeLimit = .5f;

		float movingCoolTimer;      // クールタイム用タイマー

		[SerializeField, Tooltip("ずっと傾けている状態でも動かすか")]
		bool continueTilted;

		bool isTilted;				// デバイスが傾けられているか

		[Header("SideMoving")]
		[SerializeField,Tooltip("現在プレイヤーがいる道")]
		Roads currentRoad = Roads.middle;

		[SerializeField, Tooltip("左右移動距離"), Range(0, 10)]
		float movingSideDistance = .5f;

		[SerializeField, Tooltip("移動時間"), Range(0, 3)]
		float movingDuration = .25f;

		[SerializeField, Tooltip("左右移動イージング")]
		Ease movingSideEase;

		[Header("Forward")]
		[SerializeField, Tooltip("前方移動距離"), Range(0, 1)] 
		float movingForwardDistance = .1f;


		/* Properties */
		/// <summary>
		/// 移動可能か
		/// </summary>
		public bool IsSideMovable { get; private set; }

		/* Other */
		// 道の列挙
		enum Roads {
			left,
			middle,
			right,
		}

		//--------------------------------------------------

		protected override void OnUpdate()
		{
			SetSideMovable();

			MoveForward();
			MoveSide();
		}

		// 前進
		void MoveForward()
		{
			transform.position += new Vector3(0, 0, movingForwardDistance);
		}

		// 横移動
		void MoveSide()
		{
			if(IsSideMovable) {
				// 左方向
				if (core.Controller.ActiveController.IsLeft) {

					// 左の道以外だったら、現在の道を変更する
					if (currentRoad != Roads.left) {
						core.transform.DOLocalMoveX(-movingSideDistance, movingDuration)
							.SetEase(movingSideEase)
							.SetRelative();
						currentRoad--;
					}
				}

				// 右方向
				else if (core.Controller.ActiveController.IsRight) {

					// 右の道以外だったら、現在の道を変更する
					if(currentRoad != Roads.right) {
						core.transform.DOLocalMoveX(movingSideDistance, movingDuration)
							.SetEase(movingSideEase)
							.SetRelative();

						currentRoad++;
					}
				}
			}
		}

		// 左右移動できるかどうかを確認する
		void SetSideMovable()
		{
			movingCoolTimer += Time.deltaTime;     // タイマー加算

			IsSideMovable = false;

			// クールタイム超えたか
			if (movingCoolTimer >= movingCoolTimeLimit) {
				// 閾値以上
				if (core.Controller.ActiveController.IsAxisX) {
					if (!isTilted || continueTilted) {
						IsSideMovable = true;                   // 動けるようにする
						isTilted = true;

						movingCoolTimer = 0;            // タイマーリセット
					}
				}

				else {
					isTilted = false;
				}
			}
		}
	}
}