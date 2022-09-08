using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }

    private bool Validate()
    {
        if (Type == EDocumentType.CNPJ && Number.Length == 14)
        {
            return ValidateCnpj(Number);
        }

        if (Type == EDocumentType.CPF && Number.Length == 11)
        {
            return ValidateCpf(Number);
        }

        return false;
    }

    private bool ValidateCpf(string cpf)

    {
        var multiplication1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var multiplication2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        cpf = cpf.Trim();

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)

            return false;

        var hasCpf = cpf[..9];

        var soma = 0;

        for (var i = 0; i < 9; i++)

            soma += int.Parse(hasCpf[i].ToString()) * multiplication1[i];

        var rest = soma % 11;

        if (rest < 2)

            rest = 0;

        else

            rest = 11 - rest;

        var digit = rest.ToString();

        hasCpf = hasCpf + digit;

        soma = 0;

        for (var i = 0; i < 10; i++)
            soma += int.Parse(hasCpf[i].ToString()) * multiplication2[i];

        rest = soma % 11;

        if (rest < 2)
            rest = 0;

        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cpf.EndsWith(digit);
    }

    public bool ValidateCnpj(string cnpj)

    {
        int[] multiplication1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] multiplication2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        cnpj = cnpj.Trim();

        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)

            return false;

        var hasCnpj = cnpj.Substring(0, 12);

        var soma = 0;

        for (int i = 0; i < 12; i++)

            soma += int.Parse(hasCnpj[i].ToString()) * multiplication1[i];

        var rest = (soma % 11);

        if (rest < 2)

            rest = 0;

        else

            rest = 11 - rest;

        var digit = rest.ToString();

        hasCnpj = hasCnpj + digit;

        soma = 0;

        for (int i = 0; i < 13; i++)

            soma += int.Parse(hasCnpj[i].ToString()) * multiplication2[i];

        rest = (soma % 11);

        if (rest < 2)

            rest = 0;

        else

            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cnpj.EndsWith(digit);
    }


    public Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;

        AddNotifications(
            new Contract<Document>()
                .Requires()
                .IsTrue(
                    Validate(),
                    "Document.Number",
                    "Invalid Document")
        );
    }
}