using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Danplom.Events;
using Danplom.Services.Dialog.Base;

namespace Danplom.ViewModel.Dialog.Base;

public abstract partial class DefaultDialogViewModel<TResult> : ObservableObject, IDialogViewModel
{
    [ObservableProperty]
    private TResult _value;

    public event EventHandler DialogClosed;

    public event GenericEventHandler<TResult> ResultDone;


    public void Dispose()
    {
        DialogClosed = null;
        ResultDone = null;
    }

    [RelayCommand]
    private void Cancel() => CloseDialog();

    [RelayCommand]
    private void Ok()
    {
        ResultDone?.Invoke(this, new GenericEventArgs<TResult>(Value));
        DialogClosed?.Invoke(this, EventArgs.Empty);
    }

    public void CloseDialog() => DialogClosed?.Invoke(this, EventArgs.Empty);
}
