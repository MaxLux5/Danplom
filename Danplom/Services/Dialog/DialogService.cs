using Danplom.Services.Dialog.Base;
using System.Windows;

namespace Danplom.Services.Dialog;

public class DialogService : IDialogService
{
    public event EventHandler<EventArgs> DialogOpened;
    public event EventHandler<EventArgs> DialogClosed;

    public void OpenDialog(Window dialogWindow, IDialogViewModel dialogViewModel)
    {
        dialogWindow.DataContext = dialogViewModel;

        dialogWindow.Closed += (obj, e) => dialogViewModel.CloseDialog();
        dialogViewModel.DialogClosed += (obj, e) => dialogWindow?.Close();

        dialogWindow.Show();
    }
}
