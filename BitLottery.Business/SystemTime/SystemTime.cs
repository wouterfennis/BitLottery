using System;

namespace BitLottery.Business.SystemTime
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;

        public static void SetDateTime(DateTime dateTimeNow)
        {
            Now = () => dateTimeNow;
        }

        public static void ResetDateTime()
        {
            Now = () => DateTime.Now;
        }
    }
}
