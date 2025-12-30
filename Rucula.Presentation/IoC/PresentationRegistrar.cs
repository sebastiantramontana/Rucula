using Microsoft.Extensions.DependencyInjection;
using Rucula.Domain.Abstractions;
using Rucula.Presentation.ActionBinders;
using Rucula.Presentation.Format;
using Rucula.Presentation.Mappings;
using Rucula.Presentation.Presenters;
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
            .AddMappings();

    private static IServiceCollection AddActionBinders(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IRuculaScreenRefreshActionBinderAsync, RuculaScreenRefreshActionBinderAsync>()
            .AddSingleton<IRuculaParametersActionBinderAsync, RuculaParametersActionBinderAsync>()
            .AddSingleton<IRuculaParametersParser, RuculaParametersParser>();

    private static IServiceCollection AddFormatters(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ISpanishPriceFormatProvider, SpanishPriceFormatProvider>()
            .AddSingleton<ISpanishPriceFormatter, SpanishPriceFormatter>();

    private static IServiceCollection AddPresenters(this IServiceCollection serviceCollection)
        => serviceCollection
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
                .AddConfiguration(() => new VitrauxConfiguration{ UseShadowDom = false })
                .AddViewModel<WinningChoiceViewModel>().AddConfiguration<WinningChoiceConfigurationMapping>()
                .AddViewModel<BondsViewModel>().AddConfiguration<BondsConfigurationMapping>()
                .AddViewModel<CryptosViewModel>().AddConfiguration<CryptosConfigurationMapping>()
                .AddViewModel<BlueViewModel>().AddConfiguration<BlueConfigurationMapping>()
                .AddViewModel<WesternUnionViewModel>().AddConfiguration<WesternUnionConfigurationMapping>()
                .AddViewModel<NotifyProgressViewModel>().AddConfiguration<NotifyProgressConfigurationMapping>()
                .AddViewModel<ParametersViewModel>()
                    .AddConfiguration<ParametersConfigurationMapping>()
                    .AddActionParameterBinderAsync<RuculaParametersActionBinderAsync>()
                .AddViewModel<RuculaScreenViewModel>()
                    .AddConfiguration<RuculaScreenConfigurationMapping>()
                    .AddActionParameterBinderAsync<RuculaScreenRefreshActionBinderAsync>();

        return serviceCollection;
    }

    public static Task BuildPresentation(this IServiceProvider serviceProvider)
        => serviceProvider.BuildVitraux();
}