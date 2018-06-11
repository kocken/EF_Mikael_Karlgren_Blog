﻿using Domain;
using System;
using System.Linq;

namespace Data
{
    public static class DbInitializer
    {
        private static bool ReInitialize = false; // NOTE: when true the DB will be reset with the entries below

        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            // Returns true if there are any rank entries
            if (context.Ranks.Any())
            {
                if (ReInitialize)
                {
                    context.CommentEvaluations.RemoveRange(context.CommentEvaluations.Where(c => c.CommentId == c.CommentId));
                    context.Evaluations.RemoveRange(context.Evaluations.Where(e => e.Id == e.Id));
                    context.EvaluationValues.RemoveRange(context.EvaluationValues.Where(e => e.Id == e.Id));
                    context.Comments.RemoveRange(context.Comments.Where(c => c.Id == c.Id));
                    context.Threads.RemoveRange(context.Threads.Where(t => t.Id == t.Id));
                    context.Users.RemoveRange(context.Users.Where(u => u.Id == u.Id));
                    context.Ranks.RemoveRange(context.Ranks.Where(r => r.Id == r.Id));

                    context.SaveChanges();
                }
                else
                {
                    return; // DB has been seeded, end method
                }
            }


            Rank[] ranks = new Rank[]
            {
                new Rank{ Name = "Member" },
                new Rank{ Name = "Moderator" },
                new Rank{ Name = "Administrator" }
            };
            foreach (Rank r in ranks)
            {
                context.Ranks.Add(r);
            }
            context.SaveChanges();


            User[] users = new User[]
            {
                new User{
                    Username = "Mikael",
                    Password = "pass123",
                    Rank = Array.Find(ranks, r => r.Name.Equals("Administrator")),
                    JoinTime = DateTime.Now
                },
                new User{
                    Username = "Mikael2",
                    Password = "pass123",
                    Rank = Array.Find(ranks, r => r.Name.Equals("Moderator")),
                    JoinTime = DateTime.Now
                },
                new User{
                    Username = "Billy",
                    Password = "qwerty",
                    Rank = Array.Find(ranks, r => r.Name.Equals("Member")),
                    JoinTime = DateTime.Now
                }
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();


            Thread[] threads = new Thread[]
            {
                new Thread{
                    User = Array.Find(users, u => u.Username.Equals("Mikael")),
                    Title = "First thread",
                    Message = "This is the first thread made!",
                    CreationTime = DateTime.Now
                }
            };
            foreach (Thread t in threads)
            {
                context.Threads.Add(t);
            }
            context.SaveChanges();


            Comment[] comments = new Comment[]
            {
                new Comment{
                    User = Array.Find(users, u => u.Username.Equals("Billy")),
                    Thread = threads[0],
                    Message = "Cool!",
                    CreationTime = DateTime.Now
                },
                new Comment{
                    User = Array.Find(users, u => u.Username.Equals("Billy")),
                    Thread = threads[0],
                    Message = "Can I get mod?",
                    CreationTime = DateTime.Now
                }
            };
            foreach (Comment c in comments)
            {
                context.Comments.Add(c);
            }
            context.SaveChanges();


            EvaluationValue[] evaluationValues = new EvaluationValue[]
            {
                new EvaluationValue{ Name = "Approved" },
                new EvaluationValue{ Name = "Disapproved" }
            };
            foreach (EvaluationValue e in evaluationValues)
            {
                context.EvaluationValues.Add(e);
            }
            context.SaveChanges();


            Evaluation[] evaluations = new Evaluation[]
            {
                new Evaluation{
                    Comment = comments[0],
                    EvaluationValue = Array.Find(evaluationValues, e => e.Name.Equals("Approved")),
                    EvaluatedBy = Array.Find(users, u => u.Username.Equals("Mikael")),
                    EvaluationTime = DateTime.Now
                },
                new Evaluation{
                    Comment = comments[0],
                    EvaluationValue = Array.Find(evaluationValues, e => e.Name.Equals("Approved")),
                    EvaluatedBy = Array.Find(users, u => u.Username.Equals("Mikael2")),
                    EvaluationTime = DateTime.Now
                }
            };
            foreach (Evaluation e in evaluations)
            {
                context.Evaluations.Add(e);
            }
            context.SaveChanges();


            CommentEvaluation[] commentEvaluations = new CommentEvaluation[]
            {
                new CommentEvaluation{
                    Comment = comments[0],
                    Evaluation = evaluations[0]
                },
                new CommentEvaluation{
                    Comment = comments[0],
                    Evaluation = evaluations[1]
                }
            };
            foreach (CommentEvaluation c in commentEvaluations)
            {
                context.CommentEvaluations.Add(c);
            }
            context.SaveChanges();
        }
    }
}
