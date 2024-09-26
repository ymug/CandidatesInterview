using System;
using System.Windows;

namespace InterviewTest.App.Helpers
{
    internal static class DispatcherHelpers
    {
        public static void DispatchIfNecessary(Action action)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }

        }

    }
}
