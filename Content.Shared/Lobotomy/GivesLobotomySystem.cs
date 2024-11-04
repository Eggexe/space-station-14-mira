using Content.Shared.DoAfter;
using Content.Shared.Interaction;

namespace Content.Shared.Lobotomy;

public sealed partial class GivesLobotomySystem : EntitySystem
{
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GivesLobotomyComponent, AfterInteractEvent>(OnInteract);
        SubscribeLocalEvent<GivesLobotomyComponent, GivesLobotomyDoAfterEvent>(OnDoAfter);
    }

    private void OnInteract(EntityUid uid, GivesLobotomyComponent component, ref AfterInteractEvent args)
    {
        if (args.Handled || !args.CanReach || args.Target == null)
            return;

        _doAfter.TryStartDoAfter(new DoAfterArgs(EntityManager, args.User, component.Time, new GivesLobotomyDoAfterEvent(), uid, target: args.Target)
        {
            BreakOnMove = true,
            BreakOnDropItem = true,
            BreakOnDamage = true
        });
        args.Handled = true;
    }

    private void OnDoAfter(EntityUid uid, GivesLobotomyComponent component, DoAfterEvent args)
    {
        if (args.Handled || args.Cancelled || args.Target == null)
            return;

        EnsureComp<LobotomyComponent>(args.Target.Value);
        args.Handled = true;
    }
}
