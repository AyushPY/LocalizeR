using LocalizeR.Core.DTO;
using LocalizeR.Core.Entities;
using LocalizeR.Core.Models;
using LocalizeR.Core.ServiceContracts;
using LocalizeR.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalizeR.WebAPI.Controllers
{
    [AllowAnonymous]
    public class JobSequenceController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJobSequencer _jobSequencer;
        private readonly IRequestClassifier _requestClassifier;
        public JobSequenceController(IJobSequencer jobSequencer, ApplicationDbContext context, IRequestClassifier requestClassifier)
        {
            _jobSequencer = jobSequencer;
            _context = context;
            _requestClassifier = requestClassifier;
        }
        [HttpPost("JobSequencing")]
        public async Task<IActionResult> JobSequencing(RequestDTO requestDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join("|", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            int result = 0;
            RequestSequence requestValues = new RequestSequence()
            {
                budget = requestDTO.budget,
                deadline = (DateTime)requestDTO.deadline,
                ServiceId = requestDTO.serviceID,
                RequesterId = requestDTO.UserID,
                RequestDetails = requestDTO.requestDetails
            };
            if (requestValues == null)
            {
                return Problem("Unable to accept HTTP Values");
            }
            else
            {
                await _context.RequestRecords.AddAsync(requestValues);
                result = await _context.SaveChangesAsync();
                var records = await _context.RequestRecords.Where(r => r.Severity == null).ToListAsync();
                if (records == null)
                {
                    return Problem("Unable to Find Specified Record");
                }
                foreach (var record in records)
                {

                    var severity = _requestClassifier.ClassifyRequestDetails(record.RequestDetails).ToString();
                    //if (severity != null)
                    //{
                    //    record.Severity = severity;
                    //    await _context.SaveChangesAsync();
                    //}

                }
            }
            if (result > 0)
            {
                //Work on Job Sequence Now
                var recordsForServiceId = await _context.RequestRecords
                 .Where(r => r.ServiceId == requestDTO.serviceID)
                 .Select(r => new BudgetDeadlinePair { budget = r.budget, deadline = r.deadline, requesterID = r.RequesterId })
                 .ToListAsync();

                var optimizedSequence = await _jobSequencer.SequenceJobs(recordsForServiceId);
                var optimizedJobs = optimizedSequence.Select(item => item.Job).ToList();

                //Work on Request Classification Now


                return Ok(new
                {
                    OptimizedSequence = optimizedJobs
                });
            }
            else
            {
                return Problem("Failed To Optimize Sequence");
            }
        }
    }
}
