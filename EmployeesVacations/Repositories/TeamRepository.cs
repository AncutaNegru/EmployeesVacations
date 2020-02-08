using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesVacations.Models;
using EmployeesVacations.Models.DBObjects;

namespace EmployeesVacations.Repositories
{
    public class TeamRepository
    {
        private EmployeesAndVacationsModelsDataContext dbContext;
        public TeamRepository()
        {
            this.dbContext = new EmployeesAndVacationsModelsDataContext();
        }
        public TeamRepository(EmployeesAndVacationsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private TeamModel MapDbObjectToModel(Team dbTeam)
        {
            TeamModel teamModel = new TeamModel();
            if (dbTeam != null)
            {
                teamModel.IDTeam = dbTeam.IDTeam;
                teamModel.IDBusinessUnit = dbTeam.IDBusinessUnit;
                teamModel.Name = dbTeam.Name;
                teamModel.IDTeamLead = dbTeam.IDTeamLead;
                return teamModel;
            }
            return null;
        }
        private Team MapModelToDbObject(TeamModel teamModel)
        {
            Team dbTeam = new Team();
            if (teamModel != null)
            {
                dbTeam.IDTeam = teamModel.IDTeam;
                dbTeam.IDBusinessUnit = teamModel.IDBusinessUnit;
                dbTeam.Name = teamModel.Name;
                dbTeam.IDTeamLead = teamModel.IDTeamLead;
                return dbTeam;
            }
            return null;
        }
        public List<TeamModel> GetAllTeams()
        {
            List<TeamModel> allTeamsList = new List<TeamModel>();
            foreach (Team dbTeam in dbContext.Teams)
            {
                allTeamsList.Add(MapDbObjectToModel(dbTeam));
            }
            return allTeamsList;
        }
        public TeamModel GetTeamByID(Guid id)
        {
            TeamModel teamModel = MapDbObjectToModel(dbContext.Teams.FirstOrDefault(x => x.IDTeam == id));
            return teamModel;
        }
        public void InsertTeam(TeamModel teamModel)
        {
            teamModel.IDTeam = Guid.NewGuid();
            dbContext.Teams.InsertOnSubmit(MapModelToDbObject(teamModel));
            dbContext.SubmitChanges();
        }
        public void UpdateTeam(TeamModel teamModel)
        {
            Team dbTeam = dbContext.Teams.FirstOrDefault(x => x.IDTeam == teamModel.IDTeam);
            if (dbTeam != null)
            {
                dbTeam.IDTeam = teamModel.IDTeam;
                dbTeam.IDBusinessUnit = teamModel.IDBusinessUnit;
                dbTeam.Name = teamModel.Name;
                dbTeam.IDTeamLead = teamModel.IDTeamLead;
                dbContext.SubmitChanges();
            }
        }
        public void DeleteTeam(Guid id)
        {
            Team dbTeamToDelete = dbContext.Teams.FirstOrDefault(x => x.IDTeam == id);
            if (dbTeamToDelete != null)
            {
                dbContext.Teams.DeleteOnSubmit(dbTeamToDelete);
                dbContext.SubmitChanges();
            }
        }
    }
}