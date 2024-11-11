using System.ComponentModel;
using System.Runtime.Serialization;
using EventSourcing.Events;
using EventSourcing.Exceptions;
using EventSourcing.Models;
using Microsoft.VisualBasic;

namespace EventSourcing;

public class AccountAggregate
{

  public string? AccountId { get; set; }
  public decimal Balance { get; set; }
  public CurrencyType Currency { get; set; }
  public string? CustomerId { get; set; }
  public AccountStatus Status { get; set; }
  public List<LogMessage>? AccountLog { get; set; }

  private AccountAggregate() { }

  public static AccountAggregate? GenerateAggregate(Event[] events)
  {
    if (events.Length == 0)
    {
      return null;
    }
    var account = new AccountAggregate();
    foreach (var accountEvent in events)
    {
      account.Apply(accountEvent);
    }
    return account;
  }

  private void Apply(Event accountEvent)
  {
    switch (accountEvent)
    {
      case AccountCreatedEvent accountCreated:
        Apply(accountCreated);
        break;
      case DepositEvent deposit:
        Apply(deposit);
        break;
      case WithdrawalEvent withdrawalEventevents:
        Apply(withdrawalEventevents);
        break;
      case DeactivationEvent deactivationEvent:
        Apply(deactivationEvent);
        break;
      default:
        throw new EventTypeNotSupportedException("162 ERROR_EVENT_NOT_SUPPORTED");
    }
  }

  private void Apply(AccountCreatedEvent accountCreated)
  {
    AccountId = accountCreated.AccountId;
    Balance = accountCreated.InitialBalance;
    Currency = accountCreated.Currency;
    CustomerId = accountCreated.CustomerId;
  }

  private void Apply(DepositEvent deposit)
  {
    if (AccountId == null)
    {
      throw new Exception("128*");
    }
    if (Balance < deposit.Amount)
    {
      throw new Exception("281*");
    }
    Balance += deposit.Amount;
  }

  private void Apply(WithdrawalEvent wihdrawal)
  {
    if (AccountId == null)
    {
      throw new Exception("128*");
    }

    Balance -= wihdrawal.amount;
    if (Balance < 0)
    {
      throw new Exception("285*");
    }
  }

  private void Apply(DeactivationEvent deactivation)
  {
    Status = AccountStatus.Disabled;
    if (AccountLog == null)
    {
      AccountLog = new List<LogMessage>();
    }
    AccountLog.Add(new LogMessage("DEACTIVATE", deactivation.Reason.ToString(), deactivation.Timestamp));

  }

  private void Apply(ActivationEvent activation)
  {
    throw new NotImplementedException();
  }

  private void Apply(CurrencyChangeEvent currencyChange)
  {
    throw new NotImplementedException();
  }

  private void Apply(ClosureEvent closure)
  {
    throw new NotImplementedException();
  }
}
