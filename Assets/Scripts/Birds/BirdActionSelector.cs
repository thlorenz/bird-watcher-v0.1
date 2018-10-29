using System;

namespace Birder.Birds {
  class LikelyBirdAction {
    protected virtual Int16 Sing        { get { return 5; } }
    protected virtual Int16 Preen       { get { return 4; } }
    protected virtual Int16 Ruffle      { get { return 4; } }
    protected virtual Int16 Peck        { get { return 4; } }
    protected virtual Int16 HopForward  { get { return 5; } }
    protected virtual Int16 HopBackward { get { return 2; } }
    protected virtual Int16 HopLeft     { get { return 3; } }
    protected virtual Int16 HopRight    { get { return 3; } }
    protected virtual Int16 Fly         { get { return 5; } }

    Int16 _sing;
    Int16 _preen;
    Int16 _ruffle;
    Int16 _peck;
    Int16 _hopForward;
    Int16 _hopBackward;
    Int16 _hopLeft;
    Int16 _hopRight;
    Int16 _fly;

    protected LikelyBirdAction() {
      Int16 total = (Int16)(
        Sing +
        Preen +
        Ruffle +
        Peck +
        HopForward +
        HopBackward +
        HopLeft +
        HopRight +
        Fly
      );
      _sing = (Int16)((Sing * 100) / total);
      _preen = (Int16)(_sing + (Preen * 100) / total);
      _ruffle = (Int16)(_preen + (Ruffle * 100) / total);
      _peck = (Int16)(_ruffle + (Peck * 100) / total);
      _hopForward = (Int16)(_peck + (HopForward * 100) / total);
      _hopBackward = (Int16)(_hopForward + (HopBackward * 100) / total);
      _hopLeft = (Int16)(_hopBackward + (HopLeft * 100) / total);
      _hopRight = (Int16)(_hopLeft + (HopRight * 100) / total);
      _fly = (Int16)(_hopRight + (Fly * 100) / total);
    }

    public BirdAction Next(Int16 percent) {
      return (
          percent < _sing        ? BirdAction.Sing
        : percent < _preen       ? BirdAction.Preen
        : percent < _ruffle      ? BirdAction.Ruffle
        : percent < _peck        ? BirdAction.Peck
        : percent < _hopForward  ? BirdAction.HopForward
        : percent < _hopBackward ? BirdAction.HopBackward
        : percent < _hopLeft     ? BirdAction.HopLeft
        : percent < _hopRight    ? BirdAction.HopRight
        : BirdAction.Fly
      );
    }
  }

  class AfterIdle : LikelyBirdAction {}
  class AfterSing : LikelyBirdAction {}
  class AfterPreen : LikelyBirdAction {
    protected override Int16 Preen { get { return 1; } }
  }
  class AfterRuffle : LikelyBirdAction {
    protected override Int16 Ruffle { get { return 1; } }
  }
  class AfterPeck : LikelyBirdAction {
    protected override Int16 Peck { get { return 10; } }
  }
  class AfterHopForward : LikelyBirdAction {
    protected override Int16 HopForward { get { return 15; } }
    protected override Int16 HopBackward { get { return 1; } }
    protected override Int16 HopLeft     { get { return 7; } }
    protected override Int16 HopRight    { get { return 7; } }
  }
  class AfterHopBackward : LikelyBirdAction {
    protected override Int16 HopForward { get { return 3; } }
    protected override Int16 HopBackward { get { return 4; } }
    protected override Int16 HopLeft     { get { return 2; } }
    protected override Int16 HopRight    { get { return 2; } }
  }
  class AfterHopLeft : LikelyBirdAction {
    protected override Int16 HopForward { get { return 15; } }
    protected override Int16 HopBackward { get { return 1; } }
    protected override Int16 HopLeft     { get { return 7; } }
    protected override Int16 HopRight    { get { return 1; } }
  }
  class AfterHopRight : LikelyBirdAction {
    protected override Int16 HopForward { get { return 15; } }
    protected override Int16 HopBackward { get { return 1; } }
    protected override Int16 HopLeft     { get { return 1; } }
    protected override Int16 HopRight    { get { return 7; } }
  }
  class AfterFly : LikelyBirdAction {
    protected override Int16 Sing        { get { return 2; } }
    protected override Int16 Preen       { get { return 0; } }
    protected override Int16 Ruffle      { get { return 0; } }
    protected override Int16 Peck        { get { return 0; } }
    protected override Int16 HopForward  { get { return 0; } }
    protected override Int16 HopBackward { get { return 0; } }
    protected override Int16 HopLeft     { get { return 0; } }
    protected override Int16 HopRight    { get { return 0; } }
    // TODO: until we've got fly implemented don't fly much :)
    protected override Int16 Fly         { get { return 0; } }
  }

  class BirdActionSelector {
    BirdAction _previousAction;

    LikelyBirdAction _afterIdle = new AfterIdle();
    LikelyBirdAction _afterSing = new AfterSing();
    LikelyBirdAction _afterPreen = new AfterPreen();
    LikelyBirdAction _afterRuffle = new AfterRuffle();
    LikelyBirdAction _afterPeck = new AfterPeck();
    LikelyBirdAction _afterHopForward = new AfterHopForward();
    LikelyBirdAction _afterHopBackward = new AfterHopBackward();
    LikelyBirdAction _afterHopLeft = new AfterHopLeft();
    LikelyBirdAction _afterHopRight = new AfterHopRight();
    LikelyBirdAction _afterFly = new AfterFly();


    public BirdAction NextAction(BirdState state) {
      Int16 percent = (Int16)UnityEngine.Random.Range(1, 100);
      // Much more likely to fly if worried
      /*
      if (state == BirdState.Worried && percent < 30) {
        _previousAction = BirdAction.Fly;
        return _previousAction;
      }
      */
      var likelyAction = LikelyAction();
      BirdAction nextAction;
      do {
        percent = (Int16)UnityEngine.Random.Range(1, 100);
        nextAction = likelyAction.Next(percent);
      } while(!nextAction.CanPerformAction(state));

      _previousAction = nextAction;
      return _previousAction;
    }

    LikelyBirdAction LikelyAction() {
      if (_previousAction == null) return new AfterIdle();
      switch (_previousAction.Action) {
        case BirdActionEnum.Sing: return _afterSing;
        case BirdActionEnum.Preen: return _afterPreen;
        case BirdActionEnum.Ruffle: return _afterRuffle;
        case BirdActionEnum.Peck: return _afterPeck;
        case BirdActionEnum.HopForward: return _afterHopForward;
        case BirdActionEnum.HopBackward: return _afterHopBackward;
        case BirdActionEnum.HopLeft: return _afterHopLeft;
        case BirdActionEnum.HopRight: return _afterHopRight;
        case BirdActionEnum.Fly: return _afterFly;
        default: throw new Exception("Unknown previous Action");
      }
    }
  }
}
