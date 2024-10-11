using System.Diagnostics;
using NUnit.Framework.Internal;

class Employees
{
    public static IEnumerable<Employee> GetJuniors5() {	
        var juniors = new List<Employee>() {
            new(1, "Юдин Адам"),
            new(2, "Яшина Яна"),
            new(3, "Никитина Вероника"),
            new(4, "Рябинин Александр"),
            new(5, "Ильин Тимофей")
        };
        
        return juniors;
    }

    public static IEnumerable<Employee> GetTeamLeads5() {
        var teamLeads = new List<Employee>() {
            new(1, "Филиппова Ульяна"),
            new(2, "Николаев Григорий"),
            new(3, "Андреева Вероника"),
            new(4, "Коротков Михаил"),
            new(5, "Кузнецов Александр")
        };
        return teamLeads;
    }

    public static IEnumerable<Wishlist> GetSimpleTeamLeadsWishLists(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors) {
        var teamLeadsArray = teamLeads.ToArray();
        var juniorsArray = juniors.ToArray();
        Debug.Assert(teamLeadsArray.Length == juniorsArray.Length);

        List<Wishlist> wishlists = [];
        for (int i = 0; i < teamLeadsArray.Length; ++i) 
        {
            List<Employee> preferredJuniors = [];
            for (int j = 0; j < juniorsArray.Length; ++j)
            {
                preferredJuniors.Add(juniorsArray[(i + j) % juniorsArray.Length]);
            }
            wishlists.Add(new Wishlist(teamLeadsArray[i], preferredJuniors));
        }
        return wishlists;
    }

    public static IEnumerable<Wishlist> GetSimpleJuniorsWishLists(IEnumerable<Employee> juniors, IEnumerable<Employee> teamLeads) {
        var juniorsArray = juniors.ToArray();
        var teamLeadsArray = teamLeads.ToArray();
        Debug.Assert(juniorsArray.Length == teamLeadsArray.Length);

        List<Wishlist> wishlists = [];
        for (int i = 0; i < juniorsArray.Length; ++i) {
            List<Employee> preferredTeamLeads = [];
            for (int j = 0; j < teamLeadsArray.Length; ++j)
            {
                preferredTeamLeads.Add(teamLeadsArray[(i + j) % teamLeadsArray.Length]);
            }
            wishlists.Add(new Wishlist(juniorsArray[i], preferredTeamLeads));
        }
        return wishlists;
    }

    public static IEnumerable<Team> GetSimpleTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors) {
        var teams = new List<Team>();
        Debug.Assert(teamLeads.Count() == juniors.Count());
        var teamLeadsArray = teamLeads.ToArray();
        var juniorArray = juniors.ToArray();
        for (int i = 0; i < teamLeads.Count(); ++i) 
        {
            teams.Add(new Team(teamLeadsArray[i], juniorArray[i]));
        }
        return teams;
    }
}