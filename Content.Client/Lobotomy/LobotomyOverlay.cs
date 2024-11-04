using Robust.Client.Graphics;
using Robust.Shared.Prototypes;
using Robust.Shared.Enums;

namespace Content.Client.Lobotomy;

public sealed class LobotomyOverlay : Overlay
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public override bool RequestScreenTexture => true;
    public override OverlaySpace Space => OverlaySpace.WorldSpace;

    private readonly ShaderInstance _lobotomyShader;

    public LobotomyOverlay()
    {
        IoCManager.InjectDependencies(this);
        _lobotomyShader = _prototypeManager.Index<ShaderPrototype>("Lobotomy").InstanceUnique();
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture == null)
            return;

        var handle = args.WorldHandle;
        handle.UseShader(_lobotomyShader);
    }
}
