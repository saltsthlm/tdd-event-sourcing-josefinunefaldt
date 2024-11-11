using System.Text.Json.Serialization;

namespace EventSourcing.Events;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AccountCreatedEvent), typeDiscriminator: "account-created")]
[JsonDerivedType(typeof(DepositEvent), typeDiscriminator: "deposit")]
[JsonDerivedType(typeof(WithdrawalEvent), typeDiscriminator: "withdrawal")]
[JsonDerivedType(typeof(CurrencyChangeEvent), typeDiscriminator: "currency-change")]
[JsonDerivedType(typeof(DeactivationEvent), typeDiscriminator: "deactivate")]
[JsonDerivedType(typeof(ActivationEvent), typeDiscriminator: "activate")]
[JsonDerivedType(typeof(ClosureEvent), typeDiscriminator: "closure")]
[JsonDerivedType(typeof(InvalidEvent), typeDiscriminator: "unsupported-event")]
public partial record Event
{
  [JsonPropertyName("eventId")]
  public required int EventId { get; init; }

  [JsonPropertyName("timestamp")]
  public required DateTime Timestamp { get; init; }

  [JsonPropertyName("accountId")]
  public required string AccountId { get; init; }
}

