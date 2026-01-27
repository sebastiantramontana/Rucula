namespace Rucula.Domain.Entities.Parameters;

public sealed record class OptionalOptionParameters(Optional<BondCommissions> BondCommissions, Optional<DolarCryptoParameters> CryptoParameters, Optional<WesternUnionParameters> WesternUnionParameters, Optional<DolarAppParameters> DolarAppParameters);
