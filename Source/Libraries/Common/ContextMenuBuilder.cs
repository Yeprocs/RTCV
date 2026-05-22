using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RTCV.Common
{
    public class ContextMenuBuilder
    {
        private readonly ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private readonly Stack<ToolStripMenuItem> _subMenus = new Stack<ToolStripMenuItem>();
        private Stack<bool> _controlFlow = new Stack<bool>();
        private bool _cancelClose;
        private bool _dontCloseOnClick;
        private bool _doCloseOnClick;
        private bool IsActive => _controlFlow.Count == 0 || _controlFlow.Peek();

        public ContextMenuBuilder()
        {
        }

        public ContextMenuBuilder If(bool condition)
        {
            _controlFlow.Push(IsActive && condition);
            return this;
        }
        public ContextMenuBuilder EndIf()
        {
            if (_controlFlow.Count == 0)
                throw new InvalidOperationException("EndIf() must be called after If() and Else()");

            _controlFlow.Pop();
            return this;
        }
        public ContextMenuBuilder Else()
        {
            if (_controlFlow.Count == 0)
                throw new InvalidOperationException("Else() must be called between If() and EndIf()");

            bool current = _controlFlow.Pop();
            bool parentActive = IsActive;
            _controlFlow.Push(parentActive && !current);
            return this;
        }

        public ContextMenuBuilder BeginSubMenu(string text, bool enabled = true)
        {
            if (!IsActive)
                return this;

            _subMenus.Push(new ToolStripMenuItem(text) { Enabled = enabled });
            return this;
        }
        public ContextMenuBuilder EndSubMenu()
        {
            if (!IsActive)
                return this;

            if (_subMenus.Count == 0)
                throw new InvalidOperationException("EndSubMenu() called without a matching BeginSubMenu()");

            var item = _subMenus.Pop();

            if (_subMenus.Count > 0)
                _subMenus.Peek().DropDownItems.Add(item);
            else
                _contextMenu.Items.Add(item);

            return this;
        }

        public ContextMenuBuilder AddItem(ToolStripItem item)
        {
            if (!IsActive)
                return this;

            if (_subMenus.Count > 0)
                _subMenus.Peek().DropDownItems.Add(item);
            else
                _contextMenu.Items.Add(item);

            return this;
        }
        public ContextMenuBuilder AddItem(string text, EventHandler onClick, bool enabled = true, bool isChecked = false)
        {
            var item = new ToolStripMenuItem(text);
            item.Enabled = enabled;
            item.Checked = isChecked;
            item.Click += onClick;
            return AddItem(item);
        }

        public ContextMenuBuilder AddText(string text, FontStyle style) =>
            AddText(text, true, style);
        public ContextMenuBuilder AddText(string text, bool enabled = true, FontStyle style = FontStyle.Regular)
        {
            var item = new ToolStripLabel(text);
            item.Enabled = enabled;
            item.Font = new Font(item.Font, style);
            return AddItem(item);
        }

        public ContextMenuBuilder AddHeader(string text)
        {
            var item = new ToolStripLabel(text);
            item.Font = new Font("Segoe UI", 12);
            return AddItem(item);
        }

        public ContextMenuBuilder AddSeparator()
        {
            return AddItem(new ToolStripSeparator());
        }

        public ContextMenuBuilder DontCloseOnClick()
        {
            _dontCloseOnClick = true;
            return this;
        }
        public ContextMenuBuilder DoCloseOnClick()
        {
            ToolStripItemCollection items;
            if (_subMenus.Count > 0)
                items = _subMenus.Peek().DropDownItems;
            else
                items = _contextMenu.Items;

            items[items.Count - 1].MouseEnter += (s, e) => _doCloseOnClick = true;
            items[items.Count - 1].MouseLeave += (s, e) => _doCloseOnClick = false;

            return this;
        }

        public ContextMenuStrip Build()
        {
            if (_controlFlow.Count > 0)
                throw new InvalidOperationException("All If() calls must be closed with EndIf()");

            if (_subMenus.Count > 0)
                throw new InvalidOperationException("All BeginSubMenu() calls must be closed with EndSubMenu()");

            if (_dontCloseOnClick)
            {
                ApplyDontCloseOnClick(_contextMenu);
            }

            return _contextMenu;
        }

        private void ApplyDontCloseOnClick(ToolStripDropDown dropDown)
        {
            dropDown.Closing += (o, e) =>
            {
                if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                {
                    e.Cancel = !_doCloseOnClick;
                }
            };

            foreach (ToolStripItem item in dropDown.Items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    ApplyDontCloseOnClick(menuItem.DropDown);
                }
            }
        }
    }
}