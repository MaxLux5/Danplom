namespace Danplom.Services.Dialog.Base;

/// <summary>
///     Интерфейс диалоговой модели представления, который должен сам отвечать за время жизни диалога, в котором он был вызван.
/// </summary>
public interface IDialogViewModel : IDisposable
{
    public event EventHandler DialogClosed;

    public void CloseDialog();
}
