using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            //frm.SetContent("Файлы, не прошедшие проверку и причина ошибки");
            StringBuilder fileErrorText = new StringBuilder();
            foreach (var fe in filesErrors)
            {
                fileErrorText.Append(string.Format("{0} - {1}", fe.Key, fe.Value)).Append(Environment.NewLine);
            }
            frm.SetExtendedContent(fileErrorText.ToString());
            MessageForm.ShowDialog("Результаты верификации", "Файлы, не прошедшие проверку и причина ошибки", fileErrorText.ToString(), MessageForm.EnumMessageStyle.Error);
        }
        public static void ShowValidationSuccess()
        {
            MessageForm.ShowDialog("Результаты верификации", "Верификация пройдена без ошибок!", "Ошибок нет.", MessageForm.EnumMessageStyle.Information);
        }

        internal static void ShowPrepareSuccess()
        {
            MessageForm.ShowDialog("Подготовка", "Подготовка файлов окончена!", "Подготовка завершена", MessageForm.EnumMessageStyle.Information);
        }

        internal static void ShowImportSuccess()
        {
            MessageForm.ShowDialog("Импорт", "Импорт файлов окончен!", "Импорт завершён", MessageForm.EnumMessageStyle.Information);
        }

        internal static void ShowImportErrors(string v)
        {
            MessageForm.ShowDialog("Результаты импорта", "В процессе импорта произошли ошибки!", v, MessageForm.EnumMessageStyle.Error);
        }

        internal static void ShowPrepareErrors(string v)
        {
            MessageForm.ShowDialog("Подготовка", "В процессе подготовки произошли ошибки!", v, MessageForm.EnumMessageStyle.Error);
        }

        internal static void ShowImportPrepareErrors(List<string> guf)
        {
            StringBuilder errorText = new StringBuilder();
            foreach (var fe in guf)
            {
                errorText.Append(string.Format("{0}", fe)).Append(Environment.NewLine);
            }
            MessageForm.ShowDialog("Проверка", "Импорт невозможен, так как указаны файлы, которые имеют слишком большой размер для импорта без подготовки.", errorText.ToString(), MessageForm.EnumMessageStyle.Warning);
        }

        internal static void ShowTruncateSuccess()
        {
            MessageForm.ShowDialog("Очистка", "Очистка таблиц завершена!", "Очищено", MessageForm.EnumMessageStyle.Information);
        }
    }
}
