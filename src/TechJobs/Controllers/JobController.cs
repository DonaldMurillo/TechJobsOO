using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            SearchJobsViewModel searchJobsViewModel = new SearchJobsViewModel();
            //Job newJob = new Job();
            Job newJob = jobData.Find(id);
            searchJobsViewModel.Jobs = new System.Collections.Generic.List<Job>();
            searchJobsViewModel.Jobs.Add(newJob);

            return View(searchJobsViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.JobLocation),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.JobCoreCompetency),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.JobPoitionType)
                    // set properties within braces
                };

                jobData.Jobs.Add(newJob);

                //return RedirectToAction("Job", "Index" , new { id = newJob.ID });

                //return Redirect("Index?=" + newJob.ID.ToString());

                //return RedirectToAction( "Job" ,new { id = newJob.ID });

                return Redirect(Url.Action("Index") + "?id=" + newJob.ID);
            }

            return View(newJobViewModel);
        }
    }
}
