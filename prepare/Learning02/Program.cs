using System;

Job job1 = Job.job("Del Taco", "Manager", 1706, 2020);
job1.Display();



Job job2 = Job.job("Google", "Manager", 2020, 2024);
job2.Display();

Resume r = new()
{
    _name = "Blayke Peapealalo"
};

r._jobs.Add(job1);
r._jobs.Add(job2);

r.Display();
