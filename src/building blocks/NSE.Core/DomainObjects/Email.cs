using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSE.Core.DomainObjects;

public class Email
{
    public const int ADDRESS_MAX_LENGTH = 254;
    public const int ADDRESS_MIN_LENGTH = 5;
    public const string EMAIL_REGEX = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";
    public string Address { get; private set; }

    // EF Relation
    protected Email() { }

    public Email(string address)
    {
        if (!Validate(address)) throw new DomainException("Invalid email");
        Address = address;
    }

    public static bool Validate(string email)
    {
        var regexEmail = new Regex(EMAIL_REGEX);

        return regexEmail.IsMatch(email);
    }
}
