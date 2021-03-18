# LambdaNet5

Please follow the instructions below to setup the project

## Directory.Build.props
```xml
  <PropertyGroup>
    ...
        <PackageProjectUrl>{CHANGE_THIS}</PackageProjectUrl>
        <RepositoryUrl>{CHANGE_THIS}</RepositoryUrl>
    ...
    </PropertyGroup>
```

## Execute
> dotnet new sln
> dotnet new console -n {PROJECT_NAME} -o src/{PROJECT_NAME}

## Copy
Sample.DockerFile from root folder to project folder and rename to Dockerfile

## Add Function.cs in src/{PROJECT_NAME}

```c#    
    public class Function
    {
        /// <summary>
        ///     A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="context">Lambda context.</param>
        public void FunctionHandler(ILambdaContext context)
        {
            Program.SetUp();
            // Action to take.
        }
    }
```

## Add to Program.cs

```c#
        /// <summary>
        ///     Gets or sets configuration root.
        /// </summary>
        public static IConfigurationRoot? Configuration { get; set; }

        /// <summary>
        ///     Default set up.
        /// </summary>
        public static void SetUp()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            AWSXRayRecorder.InitializeInstance(Configuration);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            Audit.Core.Configuration.Setup().UseSerilog();
            Audit.Core.Configuration.AuditDisabled = true;
        }
        ...        
```
