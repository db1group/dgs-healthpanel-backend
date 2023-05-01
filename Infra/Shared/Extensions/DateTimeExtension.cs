using Db1HealthPanelBack.Entities;

namespace Db1HealthPanelBack.Infra.Shared.Extension
{
    public static class DateTimeExtension
    {
        public static void UpdateAllDates(this ICollection<AnswerQuestion> collection)
        {
            collection = collection.Select(prop => 
            {
                prop.UpdatedAt = DateTime.Now;

                return prop;
            }).ToList();
        }

        public static void UpdateAllDates(this ICollection<AnswerPillar> collection)
        {
            collection = collection.Select(prop => 
            {
                prop.UpdatedAt = DateTime.Now;

                return prop;
            }).ToList();
        }
    }
}