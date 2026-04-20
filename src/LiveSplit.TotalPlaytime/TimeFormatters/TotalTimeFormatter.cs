using System;
using System.Globalization;
using System.Text;

namespace LiveSplit.TimeFormatters;

public class TotalTimeFormatter : ITimeFormatter
{
    public bool ShowTotalHours { get; set; }
    public bool ShowMinutes { get; set; }
    public bool ShowSeconds { get; set; }

    public TotalTimeFormatter()
    {
        ShowTotalHours = false;
        ShowMinutes = true;
        ShowSeconds = true;
    }

    public string Format(TimeSpan? time)
    {
        if (!time.HasValue)
        {
            return "0";
        }

        var timeValue = time.Value;

        if (!ShowMinutes && timeValue.TotalHours <= 1)
        {
            return "0h";
        }

        var builder = new StringBuilder();

        // days, hours
        if (timeValue.TotalHours >= 1)
        {
            if (timeValue.TotalDays >= 1)
            {
                if (ShowTotalHours)
                {
                    builder.Append((int)timeValue.TotalHours);
                }
                else
                {
                    builder.Append($"{(int)timeValue.TotalDays}d {timeValue.Hours}");
                }
            }
            else
            {
                builder.Append(timeValue.Hours);
            }
        }

        // minutes, seconds
        if (ShowMinutes)
        {
            var format = new StringBuilder();

            if (timeValue.TotalHours >= 1)
            {
                builder.Append(timeValue.ToString(@"\:mm", CultureInfo.InvariantCulture));
            }
            else
            {
                builder.Append($"{timeValue.Minutes}");
            }

            if (ShowSeconds)
            {
                builder.Append(timeValue.ToString(@"\:ss", CultureInfo.InvariantCulture));
            }
        }
        else
        {
            builder.Append("h");
        }

            return builder.ToString();
    }
}
