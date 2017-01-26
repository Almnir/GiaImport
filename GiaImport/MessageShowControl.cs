using Microsoft.Samples;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiaImport
{
    class MessageShowControl
    {

        public static void ShowValidationErrors(Dictionary<string, string> filesErrors)
        {
            TaskDialog taskDialog = new TaskDialog();
            taskDialog.WindowTitle = "Результаты верификации";
            taskDialog.MainIcon = TaskDialogIcon.Warning;
            taskDialog.MainInstruction = "Результаты верификации";
            taskDialog.Content = "Файлы, не прошедшие проверку и причина ошибки";
            taskDialog.ExpandedByDefault = true;
            StringBuilder fileErrorText = new StringBuilder();
            foreach (var fe in filesErrors)
            {
                fileErrorText.Append(string.Format("{0} - {1}", fe.Key, fe.Value)).Append(Environment.NewLine);
            }
            taskDialog.ExpandedInformation = fileErrorText.ToString();
            taskDialog.CommonButtons = TaskDialogCommonButtons.Close;
            int result = taskDialog.Show();
        }
        public static void ShowValidationSuccess()
        {
            TaskDialog taskDialog = new TaskDialog();
            taskDialog.WindowTitle = "Результаты верификации";
            taskDialog.MainIcon = TaskDialogIcon.None;
            taskDialog.MainInstruction = "Результаты верификации";
            taskDialog.Content = "Верификация пройдена без ошибок!";
            taskDialog.CommonButtons = TaskDialogCommonButtons.Close;
            int result = taskDialog.Show();
        }
    }
}
