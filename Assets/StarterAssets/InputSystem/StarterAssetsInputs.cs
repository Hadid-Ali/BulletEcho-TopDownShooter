using CnControls;
using UnityEngine;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public bool jump;
		public bool sprint;

		public Vector2 MoveInput => new Vector2(CnInputManager.GetAxis("Horizontal"),CnInputManager.GetAxis("Vertical"));
		public Vector2 LookInput => new Vector2(CnInputManager.GetAxis("CamX"),CnInputManager.GetAxis("CamY"));

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
		//	SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}