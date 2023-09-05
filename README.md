# Microservices on ASP.NET 8.0

The course has some drawbacks, mainly the .NET version its based on. Since I
program on Linux, I've found myself some shortcommings that I'd like to write
down in the remote case anyone is in a similar situation or setup as mine.

## Setting up .NET 8.0 SDK

You'll need, first of all, the SDK and run-times that the course is based on.
Luckily, Microsoft offers a handy script that basically downloads all the
`.rpm`s you'll need and installs them for you. I cannot find the page that I
found the script but you can download it
[here](https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh)

However, when executing the script, you may encounter some issues if you already
have installed previous versions. At time of writing this, the LTS and Standard
versions are 6.0 and 7.0 respectively.

For those of you running Fedora Workstation, you'll be glad to know you can `cd`
to the directory where all the `.rmp`s are and run `sudo yum localinstall *
--allowerase`. Which will remove the dependencies that you have in place for the
8.0 preview.

## NuGet Packet Manager "workarounds"

You'll be able now to use `dotnet restore` succesfully. Although there are some
other issues we have to attend first.

If you are using Linux like me, doing things like migrations, database updates
and other shennanigans that use the NuGet packet manager available in Visual
Studio, you'll need to install globally available tools since it relies on
`EntityFramework` and I'm using Neovim we'll have to use the good ol' CLI to
accomplish the same tasks

- [EntityFramework](https://www.nuget.org/packages/dotnet-ef/8.0.0-preview.7.23375.4)

## Environment variables

Now we can use `dotnet user-secrets init` so we create a `secrets.json` that can
be called anywhere in the project where we need. We should add our connection
string first so we can create our first migration and update the SQL table.

``` dotnet user-secrets set "Coupons:DockerConnection" "Server=localhost,1433;
Database=master; User=sa; Password=pwd; TrustServerCertificate=true;" ```

We should `dotnet clean && dotnet build` for good measure and run:

``` dotnet ef migrations add <MigrationName> dotnet ef database update ```

If all went well, we should see a succesfull message on our CLI.
