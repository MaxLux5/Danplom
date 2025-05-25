using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace Danplom.Services.Dialog.Base;

public interface IDialogService
{
    public event EventHandler<EventArgs> DialogOpened;

    public event EventHandler<EventArgs> DialogClosed;

    /// <summary>
    ///     Открывает диалог, за время жизни которого отвечает модель представления диалогового элемента, реализующая IDialogViewModel.
    /// </summary>
    void OpenDialog(Window dialogWindow, IDialogViewModel dialogViewModel);
}
