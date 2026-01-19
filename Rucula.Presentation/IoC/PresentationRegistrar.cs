using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;
using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.Format;
using Rucula.Presentation.Mappings;
using Rucula.Presentation.Presenters;
using Rucula.Presentation.Repositories;
using Rucula.Presentation.ViewModels;
using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;

namespace Rucula.Presentation.IoC;

public static class PresentationRegistrar
{
    public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddPresenters()
            .AddActionBinders()
            .AddFormatters()
            .AddMappings()
            .AddParameterUtils();

    private static IServiceCollection AddParameterUtils(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IParametersRepository, ParametersRepository>()
            .AddSingleton<IParametersProvider>((sp) => sp.GetRequiredService<IParametersRepository>());

    private static IServiceCollection AddActionBinders(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IRuculaParametersSaver, RuculaParametersSaver>()
            .AddSingleton<IRuculaParametersParser, RuculaParametersParser>()
            .AddSingleton<IRuculaValidateParametersActionBinderAsync, RuculaValidateParametersActionBinderAsync>();

    private static IServiceCollection AddFormatters(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ISpanishPriceFormatter, SpanishPriceFormatter>();

    private static IServiceCollection AddPresenters(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IRuculaStarterPresenter, RuculaStarterPresenter>()
            .AddSingleton<IRuculaScreenPresenter, RuculaScreenPresenter>()
            .AddSingleton<IBluePresenter, BluePresenter>()
            .AddSingleton<IBondsPresenter, BondsPresenter>()
            .AddSingleton<ICryptosPresenter, CryptosPresenter>()
            .AddSingleton<IWesternUnionPresenter, WesternUnionPresenter>()
            .AddSingleton<IWinningChoicePresenter, WinningChoicePresenter>()
            .AddSingleton<INotifier, NotifyProgressPresenter>()
            .AddSingleton<IParametersPresenter, ParametersPresenter>();

    private static IServiceCollection AddMappings(this IServiceCollection serviceCollection)
    {
        _ = serviceCollection
                .AddSingleton<IConfigurationBehaviorProvider, ConfigurationBehaviorProvider>()
                .AddVitraux()
                .AddConfiguration(() => new VitrauxConfiguration { UseShadowDom = false })
                .AddViewModelConfiguration<WinningChoiceViewModel, WinningChoiceConfigurationMapping>()
                .AddViewModelConfiguration<BondsViewModel, BondsConfigurationMapping>()
                .AddViewModelConfiguration<CryptosViewModel, CryptosConfigurationMapping>()
                .AddViewModelConfiguration<BlueViewModel, BlueConfigurationMapping>()
                .AddViewModelConfiguration<WesternUnionViewModel, WesternUnionConfigurationMapping>()
                .AddViewModelConfiguration<NotifyProgressViewModel, NotifyProgressConfigurationMapping>()
                .AddViewModelConfiguration<BondParameterValuesViewModel, BondParametersConfigurationMapping>()
                .AddViewModelConfiguration<CryptoParameterValuesViewModel, CryptoParametersConfigurationMapping>()
                .AddViewModelConfiguration<WesternUnionParameterValuesViewModel, WesternUnionParametersConfigurationMapping>()
                .AddViewModel<RuculaScreenViewModel>()
                    .AddConfiguration<RuculaScreenConfigurationMapping>()
                    .AddActionParameterBinderAsync<RuculaValidateParametersActionBinderAsync>()
                .AddViewModel<SaveParametersViewModel>()
                    .AddConfiguration<SaveParametersConfigurationMapping>();

        return serviceCollection;
    }

    public static Task BuildPresentation(this IServiceProvider serviceProvider)
        => serviceProvider.BuildVitraux();
}