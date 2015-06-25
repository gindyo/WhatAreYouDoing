using WhatAreYouDoing.Main;

namespace WhatAreYouDoing.Interfaces
{
    public interface IApplicationWrapper
    {
        void Shutdown();
        void PopWindow(MainWindow mainWindow);
        bool WindowIsOpen();
        void CloseCurrentWindow();
    }
}