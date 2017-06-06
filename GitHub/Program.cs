using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubProject;

namespace GitHubProject
{
    class Program
    {
        static void Main(string[] args)
        {
            GitHubClass gitHub = new GitHubClass();
            Console.WriteLine("GITHUB REPOS SEARCH");

            try
            {
                do
                {
                    var asyncTask = Task.Run(async () =>
                    {
                        Console.Write("\nIngresa el nombre el usuario a buscar: ");
                        var userName = Console.ReadLine();

                        var success = await gitHub.GetUserRepos(userName);

                        if (success)
                        {
                            Console.WriteLine("¿Desea filtar algún lenguaje?");
                            gitHub.ShowUserReposLanguages();
                            Console.Write("Escriba el lenguaje a filtrar: ");
                            var filter = Console.ReadLine();
                            gitHub.FilterRepos(filter);
                        }

                    });
                    asyncTask.Wait();
                    asyncTask.Dispose();

                    Console.Write("\n¿Realizar otra búsqueda? Y/N :");
                    var again = Console.ReadLine();
                    if (again.Equals("N") || again.Equals("n"))
                    {
                        Environment.Exit(0);
                    }
                } while (true);
            }
            catch (Exception excep)
            {
              throw excep;
            }
        }

    }
}
