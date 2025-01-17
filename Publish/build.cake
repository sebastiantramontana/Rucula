#tool "dotnet:?package=GitVersion.Tool&version=6.1.0"
#r "tools/TailwindPlayRemover.dll"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
string version = String.Empty;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => {
	CleanDirectories("./../dist",new CleanDirectorySettings { Force = true });
    DotNetClean("./../Rucula.WebAssembly/Rucula.WebAssembly.csproj");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Description("Restaurando las dependencias")
    .Does(() => {
    
    var projects = GetFiles("./../*/*.csproj");
    var settings =  new DotNetRestoreSettings
    {
      Verbosity = DotNetVerbosity.Minimal,
      Sources = new [] { "https://api.nuget.org/v3/index.json" }
    };
    foreach(var project in projects )
    {
      Information($"Restoring { project.ToString()}");
      DotNetRestore(project.ToString(), settings);
    }

});

Task("Version")
  .IsDependentOn("Restore")
   .Does(() =>
{
   var result = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });
    
    version =  result.FullSemVer.ToString();
    Information($"Nuget Version: { version.ToString() }");
    Information($"Semantic Version: { result.FullSemVer.ToString() }");
});

Task("Build")
    .IsDependentOn("Version")
    .Does(() => {
     var buildSettings = new DotNetBuildSettings { Configuration = configuration };
     var projects = GetFiles("./../*/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building {project.ToString()}");
         DotNetBuild(project.ToString(),buildSettings);
     }
});

Task("Publish")
    .IsDependentOn("Build")
    .Does(() => {
		 var projects = GetFiles("./../Rucula.WebAssembly/Rucula.WebAssembly.csproj");
		 foreach(var project in projects )
		 {
		   var publishSettings = new DotNetPublishSettings  {
										   Configuration = configuration,
										   NoLogo = true,
										   NoRestore = true,
										   NoBuild = false,
										   Framework = "net9.0",
										   SelfContained = true,
										   Runtime = "browser-wasm",
										   OutputDirectory = "./../dist",
									   };
		   Information($"Publicando wasm de Rúcula");
		   DotNetPublish(project.ToString(), publishSettings );
		 }
});

Task("Moving")
    .IsDependentOn("Publish")
	.Description("Moviendo la carpeta _framework al raiz de la carpeta de publicacion y eliminando wwwroot...")
    .Does(() => {
		Information("Moviendo _framework y eliminado wwwroot...");
		CopyDirectory("./../dist/wwwroot/_framework", "./../dist/_framework");
		DeleteDirectory("./../dist/wwwroot", new DeleteDirectorySettings { Recursive = true, Force = true });
		DeleteFiles("./../dist/_framework/*.pdb.gz");
});

Task("CopyStatics")
	.Description("Copiando archivos estaticos")
    .Does(() => {
		CreateDirectory("../dist/images/");
		CreateDirectory("../dist/modules/");
		CopyFiles("../Rucula.Frontend/images/*.*", "../dist/images/");
		CopyFiles("../Rucula.Frontend/modules/*.*", "../dist/modules/");
		CopyFiles("../Rucula.Frontend/*.*", "../dist/");
		DeleteFile("./../dist/web.config");
});

Task("Css")
	.Description("Procesando CSS")
    .Does(() => {
		CreateDirectory("../dist/css/");
		CopyFiles("../Rucula.Frontend/css/titulos-publicos.css", "../dist/css/");
		CopyFiles("../Rucula.Frontend/css/wu.css", "../dist/css/");
		CopyFiles("../Rucula.Frontend/css/crypto.css", "../dist/css/");
		RemoveTailwindCdn("../dist/index.html");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.IsDependentOn("Version")
	.IsDependentOn("Build")
	.IsDependentOn("Publish")
	.IsDependentOn("Moving")
	.IsDependentOn("CopyStatics")
	.IsDependentOn("Css");

RunTarget(target);