using Rucula.Domain.Entities;

namespace Rucula.Application;

public sealed record class OptionCallbacks(Func<WinningOption, Task> OnWinningOption,
                                            Func<IEnumerable<TituloIsin>, Task> OnBonds,
                                            Func<Optional<Blue>, Task> OnBlue,
                                            Func<Optional<DolarWesternUnion>, Task> OnWesternUnion,
                                            Func<IEnumerable<DolarCryptoPrices>, Task> OnCrypto);
