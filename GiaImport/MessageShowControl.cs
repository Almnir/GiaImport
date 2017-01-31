using System;
using System.Collections.Concurrent;
using System.Text;

namespace GiaImport
{
    class MessageShowControl
    {

        public static void ShowValidationErrors(ConcurrentDictionary<string, string> filesErrors)
        {
            MessageForm frm = new MessageForm();
            frm.SetTitle("Результаты верификации");
            frm.SetStyling(MessageForm.EnumMessageStyle.Error);
            frm.SetContent("Файлы, не прошедшие проверку и причина ошибки");
            StringBuilder fileErrorText = new StringBuilder();
            foreach (var fe in filesErrors)
            {
                fileErrorText.Append(string.Format("{0} - {1}", fe.Key, fe.Value)).Append(Environment.NewLine);
            }
            frm.SetExtendedContent(fileErrorText.ToString());
            MessageForm.ShowDialog("Результаты верификации", "Файлы, не прошедшие проверку и причина ошибки", fileErrorText.ToString(), MessageForm.EnumMessageStyle.Error);
            //TaskDialog taskDialog = new TaskDialog();
            //taskDialog.WindowTitle = "Результаты верификации";
            //taskDialog.MainIcon = TaskDialogIcon.Warning;
            //taskDialog.MainInstruction = "Результаты верификации";
            //taskDialog.Content = "Файлы, не прошедшие проверку и причина ошибки";
            //taskDialog.ExpandedByDefault = true;
            //StringBuilder fileErrorText = new StringBuilder();
            //foreach (var fe in filesErrors)
            //{
            //    fileErrorText.Append(string.Format("{0} - {1}", fe.Key, fe.Value)).Append(Environment.NewLine);
            //}
            //taskDialog.ExpandedInformation = fileErrorText.ToString();
            //taskDialog.CommonButtons = TaskDialogCommonButtons.Close;
            //int result = taskDialog.Show();
        }
        public static void ShowValidationSuccess()
        {
            MessageForm.ShowDialog("Результаты верификации", "Верификация пройдена без ошибок!", "Ошибок нет.", MessageForm.EnumMessageStyle.Information);
        }

        internal static void ShowPrepareSuccess()
        {
            MessageForm.ShowDialog("Подготовка", "Подготовка файлов окончена!", "Подготовка завершена", MessageForm.EnumMessageStyle.Information);
        }
    }
}
