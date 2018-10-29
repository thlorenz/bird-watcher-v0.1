using System;
using System.Collections.Generic;

namespace Birder.Birds {

  [Flags]
  public enum BirdState {
    Any = 0x0,
    Grounded = 0x1,
    Perched = 0x2,
    Worried = 0x4,
    Flying = 0x8
  }

  public enum BirdActionEnum {
    Sing,
    Preen,
    Ruffle,
    Peck,
    HopForward,
    HopBackward,
    HopLeft,
    HopRight,
    Fly,
  }

  public class BirdAction {
    BirdActionEnum _action;
    Int16 _actionBlockers;

    private BirdAction(BirdActionEnum action, Int16 actionBlockers) {
      this._action = action;
      this._actionBlockers = actionBlockers;
    }

    public bool CanPerformAction(BirdState birdState) {
      Int16 state = (Int16)birdState;
      return (this._actionBlockers & state) != state;
    }

    const Int16 anyState = (Int16) BirdState.Any;
    const Int16 flying = (Int16)BirdState.Flying;
    const Int16 aboveGround = flying | (Int16)BirdState.Perched;
    const Int16 worried = (Int16) BirdState.Worried;

    static BirdAction _sing = new BirdAction(BirdActionEnum.Sing, worried);
    static BirdAction _preen = new BirdAction(BirdActionEnum.Preen, aboveGround);
    static BirdAction _ruffle = new BirdAction(BirdActionEnum.Ruffle, flying);
    static BirdAction _peck = new BirdAction(BirdActionEnum.Peck, aboveGround | worried);
    static BirdAction _hopForward = new BirdAction(BirdActionEnum.HopForward, aboveGround);
    static BirdAction _hopBackward = new BirdAction(BirdActionEnum.HopBackward, aboveGround);
    static BirdAction _hopLeft = new BirdAction(BirdActionEnum.HopLeft, aboveGround);
    static BirdAction _hopRight = new BirdAction(BirdActionEnum.HopRight, aboveGround);
    static BirdAction _fly = new BirdAction(BirdActionEnum.Fly, flying);

    static List<BirdAction> _birdActions = new List<BirdAction>{
      BirdAction.Sing,
      BirdAction.Preen,
      BirdAction.Ruffle,
      BirdAction.Peck,
      BirdAction.HopForward,
      BirdAction.HopBackward,
      BirdAction.HopLeft,
      BirdAction.HopRight,
      BirdAction.Fly
    };

    public BirdActionEnum Action {
      get { return _action; }
    }
    public static BirdAction Sing {
      get { return _sing; }
    }
    public static BirdAction Preen {
      get { return _preen; }
    }
    public static BirdAction Ruffle {
      get { return _ruffle; }
    }
    public static BirdAction Peck {
      get { return _peck; }
    }
    public static BirdAction HopForward {
      get { return _hopForward; }
    }
    public static BirdAction HopBackward {
      get { return _hopBackward; }
    }
    public static BirdAction HopLeft {
      get { return _hopLeft; }
    }
    public static BirdAction HopRight {
      get { return _hopRight; }
    }
    public static BirdAction Fly {
      get { return _fly; }
    }
    public static List<BirdAction> BirdActions {
      get { return _birdActions; }
    }
  }
}
