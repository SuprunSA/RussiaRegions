using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class ListSelector<T> where T : class
    {
        Func<IList<T>> getNodes;
        IList<T> Nodes => getNodes();

        int selectedNodeIndex = 0;
        public int SelectedNodeIndex
        {
            get => selectedNodeIndex;
            set
            {
                if (value < 0)
                {
                    selectedNodeIndex = 0;
                }
                else if (value >= Nodes.Count)
                {
                    selectedNodeIndex = Nodes.Count - 1;
                }
                else
                {
                    selectedNodeIndex = value;
                }
            }
        }
        public T SelectedNode
        {
            get => Nodes.Count > 0
            ? Nodes[SelectedNodeIndex]
            : null;
            set => SelectedNodeIndex = Nodes.IndexOf(value);
        }
        public Menu Menu { get; }

        public ListSelector(Func<IList<T>> getNodes)
        {
            Menu = new Menu(new MenuItem[] {
                new MenuAction(ConsoleKey.UpArrow, "Вверх", () => SelectedNodeIndex--, true),
                new MenuAction(ConsoleKey.DownArrow, "Вниз", () => SelectedNodeIndex++, true),
            });
            this.getNodes = getNodes;
        }
    }
}
