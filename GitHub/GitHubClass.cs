using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubProject
{
    class GitHubClass
    {
        GitHubClient github;
        IReadOnlyList<Repository> repository;
        List<string> languages;

        public GitHubClass()
        {
            github = new GitHubClient(new ProductHeaderValue("GitHubAPITest"));
            github.Credentials = new Credentials("6bcf82c8d7795ae104b88848554f606d30380bf5"); ;
        }

        public async void GetUser(string userName)
        {
            try
            {
                var user = await github.User.Get(userName.Trim());
            }
            catch (Exception excep)
            {
                throw excep;
            }

        }

        public async Task<bool> GetUserRepos(string userName)
        {
            bool success = false;

            if (!string.IsNullOrEmpty(userName))
            {
                try
                {
                    repository = await github.Repository.GetAllForUser(userName.Trim());
                    if (repository == null || !repository.Any())
                    {
                        Console.WriteLine("No contiene reposirotios.");
                    }
                    else
                    {
                        success = true;
                    }
                }
                catch (Exception excep)
                {
                    throw excep;
                }
            }
            else
            {
                Console.WriteLine("Usuario inválido");
            }

            return success;
        }

        public void ShowUserReposLanguages()
        {
            languages = new List<string>();
            languages.Add("All");
            Console.Write("All");

            if (repository != null && repository.Any())
            {
                foreach (var repo in repository)
                {
                    if (!languages.Contains(repo.Language))
                    {
                        languages.Add(repo.Language);
                        Console.Write(" " + repo.Language);
                    }
                }
            }
            Console.WriteLine();

        }

        public void FilterRepos(string filter)
        {
            if (filter.Equals("All") || languages.Contains(filter))
            {
                foreach (var repo in repository)
                {
                    if (filter.Equals("All") || (repo.Language != null && repo.Language.Equals(filter)))
                    {
                        Console.WriteLine(repo.Name);
                    }
                }
            }
            else
            {
                Console.WriteLine("Lenguaje erróneo");
            }
        }

    }
}
