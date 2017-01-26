using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FtcReportControls.WindowsSystem
{
    class ErrorStrings
    {
        public static string ProgressDialogNotRunningError = "Диалог прогресса не отображён!";
        public static string ProgressDialogRunning = "Диалог прогресса уже запущен!";
        public static string AnimationLoadErrorFormat = "Невозможно загрузить анимацию для отображения!";
        public static string FileNotFoundFormat = "Файл {0} не найден!";
        public static string CredentialEmptyTargetError = "Учётная запись не может быть пустой!";
        public static string CredentialPromptNotCalled = "Метод PromptForCredentialsWithSave не был вызван или данные были изменены после вызова.";
        public static string CredentialError = "Ошибка возникла при доступе к данным учётной записи.";
        public static string Preview = "Превью";
        public static string GlassNotSupportedError = "Текущая операционная система не поддерживает AeroGlass или не запущен менджер окон!";

        public static string TaskDialogNotRunningError = "Диалог задачи не отображается!";
        public static string TaskDialogsNotSupportedError = "Текущая операционная система не поддерживает Task Dialog";
        public static string TaskDialogRunningError = "Task Dialog уже запущен!";
        public static string TaskDialogNoButtonsError = "Task Dialog должен иметь кнопки!";
        public static string InvalidTaskDialogItemIdError = "The id of a task dialog item must be higher than 0.";
        public static string TaskDialogEmptyButtonLabelError = "A custom button or radio button cannot have an empty label.";
        public static string TaskDialogIllegalCrossThreadCallError = "Cross-thread operation not valid: Task dialog accessed from a thread other than the thread it was created on while it is visible.";

        public static string NonCustomTaskDialogButtonIdError = "Cannot change the id for a standard button.";
        public static string DuplicateButtonTypeError = "The task dialog already has a non-custom button with the same type.";

        public static string NoAssociatedTaskDialogError = "The item is not associated with a task dialog.";
        public static string DuplicateItemIdError = "The task dialog already has an item with the same id.";

        public static string TaskDialogItemHasOwnerError = "The task dialog item already belongs to another task dialog.";

        public static string InvalidFilterString = "Invalid filter string.";
        public static string Help = "Help";
    }
}
