using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories.Student;
using PaymentContext.Domain.Services.Email;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler :
    Notifiable<Notification>,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>,
    IHandler<CreateCreditCardSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to subscribe");
        }

        // Verificar se documento está cadastrado 
        if (_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Already exists document");
        }

        // Verificar se email tá cadastrado
        if (_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Already exists email");
        }

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        // Gerar entidades
        var student = new Student(
            name,
            document,
            email
        );
        // Assumindo que o boleto vence a cada 1 mês
        // mas isso pode ser diferente (30, 60, 120, ...)
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BardCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            new Document(command.PayerDocument, command.PayerDocumentType),
            command.Payer,
            address,
            email
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        if (!IsValid)
        {
            return new CommandResult(false, "Unable to subscribe");
        }
        
        // Salvar informações
        _repository.CreateSubscription(student);

        // Enviar e-mail
        _emailService.Send(
            student.Name.ToString(),
            student.Email.Address,
            "Welcome",
            "Subscription created"
        );

        // retornar infos
        return new CommandResult(true, "Successful subscription");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to subscribe");
        }

        // Verificar se documento está cadastrado 
        if (_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Already exists document");
        }

        // Verificar se email tá cadastrado
        if (_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Already exists email");
        }

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        // Gerar entidades
        var student = new Student(
            name,
            document,
            email
        );
        // Assumindo que o boleto vence a cada 1 mês
        // mas isso pode ser diferente (30, 60, 120, ...)
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(
            command.TransactionCode,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            new Document(command.PayerDocument, command.PayerDocumentType),
            command.Payer,
            address,
            email
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        if (!IsValid)
        {
            return new CommandResult(false, "Unable to subscribe");
        }
        
        // Salvar informações
        _repository.CreateSubscription(student);

        // Enviar e-mail
        _emailService.Send(
            student.Name.ToString(),
            student.Email.Address,
            "Welcome",
            "Subscription created"
        );

        // retornar infos
        return new CommandResult(true, "Successful subscription");
    }

    public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to subscribe");
        }

        // Verificar se documento está cadastrado 
        if (_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Already exists document");
        }

        // Verificar se email tá cadastrado
        if (_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Already exists email");
        }

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(
            command.Street,
            command.Number,
            command.Neighborhood,
            command.City,
            command.State,
            command.Country,
            command.ZipCode
        );

        // Gerar entidades
        var student = new Student(
            name,
            document,
            email
        );
        // Assumindo que o boleto vence a cada 1 mês
        // mas isso pode ser diferente (30, 60, 120, ...)
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new CreditCardPayment(
            command.CardHolderName,
            command.CardNumber,
            command.LastTransactionNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            new Document(command.PayerDocument, command.PayerDocumentType),
            command.Payer,
            address,
            email
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        if (!IsValid)
        {
            return new CommandResult(false, "Unable to subscribe");
        }
        
        // Salvar informações
        _repository.CreateSubscription(student);

        // Enviar e-mail
        _emailService.Send(
            student.Name.ToString(),
            student.Email.Address,
            "Welcome",
            "Subscription created"
        );

        // retornar infos
        return new CommandResult(true, "Successful subscription");
    }
}