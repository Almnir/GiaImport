using RBD.Common.Enums;

namespace RBD.Client.Services.Import.ImportWizard.Base
{
    public interface IWizardSettingsManager
    {
        ImportSenderType Sender { get; set; }
    }
}
