using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared.Lobotomy;

[RegisterComponent]
public sealed partial class GivesLobotomyComponent : Component
{
    [DataField("time")]
    public float _time = 5f;

    [ViewVariables]
    public TimeSpan Time => TimeSpan.FromSeconds(_time);
}

[Serializable, NetSerializable]
public sealed partial class GivesLobotomyDoAfterEvent : SimpleDoAfterEvent;
