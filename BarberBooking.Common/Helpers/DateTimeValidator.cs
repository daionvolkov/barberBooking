namespace BarberBooking.Common.Helpers;

public static class DateTimeValidator
{
    public static bool AreDatesValid(DateTime startTime, DateTime endTime)
    {
        var result = startTime.Date == endTime.Date && startTime < endTime;;
        return result;
    }
}