using Robust.Client.Graphics;
using Robust.Client.Player;
using Content.Shared.Lobotomy;

namespace Content.Client.Lobotomy;

public sealed partial class LobotomySystem : EntitySystem
{
    [Dependency] private readonly IOverlayManager _overlayManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    private LobotomyOverlay _lobotomy = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LobotomyComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<LobotomyComponent, ComponentShutdown>(OnShutdown);

        _lobotomy = new();
    }

    private void OnInit(EntityUid uid, LobotomyComponent component, ComponentInit args)
    {
        if (_playerManager.LocalEntity == uid)
        {
            _overlayManager.AddOverlay(_lobotomy);
        }
    }

    private void OnShutdown(EntityUid uid, LobotomyComponent component, ComponentShutdown args)
    {
        if (_playerManager.LocalEntity == uid)
        {
            _overlayManager.RemoveOverlay(_lobotomy);
        }
    }
}
