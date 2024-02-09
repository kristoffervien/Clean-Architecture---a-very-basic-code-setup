using CleanArch.Domain.Common;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private UniversityDBContext _ctx;

        public CourseRepository(UniversityDBContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(Course course)
        {

            var claimantRepository = new ClaimantRepository(_ctx);

            var claimant = new Claimant
            {
                Note = "Some Note",
                UserId = "Some UserId",
            };

            Task.Run(async () =>
            {
                await claimantRepository.AddAsync(claimant);
                var result1 = await claimantRepository.SaveChangesAsync();

            }).Wait();


            var claimaintId = claimant.Id;

            Task<Claimant> claimaintEntiry = claimantRepository.GetAsync(claimaintId);
            claimaintEntiry.Wait();

            var ClaimantEntity = claimaintEntiry.Result;

            Task.Run(async () =>
            {
                ClaimantEntity.UserId = $"{ClaimantEntity.UserId} Updated";
                ClaimantEntity.Note = $"{ClaimantEntity.Note} Updated";
                ClaimantEntity.LastModified = DateTime.Now;
                ClaimantEntity.LastModifiedBy = (new Guid()).ToString();

                await claimantRepository.UpdateAsync(ClaimantEntity);
                var result2 = await claimantRepository.SaveChangesAsync();

            }).Wait();


            Task.Run(async () => 
            {
                await claimantRepository.RemoveAsync(claimaintId);
                var result3 = await claimantRepository.SaveChangesAsync();

            }).Wait();


            Task.Run(async () =>
            {
                var allClaimants = await claimantRepository.GetAllAsync();

                await claimantRepository
                .RemoveRangeAsync(
                    ids: string.Join(",", allClaimants.Select(p => p.Id)
                ));

                var result4 = await claimantRepository.SaveChangesAsync();

            }).Wait();

            _ctx.Courses.Add(course);
            _ctx.SaveChanges();
        }

        public Course GetCourse(int Id)
        {
            return _ctx.Courses.FirstOrDefault(x => x.Id == Id);
        }
    }


}
