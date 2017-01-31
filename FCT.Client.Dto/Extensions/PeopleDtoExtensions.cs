using FCT.Client.Dto.Interfaces;

namespace FCT.Client.Dto.Extensions
{
    public static class PeopleDtoExtensions
    {
        public static void CorrectionFio(this IPeopleDto people)
        {
            if (people != null)
            {
                if (!string.IsNullOrEmpty(people.Surname)) people.Surname = people.Surname.Trim();
                if (!string.IsNullOrEmpty(people.Name)) people.Name = people.Name.Trim();
                if (!string.IsNullOrEmpty(people.SecondName)) people.SecondName = people.SecondName.Trim();
            }
        }
    }
}
