using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]



public class UnityChanController : MonoBehaviour {
	private Animator anim;
	
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	public void AnimateForwardMotion(float amount) {
		anim.SetFloat ("Speed", amount);
	}
	
	public void AnimateTurningMotion(float amount) {
		anim.SetFloat ("Direction", amount);
	}
	
	public void BeginJumpAnimation() {
		anim.SetBool("Jump", true);
	}
}
