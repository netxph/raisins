using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Raisins.Client.Raffle.Wpf
{
    public class ElementBehaviours
    {

        public static readonly DependencyProperty LoadedCommand = BehaviourFactory.CreateCommand(Window.LoadedEvent, "LoadedCommand", typeof(ElementBehaviours));

        public static void SetLoadedCommand(Control o, ICommand command)
        {
            o.SetValue(LoadedCommand, command);
        }

        public static void GetLoadedCommand(Control o)
        {
            o.GetValue(LoadedCommand);
        }

    }
}
