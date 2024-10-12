namespace panitab_backend.Helpers
{
    //esta clase va a funcionar con el refresh token y lo que hace es que va a convertir la fecha de expiracion del token a la hora de honduras
    public class DateTimeUtils
    {
        private static readonly TimeZoneInfo HondurasTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");

        public static DateTime GetHondurasDateTime()
        {
            DateTime dateTime = DateTime.UtcNow;
            DateTime hondurasDateTime = TimeZoneInfo.ConvertTime(dateTime, HondurasTimeZone);
            return hondurasDateTime;
        }
    }
}
