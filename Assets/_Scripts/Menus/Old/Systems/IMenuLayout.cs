using UnityEngine;

namespace OldMenus
{
    public interface IMenuLayout<T> where T : DataEnum, new()
    {
        public TMPro.TextAlignmentOptions TextAlignment { get; }
        public Vector2 TMPRectPivot { get; }
        public Vector2 GetPosition(int i, int length);
        public Vector2 GetDescPosition();
        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems);
    }

    public class LeftScroll<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => new(Cam.UIOrthoX - 2.5f, 2);
        public Vector2 GetPosition(int i, int length) => new(-Cam.UIOrthoX + 2.5f, -(i * .8f));

        public Vector2 TMPRectPivot => new(0, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            var cur = dir switch
            {
                Dir.Up => PrevItem(current, menuItems),
                Dir.Down => NextItem(current, menuItems),
                _ => current,
            };
            Reposition(cur, menuItems);
            return cur;
        }

        private void Reposition(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Card.SetTMPPosition(
                    new(-Cam.UIOrthoX + 2.5f, (current * .8f) - (i * .8f)));
            }
        }

        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            MenuItem<T> cur = current == menuItems[^1] ? current : menuItems[current + 1];
            Reposition(cur, menuItems);
            return cur;
        }
        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            MenuItem<T> cur = current <= 0 ? current : menuItems[current - 1];
            Reposition(cur, menuItems);
            return cur;
        }
    }


    public class ScrollingHeader<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => Vector2.zero;
        public Vector2 GetPosition(int i, int length) => new((i * 3), Cam.UIOrthoY - 1);

        public Vector2 TMPRectPivot => new(.5f, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            var cur = dir switch
            {
                Dir.Left => PrevItem(current, menuItems),
                Dir.Right => NextItem(current, menuItems),
                _ => current,
            };
            Reposition(cur, menuItems);
            return cur;
        }

        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            MenuItem<T> cur = current == menuItems[^1] ? current : menuItems[current + 1];
            Reposition(cur, menuItems);
            return cur;
        }

        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            MenuItem<T> cur = current <= 0 ? current : menuItems[current - 1];
            Reposition(cur, menuItems);
            return cur;
        }

        private void Reposition(MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Card.SetTMPPosition(new(
                    -(current * 3) + (i * 3),
                    Cam.UIOrthoY - 1));
            }
        }
    }

    public class AlignLeft<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => new(0, -Cam.UIOrthoY * .5f);
        public Vector2 GetPosition(int i, int length) => new(-Cam.UIOrthoX + 2.5f, 1.8f - (i * .8f));

        public Vector2 TMPRectPivot => new(0, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            return dir switch
            {
                Dir.Up => PrevItem(current, menuItems),
                Dir.Down => NextItem(current, menuItems),
                _ => current,
            };
        }
        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];
    }

    public class AlignRight<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => new(0, -Cam.UIOrthoY * .5f);
        public Vector2 GetPosition(int i, int length) => new(Cam.UIOrthoX - 2.5f, 1.8f - (i * .8f));

        public Vector2 TMPRectPivot => new(1, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Right;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            return dir switch
            {
                Dir.Up => PrevItem(current, menuItems),
                Dir.Down => NextItem(current, menuItems),
                _ => current,
            };
        }
        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];
    }

    public class Header<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => Vector2.zero;
        public Vector2 GetPosition(int i, int length) => new(
            2 - Cam.UIOrthoX + (2 * (Cam.UIOrthoX - 2) / (length - 1) * i),
            Cam.UIOrthoY - 1);

        public Vector2 TMPRectPivot => new(.5f, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Center;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            return dir switch
            {
                Dir.Left => PrevItem(current, menuItems),
                Dir.Right => NextItem(current, menuItems),
                _ => current,
            };
        }
        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];
    }

    public class TwoColumns<T> : IMenuLayout<T> where T : DataEnum, new()
    {
        public Vector2 GetDescPosition() => new(0, -Cam.UIOrthoY * .5f);
        public Vector2 GetPosition(int i, int length) => new(
                i < length * .5f ? -Cam.UIOrthoX + 2.5f : 2,
                -1.8f - (i % Mathf.CeilToInt(length * .5f) * .8f) + (length * .5f));

        public Vector2 TMPRectPivot => new(0, .5f);

        public TMPro.TextAlignmentOptions TextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem<T> ScrollMenuItems(Dir dir, MenuItem<T> current, MenuItem<T>[] menuItems)
        {
            return dir switch
            {
                Dir.Up => PrevItem(current, menuItems),
                Dir.Down => NextItem(current, menuItems),
                Dir.Left => ScrollLeft(current, menuItems),
                Dir.Right => ScrollRight(current, menuItems),
                _ => current,
            };
        }

        MenuItem<T> NextItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            (current == Mathf.FloorToInt((menuItems.Length - .5f) * .5f) ||
             current == menuItems[^1]) ?
             current : menuItems[current + 1];

        MenuItem<T> PrevItem(MenuItem<T> current, MenuItem<T>[] menuItems) =>
            (current == Mathf.CeilToInt((menuItems.Length - .5f) * .5f) ||
             current <= 0) ?
             current : menuItems[current - 1];

        MenuItem<T> ScrollRight(MenuItem<T> current, MenuItem<T>[] menuItems) => current + Mathf.CeilToInt((menuItems.Length - .5f) * .5f) < menuItems.Length ?
            menuItems[current + Mathf.CeilToInt((menuItems.Length - .5f) * .5f)] : current;

        MenuItem<T> ScrollLeft(MenuItem<T> current, MenuItem<T>[] menuItems) => current - Mathf.CeilToInt((menuItems.Length - .5f) * .5f) >= 0 ?
            menuItems[current - Mathf.CeilToInt((menuItems.Length - .5f) * .5f)] : current;

    }
}