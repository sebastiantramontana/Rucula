using Rucula.Domain.Entities;

namespace Rucula.Domain.Abstractions;

public sealed record class ChoicesCallbacks(Func<WinningChoice, Task> OnWinningChoice,
                                            Func<IEnumerable<TituloIsin>, Task> OnBonds,
                                            Func<Optional<Blue>, Task> OnBlue,
                                            Func<Optional<DolarWesternUnion>, Task> OnWesternUnion,
                                            Func<IEnumerable<DolarCryptoPrices>, Task> OnCrypto);
