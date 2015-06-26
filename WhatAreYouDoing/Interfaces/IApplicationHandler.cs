using WhatAreYouDoing.Display.Main;

namespace WhatAreYouDoing.Interfaces
{
    public interface IApplicationHandler
    {
        void Shutdown();
        void PopWindow(MainWindow mainWindow);
        bool WindowIsOpen();
        void CloseCurrentWindow();
    }
}

