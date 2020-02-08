﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Models;
using EmployeesVacations.Repositories;

namespace EmployeesVacations.Controllers
{
    public class TeamController : Controller
    {
        private TeamRepository teamRepository = new TeamRepository();
        // GET: Team
        public ActionResult Index()
        {
            List<TeamModel> allTeams = teamRepository.GetAllTeams();
            return View("Index", allTeams);
        }

        // GET: Team/Details/5
        public ActionResult Details(Guid id)
        {
            TeamModel teamModel = teamRepository.GetTeamByID(id);
            return View("DetailsTeam", teamModel);
        }

        // GET: Team/Create
        public ActionResult Create()
        {

            return View("CreateTeam");
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                TeamModel teamModel = new TeamModel();
                UpdateModel(teamModel);
                teamRepository.InsertTeam(teamModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateTeam");
            }
        }

        // GET: Team/Edit/5
        public ActionResult Edit(Guid id)
        {
            TeamModel teamModel = teamRepository.GetTeamByID(id);
            return View("EditTeam", teamModel);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                TeamModel teamModel = new TeamModel();
                UpdateModel(teamModel);
                teamRepository.UpdateTeam(teamModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditTeam");
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(Guid id)
        {
            TeamModel teamModel = teamRepository.GetTeamByID(id);
            return View("DeleteTeam", teamModel);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                teamRepository.DeleteTeam(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteTeam");
            }
        }
    }
}
