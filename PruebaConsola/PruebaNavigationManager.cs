using Microsoft.AspNetCore.Components;

namespace PruebaConsola;

public class PruebaNavigationManager : NavigationManager
{
    public PruebaNavigationManager()
    {
        Initialize("http://localhost/", "http://localhost/rucula-dev/");
    }
}
