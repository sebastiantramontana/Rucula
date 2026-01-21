using Rucula.Domain.Entities;

namespace Rucula.Application;

public sealed record class OptionCallbacks(Action<WinningOption> OnWinningOption,
                                            Action<IEnumerable<TituloIsin>> OnBonds,
                                            Action<Optional<Blue>> OnBlue,
                                            Action<Optional<DolarWesternUnion>> OnWesternUnion,
                                            Action<IEnumerable<DolarCryptoPrices>> OnCrypto);
