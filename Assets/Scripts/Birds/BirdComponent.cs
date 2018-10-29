using System;
using UnityEngine;

namespace Birder.Birds {
  public class BirdComponent : MonoBehaviour {

    static int idleAnimationHash = Animator.StringToHash("Base Layer.Idle");
    static int flyAnimationHash = Animator.StringToHash ("Base Layer.fly");
    static int hopIntHash = Animator.StringToHash ("hop");
    static int flyingBoolHash = Animator.StringToHash("flying");
    static int peckBoolHash = Animator.StringToHash("peck");
    static int ruffleBoolHash = Animator.StringToHash("ruffle");
    static int preenBoolHash = Animator.StringToHash("preen");
    static int landingBoolHash = Animator.StringToHash("landing");
    static int singTriggerHash = Animator.StringToHash ("sing");
    static int flyingDirectionHash = Animator.StringToHash("flyingDirectionX");
    static int dieTriggerHash = Animator.StringToHash ("die");

    Animator _animator;
    BirdActionSelector _actionSelector;
    BirdState _birdState;
    bool _idle;

    void Start() {
      _animator = this.gameObject.GetComponent<Animator>();
      _actionSelector = new BirdActionSelector();
      _birdState = BirdState.Grounded;
    }

    void Update() {
      int currentAnimationHash = _animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
      bool inTransition = _animator.IsInTransition(0);
      bool idle = currentAnimationHash == idleAnimationHash;
      if (idle && !inTransition) {
        BirdAction nextAction = _actionSelector.NextAction(_birdState);
        TriggerAction(nextAction.Action);
      }
    }

    void TriggerAction(BirdActionEnum action) {
      Debug.Log("Trigger" + Enum.GetName(typeof(BirdActionEnum), action));
      switch (action) {
        case BirdActionEnum.Sing:
          _animator.SetTrigger(singTriggerHash);
          break;
        case BirdActionEnum.Preen:
          _animator.SetTrigger(preenBoolHash);
          break;
        case BirdActionEnum.Ruffle:
          _animator.SetTrigger(ruffleBoolHash);
          break;
        case BirdActionEnum.Peck:
          _animator.SetTrigger(peckBoolHash);
          break;
        case BirdActionEnum.HopForward:
          _animator.SetInteger(hopIntHash, 1);
          break;
        case BirdActionEnum.HopBackward:
          _animator.SetInteger(hopIntHash, -2);
          break;
        case BirdActionEnum.HopLeft:
          _animator.SetInteger(hopIntHash, -1);
          break;
        case BirdActionEnum.HopRight:
          _animator.SetInteger(hopIntHash, 2);
          break;
        case BirdActionEnum.Fly:
          // TODO
          _animator.SetTrigger(singTriggerHash);
          break;
      }
    }

    void ResetHopInt(){
      _animator.SetInteger(hopIntHash, 0);
    }

    void PlaySong() {

    }
  }
}
