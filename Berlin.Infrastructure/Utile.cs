using Berlin.Domain.Entities;

namespace Berlin.Infrastructure
{

    static public class Utile
    {
        private static Dictionary<String, EnvObj> SessonData = new Dictionary<String, EnvObj>();


        static public void Clear()
        {
            SessonData.Clear();
        }
        static public void SetSessionData(User user, String device)
        {
            if (SessonData.ContainsKey(device))
            {
                SessonData[device].User = user;
                return;
            }

            SessonData.Add(device, new EnvObj() { User = user });
        }

        static public void SetSessionData(Division item, String device)
        {
            if (SessonData.ContainsKey(device))
            {
                SessonData[device].Division = item;
                return;
            }

            SessonData.Add(device, new EnvObj() { Division = item });
        }

        static public void SetReceiptData(Receipt item, String device)
        {
            if (SessonData.ContainsKey(device))
            {
                SessonData[device].Receipt = item;
                return;
            }

            SessonData.Add(device, new EnvObj() { Receipt = item });
        }
        static public void RemoveReceiptData( String device)
        {
            if (SessonData.ContainsKey(device))
            {
                SessonData[device].Receipt = null;
                return;
            }
        }

        static public void SetSessionData(Site item, String device)
        {
            if (SessonData.ContainsKey(device))
            {
                SessonData[device].Site = item;
                return;
            }

            SessonData.Add(device, new EnvObj() { Site = item });
        }

        static public Site GetSite(String device)
        {
               return SessonData[device].Site;
        }
        static public Division GetDivision(String device)
        {
            return SessonData[device].Division;
        }

        static public User GetUser(String device)
        {
            return SessonData[device].User;
        }
        static public Receipt GetReceipt(String device)
        {
            return SessonData[device].Receipt;
        }
    }

    public class EnvObj
    {
        public Site Site { get; set; }
        public User User { get; set; }
        public Division Division { get; set; }

        public Receipt Receipt { get; set; }
    }
}
