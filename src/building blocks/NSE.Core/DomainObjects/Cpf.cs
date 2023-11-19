using NSE.Core.Util;

namespace NSE.Core.DomainObjects;

public class Cpf
{
    public const int CPF_MAX_LENGTH = 11;
    public string Number { get; private set; }
    public Cpf()
    {
            
    }
    public Cpf(string number)
    {
        if (!Validate(number)) 
            throw new DomainException("Invalid CPF");

        Number = number;
    }

    public static bool Validate(string cpf)
    {
        if(string.IsNullOrEmpty(cpf))
            return false;

        cpf = cpf.OnlyNumbers();

        if (cpf.Length > CPF_MAX_LENGTH)
            return false;

        while (cpf.Length != CPF_MAX_LENGTH)
            cpf = '0' + cpf;

        var equal = true;
        for (var i = 1; i < CPF_MAX_LENGTH && equal; i++)
            if (cpf[i] != cpf[0])
                equal = false;

        if (equal || cpf == "12345678909")
            return false;

        var numbers = new int[CPF_MAX_LENGTH];

        for (var i = 0; i < CPF_MAX_LENGTH; i++)
            numbers[i] = int.Parse(cpf[i].ToString());

        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += (10 - i) * numbers[i];

        var result = sum % CPF_MAX_LENGTH;

        if (result == 1 || result == 0)
        {
            if (numbers[9] != 0)
                return false;
        }
        else if (numbers[9] != CPF_MAX_LENGTH - result)
            return false;

        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += (CPF_MAX_LENGTH - i) * numbers[i];

        result = sum % CPF_MAX_LENGTH;

        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
                return false;
        }
        else if (numbers[10] != CPF_MAX_LENGTH - result)
            return false;

        return true;
    }
}
