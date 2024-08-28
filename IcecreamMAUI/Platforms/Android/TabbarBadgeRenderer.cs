using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using IcecreamMAUI.ViewModels;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcecreamMAUI
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        //override the bottom navbar
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }

        //name is custom
        class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
        {
            private BadgeDrawable _badgeDrawable;
            public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
            {
            }

            public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
            {
                base.SetAppearance(bottomView, appearance);

                if(_badgeDrawable is null)
                {
                    const int cartTabbarItemIndex = 1;
                    //the id of the item of the bottom nav bar
                    _badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIndex);
                    UpdateBadge(CartViewModel.TotalCartCount);
                    CartViewModel.TotalCartCountChanged += CartViewModel_TotalCartCountChanged;
                }
            }

            private void CartViewModel_TotalCartCountChanged(object? sender, int newCount) => UpdateBadge(CartViewModel.TotalCartCount);

            private void UpdateBadge(int count)
            {
                if (count <= 0)
                {
                    _badgeDrawable.SetVisible(false);
                }
                else
                {
                    _badgeDrawable.Number = count;
                    _badgeDrawable.BackgroundColor = Colors.DeepPink.ToPlatform();
                    _badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                    _badgeDrawable.SetVisible(true);
                }
            }

            protected override void Dispose(bool disposing)
            {
                CartViewModel.TotalCartCountChanged -= CartViewModel_TotalCartCountChanged;
                base.Dispose(disposing);
            }
        }
    }
}
