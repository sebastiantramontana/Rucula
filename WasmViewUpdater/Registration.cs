using Microsoft.Extensions.DependencyInjection;
using Vitraux.Modeling;

namespace Vitraux
{
    public static class Registration
    {
        public static void AddViewUpdater<T, TModelConfiguration>(this ServiceCollection serviceCollection)
            where TModelConfiguration : class, IModelConfiguration<T>
        {
            serviceCollection.AddSingleton<IViewUpdater<T>, ViewUpdater<T>>();
            serviceCollection.AddSingleton<IModelConfiguration<T>, TModelConfiguration>();
        }
    }
}
