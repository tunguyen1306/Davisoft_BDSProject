using System;
using AutoMapper;
using Quartz;
using Quartz.Impl;
using Davisoft_BDSProject.Domain.Concrete;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Helpers
{
    //public class ScheduleHelper
    //{
    //    public static void UpdatePriceSchedule(int scheduleID, DateTime time)
    //    {
    //        string JOB_NAME = scheduleID.ToString();
    //        string TRIGGER_NAME = scheduleID.ToString();

    //        // TODO: unset old schedule
    //        // Assigned to: Nhat

    //        //Create the scheduler factory
    //        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

    //        //Ask the scheduler factory for a scheduler
    //        IScheduler scheduler = schedulerFactory.GetScheduler();

    //        //Start the scheduler so that it can start executing jobs
    //        scheduler.Start();

    //        // Create a job of Type WriteToConsoleJob
    //        IJobDetail job = JobBuilder.Create(typeof (UpdatePriceSchedule))
    //                                   .WithIdentity(JOB_NAME)
    //                                   .Build();

    //        job.JobDataMap["id"] = scheduleID;

    //        //Schedule this job to execute every second, a maximum of 10 times
    //        string date = string.Format("{0} {1} {2} {3} {4} ? {5}", time.Second, time.Minute, time.Hour, time.Day, time.Month, time.Year);
    //        //ITrigger trigger =
    //        //    TriggerBuilder.Create()
    //        //                  .WithSchedule(CronScheduleBuilder.CronSchedule(date))
    //        //                  .StartNow()
    //        //                  .WithIdentity(TRIGGER_NAME)
    //        //                  .Build();
    //        ITrigger trigger =
    //            TriggerBuilder.Create()
    //                          .WithSchedule(CronScheduleBuilder.CronSchedule(date))
    //                          .StartNow()
    //                          .WithIdentity(TRIGGER_NAME)
    //                          .Build();
            
    //        scheduler.ScheduleJob(job, trigger);
    //    }

    //    public static void Update(int scheduleID, DateTime time)
    //    {
    //        string JOB_NAME = scheduleID.ToString();
    //        string TRIGGER_NAME = scheduleID.ToString();
    //        // TODO: unset old schedule
    //        // Assigned to: Nhat

    //        //Create the scheduler factory
    //        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

    //        //Ask the scheduler factory for a scheduler
    //        IScheduler scheduler = schedulerFactory.GetScheduler();

    //        //Start the scheduler so that it can start executing jobs
    //        scheduler.Start();

    //        // Create a job of Type WriteToConsoleJob
    //        IJobDetail job = JobBuilder.Create(typeof(UpdatePriceSchedule))
    //                                   .WithIdentity(JOB_NAME)
    //                                   .Build();

    //        job.JobDataMap["id"] = scheduleID;

    //        //Schedule this job to execute every second, a maximum of 10 times
    //        string date = string.Format("{0} {1} {2} {3} {4} ? {5}", time.Second, time.Minute, time.Hour, time.Day, time.Month, time.Year);
    //        TriggerKey triggerKey  = new TriggerKey(TRIGGER_NAME);

    //        ITrigger oldtrigger = scheduler.GetTrigger(triggerKey);
            
    //        ITrigger trigger = TriggerBuilder.Create()
    //            .WithSchedule(CronScheduleBuilder.CronSchedule(date))
    //                          .StartNow()
    //                          .WithIdentity(TRIGGER_NAME)
    //                          .Build();
    //        scheduler.RescheduleJob(triggerKey, trigger);
    //        //scheduler.ScheduleJob(job, trigger);
    //    }

    //    public static void Delete(int scheduleID)
    //    {
    //        string JOB_NAME = scheduleID.ToString();
    //        string TRIGGER_NAME = scheduleID.ToString();

    //        //Create the scheduler factory
    //        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

    //        //Ask the scheduler factory for a scheduler
    //        IScheduler scheduler = schedulerFactory.GetScheduler();

    //        //Start the scheduler so that it can start executing jobs
    //        scheduler.Start();

    //        TriggerKey triggerKey = new TriggerKey(TRIGGER_NAME);
    //        ITrigger oldtrigger = scheduler.GetTrigger(triggerKey);

    //        scheduler.UnscheduleJob(triggerKey);
    //    }
    //}

    //public class UpdatePriceSchedule : IJob
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {
    //        int scheduleID = int.Parse(context.JobDetail.JobDataMap["id"].ToString());

    //        using (var db = new Davisoft_BDSProjectDb())
    //        using (var pricingService = new EFInternetPricingRepository(db))
    //        {
    //            ModelPriceSchedule schedule = pricingService.GetSchedule(scheduleID);

    //            pricingService.UpdateModelPriceWithSchedule(schedule);
    //        }
    //    }

    //    public static void Test()
    //    {
    //        using (var db = new Davisoft_BDSProjectDb())
    //        using (var pricingService = new EFInternetPricingRepository(db))
    //        {
    //            ModelPriceSchedule schedule = pricingService.GetSchedule(1);
    //            //var mappedPrice = schedule.GetValues<MappedModelPrice>();
    //            //var price = Mapper.Map<ModelPrice>(mappedPrice);
    //            pricingService.UpdateModelPriceWithSchedule(schedule);
    //        }
    //    }
    //}
}
