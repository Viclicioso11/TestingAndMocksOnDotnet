using System;

namespace Api.Interfaces;

public interface ITwilioService
{
    /// <summary>
    /// Sends an SMS message to the specified phone number.
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="to">The destination number</param>
    /// <returns></returns>
    Task SendSms(string message, string to);
}
