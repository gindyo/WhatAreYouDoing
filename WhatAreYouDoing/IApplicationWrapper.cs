namespace WhatAreYouDoing
{
    public interface IApplicationWrapper
    {
        void Shutdown();
        void PopWindow(MainWindow mainWindow);
        bool WindowIsOpen();
        void CloseCurrentWindow();
    }
}