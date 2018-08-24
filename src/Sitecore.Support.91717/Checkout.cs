using Sitecore;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Support.Shell.Framework.Commands
{
  public class Checkout : Sitecore.Shell.Framework.Commands.CheckOut
  {
    public override CommandState QueryState(CommandContext context)
    {
      if (context.Items.Length != 1)
      {
        return CommandState.Hidden;
      }
      if (Context.IsAdministrator)
      {
        return CommandState.Enabled;
      }
      var item = context.Items[0];
      if (item.Locking.IsLocked())
      {
        return CommandState.Disabled;
      }
      if (item.Appearance.ReadOnly)
      {
        return CommandState.Disabled;
      }
      if (!item.Access.CanWrite())
      {
        return CommandState.Disabled;
      }
      if (item.Locking.HasLock())
      {
        return CommandState.Disabled;
      }
      return base.QueryState(context);
    }
  }
}
